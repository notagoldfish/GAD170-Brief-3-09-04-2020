using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public float speed = 12.0f;
    public float rotateSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float maxHealth = 10.0f; //Here I Am
    //public float playerHealth = 10.0f; 
    public float currentHealth = 10.0f; //Here I Am
    public Transform respawnLocation;
    

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

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Current Health is " + currentHealth);  //Here I Am
        if (Input.GetKeyDown(KeyCode.H))   //Here I Am
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
        if (collision.gameObject.CompareTag("Void"))
        {
            currentHealth = -1;
            Debug.Log("Fell");
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public void Die()
    {
        transform.position = new Vector3(respawnLocation.transform.position.x, respawnLocation.transform.position.y, respawnLocation.transform.position.z);
        currentHealth = maxHealth;
    }
}

