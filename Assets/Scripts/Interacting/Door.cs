using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using Video https://www.youtube.com/watch?v=nONlAXpCkag

public class Door : MonoBehaviour
{
    public void openClose() //Defines animator and then sets the trigger for what I've set for the animator to run
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("OpenClose");
    }
}
