using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public float speed = 12.0f;
    public float rotateSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float maxHealth = 100f; //Here I Am
    private float currentHealth = 100f; //Here I Am

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private int jumps;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth; //Here I Am
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }
            jumps = 0;
        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
            if (Input.GetKeyDown(KeyCode.Space) && jumps < 1)
            {
                moveDirection.y = jumpSpeed;
                jumps++;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        Debug.Log("Current Health is " + currentHealth);  //Here I Am
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHealth = currentHealth - 10f;
        }
    }

    private void OnTriggerStay(Collider collision)  //Here I Am
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth = currentHealth + 10f;
                Destroy(collision.gameObject);
            }
        }
    }
}

