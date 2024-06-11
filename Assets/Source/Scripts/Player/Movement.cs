using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _crouchMovementSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity;
    [SerializeField] private Transform _groundCheckObject;
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private GameObject _gameoverPanel;
     private AudioSource _runSound;

    private CapsuleCollider _playerCollider;
   private float x;
   private float z ;

    private bool _isGameOver = false;

    private Vector3 _velocity;
    private bool _isGrounded;


    private Looting _playerLooting;
    private void Start()
    {
        _runSound = GetComponent<AudioSource>();
        _characterAnimator = transform.GetChild(1).GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _playerCollider =   GetComponent<CapsuleCollider>();
        _playerLooting = GameObject.FindObjectOfType<Looting>();

    }

    private void Update()
    {
        if (_isGameOver == false) {
            PlayerMovement();
            Running();
            Jump();
            CheckingOnGrounded();
            Crouch();
            CrouchWalking();
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");


        }

        

}

    private void PlayerMovement()
    {
        
        _characterAnimator.SetBool("IsMoving", false);
        _runSound.Pause();



        if (x < 0 || x > 0 || z < 0 || z > 0)
        {
            _characterAnimator.SetBool("IsMoving", true);
            

        }
        Vector3 moving = transform.right * x + transform.forward * z;
        _characterController.Move(moving * _movementSpeed * Time.deltaTime);
        _characterController.Move(_velocity * Time.deltaTime);
        _velocity.y += _gravity * Time.deltaTime;

        

    }


    private void Running()
    {
        if (_characterController.isGrounded) {
            if (x != 0 && Input.GetKey(KeyCode.LeftShift) || z != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                _runSound.Play();
                _characterAnimator.SetBool("IsRunning", true);
                Vector3 direction = transform.forward;



                direction.y = _characterController.isGrounded ? 0 : -_gravity * Time.deltaTime;
                _characterController.Move(new Vector3(direction.x * GetRunSpeed(), direction.y, direction.z * GetRunSpeed()));
            }
            else
            {
                _characterAnimator.SetBool("IsRunning", false);

            }
        }
        
    }


    private float GetRunSpeed()
     => _runSpeed * z * Time.deltaTime;



    private float GetCrouchSpeed()
     => _crouchMovementSpeed * z * Time.deltaTime;

    private void Jump()
    {

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _characterAnimator.SetBool("_isJump", true);
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

        }
        else if (_characterController.isGrounded) {
            _characterAnimator.SetBool("_isJump", false);
        }
       
    }


    private void Crouch() {
        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _characterAnimator.SetBool("IsCrouched", true);
                _characterController.height = 2;
                _playerCollider.height = 2;
                
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                _characterAnimator.SetBool("IsCrouched", false);
                _characterController.height = 2.65f;
                _playerCollider.height = 2.65f;
            }

        }
    }

    private void CrouchWalking()
    {

        if (_characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftControl) && x != 0 || Input.GetKey(KeyCode.LeftControl) && z != 0)
            {
                
                _characterAnimator.SetBool("IsCrouchMove", true);
                Vector3 direction = transform.forward;



                direction.y = _characterController.isGrounded ? 0 : -_gravity * Time.deltaTime;
                _characterController.Move(new Vector3(direction.x * GetCrouchSpeed(), direction.y, direction.z * GetCrouchSpeed()));
            }
            else
            {
                _characterAnimator.SetBool("IsCrouchMove", false);

            }
        }

    }



    private void CheckingOnGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckObject.position, _groundDistance, _groundMask);


        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }


    public void Gameover() {
    
        _isGameOver = true;
        _gameoverPanel.SetActive(true);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _groundDistance);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Security security) && security._isPressingReverse == true) {
            Gameover();
        }
    }

}
