using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public float damage = 100f;
    public float range = 100f;
    public KeyCode interactKey; //Allows for changing the interact button in the editor

    public ParticleSystem muzzleFlash;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Shoot();
        }
    }

    void Shoot ()
    {

        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
