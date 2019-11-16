using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour
{

    [SerializeField] private float mouseSensitivy = 1f;
    [SerializeField] private float mouseSensAdjustSpeed = 1f;
    public float MouseSpeed { get => Time.deltaTime * mouseSensitivy; }
    public float MouseSendAdjustSpeed { get => Time.deltaTime * mouseSensAdjustSpeed; }

    private Vector2 rotation = new Vector2();
    private Vector2 Rotation
    {
        get => rotation; set
        {
            rotation = value;
            rotation = new Vector2(Mathf.Clamp(rotation.x, -80, 80), rotation.y);
        }
    }

    void Start()
    {
        Rotation = transform.localEulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Rotation += new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * MouseSpeed;
        mouseSensitivy += Input.mouseScrollDelta.y * MouseSendAdjustSpeed;
        transform.localEulerAngles = Rotation;
    }
}
