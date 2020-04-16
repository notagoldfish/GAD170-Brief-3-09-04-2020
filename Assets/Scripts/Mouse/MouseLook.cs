using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Mouse Camera Rotation X axis
    public float rotX;
    //Mouse Camera Rotation Y axis
    public float rotY;
    //Mouse Camera Rotation Z axis
    public float rotZ;
    // Player Rotation Speed
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            //Rotation speed / Mouse Sensitivity
            rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
            rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

            if (rotX < -90)
            {
                rotX = -90;
            }
            else if (rotX > 90)
            {
                rotX = 90;
            }

            transform.rotation = Quaternion.Euler(0, rotY, 0);
            GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);


        }
    }
}



