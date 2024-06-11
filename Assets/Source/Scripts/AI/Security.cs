using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Security : MonoBehaviour
{
    [SerializeField] private float _radiusWalk;
    [SerializeField] private LayerMask _layerGround;
    [SerializeField] private LayerMask _layerWall;
    [SerializeField] private LayerMask _player;
    [SerializeField] private float _raycastResponseRadius;

    [SerializeField] private float raycastOffsetAngle = 45f;

    [SerializeField] private Animator _securityAnimator;
    [SerializeField] private Animator _alertButton;

    private bool _isResponse;
    private Vector3 _movePoint;

    private bool _isMovePoint;
    private bool _isPlayerDetected;


    public bool _isPressingReverse = false;

    private NavMeshAgent _agent;

    private Looting _playerLooting;



   private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerLooting = GameObject.FindObjectOfType<Looting>();
    }

   
   private void Update()
    {
      
        PlayerResponse();
        if (_isMovePoint) Patrolling();
        else GetRandomPoint();

        
    }


    private void Patrolling()
    {
        if (_isResponse == false)
        {
            _agent.SetDestination(_movePoint);
            if (Vector3.Distance(transform.position, _movePoint) < 1)
            {
                _isMovePoint = false;
            }
        }

    }


    private void GetRandomPoint()
    {
        if (_isResponse == false)
        {
            float ranX = Random.Range(-_radiusWalk, _radiusWalk);
            float ranZ = Random.Range(-_radiusWalk, _radiusWalk);

            _movePoint = new Vector3(ranX, 0, ranZ) + transform.position;

            if (Physics.Raycast(_movePoint, -Vector3.up, 5, _layerGround))
            {
                if (Physics.CheckSphere(_movePoint, 1, _layerWall) == false)
                {
                    _isMovePoint = true;
                    
                    return;
                }
            }
            _isMovePoint = false;
        }
    }



    private bool IsPressingReverseCheck() {
        if (_playerLooting.isPressing == true) {
            _isPressingReverse = true;
        }

        return _isPressingReverse;
    }



    private void PlayerResponse() {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _raycastResponseRadius, _player);

        foreach (var hitCollider in hitColliders)
        {
            Transform target = hitCollider.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            if (angle < raycastOffsetAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToTarget, out hit, _raycastResponseRadius) && _playerLooting.isResponsed == true)
                {
                    if (hit.collider.CompareTag("Player") && IsPressingReverseCheck() )
                    {
                       
                        _isResponse = true;
                        _agent.speed = 3f;
                        _agent.SetDestination(hit.collider.transform.position);
                        _securityAnimator.SetBool("IsRunning", true);
                        _alertButton.SetBool("IsActive", true);
                        return;
                    }
                }

            }
        }

        _alertButton.SetBool("IsActive", false);
        _isPlayerDetected = false;
        _isResponse = false;
        _agent.speed = 2f;
        _securityAnimator.SetBool("IsRunning", false);
    }


    
        
    


    private void OnDrawGizmos()
    {
       
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _raycastResponseRadius);

            Vector3 forward = transform.forward * _raycastResponseRadius;
            Vector3 leftBoundary = Quaternion.Euler(0, -raycastOffsetAngle / 2, 0) * forward;
            Vector3 rightBoundary = Quaternion.Euler(0, raycastOffsetAngle / 2, 0) * forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
        }
    
}
