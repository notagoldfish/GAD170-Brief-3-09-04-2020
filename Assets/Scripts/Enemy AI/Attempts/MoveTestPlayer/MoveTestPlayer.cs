using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTestPlayer : MonoBehaviour
{
    public float moveSpeed = 1f;

    
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
    }
}
