using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Video: https://www.youtube.com/watch?v=LDcIDUAXVFU

public class EnemyAI : MonoBehaviour
{

    Transform playerTransform;
    UnityEngine.AI.NavMeshAgent myNavmesh;
    public float checkRate = 0.01f;
    float nextCheck;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        myNavmesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
      //  myNavmesh.transform.LookAt(playerTransform);
        myNavmesh.destination = playerTransform.position;
    }

}
