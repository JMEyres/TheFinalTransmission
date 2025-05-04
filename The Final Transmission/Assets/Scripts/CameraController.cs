using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 2.0f;
    Transform originalTransform;
    float pitch = 0f;
    float yaw = 0f;
    float initialYaw;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        originalTransform = transform;
        initialYaw = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;

        yaw = Mathf.Clamp(yaw, -100f, 100f);
        pitch = Mathf.Clamp(pitch, -60f, 80f);

        transform.rotation = Quaternion.Euler(pitch, initialYaw + yaw, 0f);
    }
}
