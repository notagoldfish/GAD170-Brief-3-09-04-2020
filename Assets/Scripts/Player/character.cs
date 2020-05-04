using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
{
    public float speed = 12.0f;
    public float rotateSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float maxHealth = 100f; //Here I Am
    public float currentHealth = 100f; //Here I Am
    public Transform respawnLocation;

    //HealthBar and Experience Bar and Text
    public HealthBar healthBar;
    public ExperienceBar experienceBar;
    public GameObject levelText;


    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private int jumps;

    //Shooting
    public float damage = 10f;
    public float range = 100f;
    public KeyCode shootKey;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    //Experience Variables
    public int currentXP = 0;
    public int nextLevelXP = 100;
    public int currentLevel = 1;
    public int nextLevelPercentIncrease = 10;
    public int levelUpIncreases = 1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth; //Here I Am
        //Healthbar Setting
        healthBar.SetMaxHealth(maxHealth);
        experienceBar.SetMaxExperience(nextLevelXP);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }
            jumps = 0;
        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
            if (Input.GetKeyDown(KeyCode.Space) && jumps < 1)
            {
                moveDirection.y = jumpSpeed;
                jumps++;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (currentHealth <= 0)
        {
            Die();
        }

        if ((Input.GetKey(shootKey)) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green);


        //Debug.Log("Current Health is " + currentHealth);  //Here I Am
        if (Input.GetKeyDown(KeyCode.H))   //Here I Am
        {
            currentHealth = currentHealth - 10;
        }
        //Debug.Log("Current XP is " + currentXP);

        if (currentXP >= nextLevelXP)
        {
            LevelUp();
        }

        //Set HealthBar and Experience Bar
        healthBar.SetHealth(currentHealth);
        experienceBar.SetExperience(currentXP);

        //Healthbar and Experience Bar Setting
        healthBar.SetMaxHealth(maxHealth);
        experienceBar.SetMaxExperience(nextLevelXP);

        //Set Experience Level Text
        GameObject.Find("LevelText").GetComponent<Text>().text = currentLevel.ToString();

    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject plImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(plImpactGO, 1f);
        }
    }

    private void OnTriggerStay(Collider collision)  //Here I Am
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth = currentHealth + 10;
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Void"))
        {
            currentHealth = -1;
            //Debug.Log("Fell");
        }
        if (collision.gameObject.CompareTag("Experience"))
        {
            currentXP = currentXP + Random.Range(10, 20);
            Destroy(collision.gameObject);
        }
    }

    void LevelUp ()
    {
        currentLevel++;
        maxHealth = maxHealth * 1 + levelUpIncreases;
        currentHealth = maxHealth;
        damage = damage * 1f + levelUpIncreases/5f;
        nextLevelXP = nextLevelXP + (nextLevelXP * 1 + nextLevelPercentIncrease);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //Debug.Log("TakeDamage is happening");
        //Debug.Log(amount);
    }

    private void Die()
    {
        transform.position = new Vector3(respawnLocation.transform.position.x, respawnLocation.transform.position.y, respawnLocation.transform.position.z);
        currentHealth = maxHealth;
    }
}

