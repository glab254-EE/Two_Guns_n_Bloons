using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] private Transform NonCamera;
    bool locked = true;
    float rotx = 0f;
    void Update()
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        if (Input.GetKey(KeyCode.Escape)) locked = !locked;
        float moveX = Input.GetAxis("Mouse X") * 15f *sensitivity* Time.deltaTime; 
        float moveY = Input.GetAxis("Mouse Y") * 14f * sensitivity * Time.deltaTime;
        rotx -= moveY;
        rotx = Mathf.Clamp(rotx, -50, 75);
        gameObject.transform.localRotation = Quaternion.Euler(rotx, 0, 0);
        NonCamera.Rotate(Vector3.up * moveX );
    }
}
