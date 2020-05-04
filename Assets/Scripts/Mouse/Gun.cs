using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public KeyCode shootKey;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    void Update()
    {

        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }

        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green);

    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject plImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(plImpactGO, 1f);
        }
    }
}
