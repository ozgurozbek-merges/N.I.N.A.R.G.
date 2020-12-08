using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public GameObject playerObject;

    public float speed = 0.1f;
    private float yaw = 0f;
    private float pitch = 0f;

    private void LateUpdate()
    {
        yaw += speed * Input.GetAxis("Mouse X");
        pitch -= speed * Input.GetAxis("Mouse Y");

        // Rotation for the Camera, handling Mouse X & Y
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(pitch, yaw, 0)), speed*10);

        // Rotation for the Player. handling only X because player can't bend World Space.
        playerObject.transform.eulerAngles = new Vector3(0, yaw, 0);
    }

}