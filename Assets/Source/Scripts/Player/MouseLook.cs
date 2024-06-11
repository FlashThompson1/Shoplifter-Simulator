using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform tPoseModelHead;
    [SerializeField] private Transform _playerBody;
    private float _xRotation = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        transform.position = tPoseModelHead.position;
        transform.rotation = tPoseModelHead.rotation;
        float mouseXAxis = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseYAxis = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseYAxis;
        _xRotation = Mathf.Clamp(_xRotation, -40f, 60f);


        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseXAxis);

    }

}
