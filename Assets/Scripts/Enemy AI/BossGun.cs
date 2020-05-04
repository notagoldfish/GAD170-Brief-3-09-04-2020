using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{

    public float damage;
    public float range;
    public KeyCode interactKey; //Allows for changing the interact button in the editor
    public float fireRate;
    public ParticleSystem muzzleFlashOne;
    public ParticleSystem muzzleFlashTwo;
    public ParticleSystem muzzleFlashThree;
    public ParticleSystem muzzleFlashFour;
    public ParticleSystem muzzleFlashFive;
    public GameObject impactEffect;
    Transform target;
    public float viewCone;
    private float shootAmount = 5;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;

    }

    void Update()
    {
        //Line of Sight
        float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
        if (dot > viewCone)
        {
            if (Time.time >= nextTimeToFire && Vector3.Distance(target.transform.position, this.transform.position) <= range)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                while(shootAmount > 0)
                {
                    Shoot();
                    shootAmount--;
                    Debug.Log("I Shot!");
                }
                shootAmount = 5;
            }
            //Debug.Log("Quite facing");
        }

    }

    void Shoot()
    {
        muzzleFlashOne.Play();
        muzzleFlashTwo.Play();
        muzzleFlashThree.Play();
        muzzleFlashFour.Play();
        muzzleFlashFive.Play();

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            character target = hit.transform.GetComponent<character>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

        }
    }
}

