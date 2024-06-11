using UnityEngine;

public class HidePlaces : MonoBehaviour
{


    private Security _security;
    private void Start()
    {
        
        _security = GameObject.FindObjectOfType<Security>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Movement player))
            _security._isPressingReverse = false;
    }


    
}
