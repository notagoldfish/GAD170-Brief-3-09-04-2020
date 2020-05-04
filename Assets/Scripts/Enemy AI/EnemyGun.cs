using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public float damage;
    public float range;
    public KeyCode interactKey; //Allows for changing the interact button in the editor
    public float fireRate;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    Transform target;
    public float viewCone;

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
                Shoot();
            }
            ////Debug.Log("Quite facing");
        }

    }

    void Shoot ()
    {
        
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range))
        {
            ////Debug.Log(hit.transform.name);

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
