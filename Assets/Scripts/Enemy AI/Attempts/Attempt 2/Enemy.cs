using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//video https://www.youtube.com/watch?v=_Z1t7MNk0c4

public class Enemy : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance) //if position is greater than stoppingdistance then move towards player
        {

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        } else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && (Vector3.Distance(transform.position, player.position) > retreatDistance))  // else if position is less than stoppingdistance and greater than retreat distance stay still
        {

            transform.position = this.transform.position;

        } else if (Vector3.Distance(transform.position, player.position) < retreatDistance) //else if position is less than retreatdistance move away from player
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }
}
