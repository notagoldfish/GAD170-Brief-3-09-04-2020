﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    //create projectile
    //place projectile into resources folder
    //add rigidbody to projectile/prefab

    public GameObject prefab;



    void Start()
    {
        prefab = Resources.Load("projectile") as GameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(prefab) as GameObject;
            projectile.transform.position = transform.position + Camera.main.transform.forward * 2;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 50;
            Destroy(projectile, 05f);


        }



    }
}
