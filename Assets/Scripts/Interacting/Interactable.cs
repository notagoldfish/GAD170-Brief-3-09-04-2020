using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Using Video https://www.youtube.com/watch?v=cLzG1HDcM4s

public class Interactable : MonoBehaviour
{

    public bool isInRange; //True or false for whether the player is close enough to trigger something
    public KeyCode interactKey; //Allows for changing the interact button in the editor
    public UnityEvent interactAction; //This opens for the events in unity to be called, making it draggable for any object to be inserted
    
    void Update()
    {
        //Checks if it is in range and then if the interact key is pressed and if so then will initialise whatever trigger was put in the event action in the editor
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    //Simply triggers the bool true or false if the tagged player enters or exits the collider
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
