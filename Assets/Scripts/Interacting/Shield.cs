using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copy of door

public class Shield : MonoBehaviour
{
    public void ShrinkGrow()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("ShrinkGrow");
    }
}
