using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    [Header("Player")]
    public Transform playerObject;

    [Header("Mouse")]
    public float mouseSensitivity = 100f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    private float xRotation = 0f;

    private void Start() {
        Cursor.visible = false;
        // Locks the mouse in window
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        mouseX = mouseSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime; // Mouse sideways
        mouseY = mouseSensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime; // Mouse up-down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Adjusts the Camera angle limitations.
    }

    private void LateUpdate()
    {
        // Rotation for the Camera, handling Mouse Y
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotation for the Player. Handling only X because player can't bend World Space.
        playerObject.Rotate(Vector3.up * mouseX);
    }

}