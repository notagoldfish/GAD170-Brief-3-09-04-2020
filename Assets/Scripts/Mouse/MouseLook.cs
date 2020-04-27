using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float rotX;
    public float rotY;
    public float rotZ;
    public float rotationSpeed;
    void Start()
    {

    }

    void Update()
    {

        rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed; //speed of mouse camera
        rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed; //speed of mouse camera

        if (rotX < -90)
        {
            rotX = -90;
        }
        else if (rotX > 90) //will stop constant rotation of camera when looking down
        {
            rotX = 90;
        }

        transform.rotation = Quaternion.Euler(0, rotY, 0);
        GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }
}