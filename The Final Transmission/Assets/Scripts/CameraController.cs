using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 2.0f;
    [SerializeField] GameObject chair;
    [SerializeField] float chairSpinSpeed = 2.0f;
    Transform originalTransform;
    Transform chairOriginalTransform;
    float pitch = 0f;
    float yaw = 0f;
    float chairYaw = 0f;
   
    float initialYaw;
    float chairInitialYaw;

    public bool lockCamera = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalTransform = transform;
        initialYaw = transform.eulerAngles.y;

        chairOriginalTransform = chair.transform;
        chairInitialYaw = chair.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(lockCamera) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;

        //yaw = Mathf.Clamp(yaw, chair.transform.eulerAngles.y - 100f, chair.transform.eulerAngles.y + 100f); // has an issue when it gets to 360 degrees rotation where it jumps
        yaw = Mathf.Clamp(yaw, -100f, 100f);
        pitch = Mathf.Clamp(pitch, -60f, 80f);

        transform.rotation = Quaternion.Euler(pitch, initialYaw + yaw, 0f);

        if(Input.GetKey(KeyCode.E))
        {
            chairYaw += chairSpinSpeed;
            chair.transform.rotation = Quaternion.Euler(0, chairYaw, 0);
        }
        if(Input.GetKey(KeyCode.Q))
        {
            chairYaw -= chairSpinSpeed;
            chair.transform.rotation = Quaternion.Euler(0, chairYaw, 0);;
        }
     
    }
}
