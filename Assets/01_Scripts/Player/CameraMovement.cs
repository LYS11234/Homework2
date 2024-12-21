using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private float cameraRotationLimit;

    private float currentCameraRotationX = 0;
    private float currentCameraRotationY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        transform.position = player.position;
        CameraRotate();
    }

    private void CameraRotate()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        float _yRotation = -Input.GetAxisRaw("Mouse X");
        float _cameraRotationY = _yRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationY -= _cameraRotationY;
        float currentPlayerRotation = currentCameraRotationY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        currentCameraRotationY = Mathf.Clamp(currentCameraRotationY, -1e+12f, 1e+12f);
        this.transform.localEulerAngles = new Vector3(currentCameraRotationX, currentCameraRotationY, 0f);
        Quaternion rotation = Quaternion.Euler(player.rotation.x, currentPlayerRotation, player.rotation.z);
    }
}
