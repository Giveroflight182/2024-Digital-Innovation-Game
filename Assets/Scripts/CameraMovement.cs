using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of mouse movement
    public Transform playerBody; // Reference to the player's body GameObject

    float xRotation = 0f;

    void Start()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp rotation to prevent flipping

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate camera vertically
        playerBody.Rotate(Vector3.up * mouseX); // Rotate player horizontally
    }
}
