using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform player;
    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MouseRotation();
    }

    private void MouseRotation()
    {
        float rotationX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float rotationY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= rotationY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * rotationX);
    }
}
