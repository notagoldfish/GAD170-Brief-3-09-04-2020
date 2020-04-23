using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copy of door

public class Lever : MonoBehaviour
{
    public void OnOff()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("OnOff");
    }
}
