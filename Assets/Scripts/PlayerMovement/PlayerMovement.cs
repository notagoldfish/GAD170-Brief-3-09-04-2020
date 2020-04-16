using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    // Player Movement Speed.
    public float movementSpeed;
    // Player Rotation Speed.
    public float rotationSpeed;
    // Upward velocity when user presses spacebar.
    public float jumpHeight = 5f;



    // Use this for initialization
    void Start()
    {

    }

    //Update is called once per frame
    void FixedUpdate()
    {
        // Left Shift (Sprint) 
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            // Left Shift (Sprint) = Player Movement Speed * 1.5 
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 1.5f;
        }
        // Left Shift (Sprint) will not work unless "w" is pressed
        else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
    }

    void Update()
    {

        // Check spacebar to trigger jumping. Checks if vertical velocity (eg velocity.y) is near to zero.
        if (Input.GetKey(KeyCode.Space) == true && Mathf.Abs(this.GetComponent<Rigidbody>().velocity.y) < 0.01f)
        {
            this.GetComponent<Rigidbody>().velocity += Vector3.up * this.jumpHeight;
        }
    }
}