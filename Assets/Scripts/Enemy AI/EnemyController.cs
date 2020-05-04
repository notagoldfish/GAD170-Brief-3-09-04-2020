using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Videos Used: https://www.youtube.com/watch?v=xppompv1DBg | https://www.youtube.com/watch?v=xppompv1DBg | https://www.youtube.com/watch?v=_Z1t7MNk0c4 | https://www.youtube.com/watch?v=mxd5hwfN6iw

public class EnemyController : MonoBehaviour
{
    //Defines
    ///Follow Player Defines
    ////Follow Speed, stopping distance and such are defined in the NavMesh
    public float lookRadius;
    public float unsafeDistance;
    public float retreatSpeed;

    Transform target;
    NavMeshAgent agent;

    ///Stat Defines
    public float health;

    ///Wander Defines
    Transform myTransform;
    public float wanderRange;
    NavMeshHit navHit;
    Vector3 wanderTarget;
    bool isOnRoute = false;

    //Percentage Chance for Heart to Drop
    public float chanceToDrop = 100;
    public Transform Heart;

    //Sets up the variable for random number gen
    private float randomDropHeart = 0;

    //Bool for whether a heart has had the chance to drop
    private bool hasDropped = false;

    //For Wander, Sets Initial References. Could be done better and with less fidley code but this is how the youtube video did it
    private void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {
        //Locates player object and defines the agent (where the enemy can go without falling off the edge)
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        randomDropHeart = Random.Range(0f, 100f);
    }

    void Update()
    {
        //Checks if player is in the scene, if not will stop moving.
        if (PlayerManager.instance.player != null)
        {
            //Gets location of player
            float distance = Vector3.Distance(target.position, transform.position);

            //If distance is less than the lookRadius, then if distance is greater than the unsafeDistance the path is set towards the player, 
            //if the distance is less than the unsafeDistance then the path is set to the player but with negative speed to move the enemy in the opposite direction
            if (distance <= lookRadius)
            {
                isOnRoute = true;
                if (distance >= unsafeDistance)
                {
                    agent.SetDestination(target.position);
                }
                else if (distance < unsafeDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, -retreatSpeed * Time.deltaTime);
                }
                if (distance <= agent.stoppingDistance)
                {
                    //Shoot Target Script (not implemented yet)

                    //Activates FaceTarget
                    FaceTarget();
                }
            } else
            {
                isOnRoute = false;
            }
            //if health reaches 0 activate die thing (got from https://answers.unity.com/questions/598350/delay-destroygameobject.html)
            if (health <= 0)
            {
                Die();
            }
            //Test Reduce Health - MUST BE REMOVED BEFORE COMPLETING GAME, IS BEING LEFT TO BE ABLE TO TEST THE DEATH FUNCTION WITHOUT NEED FOR THE PLAYER'S ATTACK
            if (Input.GetKey(KeyCode.H))
            {
                health = health - health;
            }
        }
        //Initialise CheckIfIShouldWander
        CheckIfIShouldWander();

    }

    //Take Damage by amount from the gun of the player
    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    //Makes Face Target, research more into this because I don't quite understand the Quaternion part
    void FaceTarget()
    {
        Vector3 direction = target.position - transform.position; //
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Destroy Object, change colour, explode or ragdoll. Possibly determine experience
    void Die()
    {
        //Change Colour? Explode? Ragdoll?
        //Determine Experience?

        //Check chances and whether a heart has already been spawned, then instantiate a heart prefab in the position of the enemy killed.
        //However for some NONSENSICAL reason when I only lightly tap the "L" button, from the void update, then this if statement won't run, but the destroy game object will. WHICH MAKES NO SENSE AT ALL
        if ((randomDropHeart <= chanceToDrop) && (hasDropped == false))
        {
            Instantiate(Heart, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            hasDropped = true;
            Debug.Log("Dropped");
        }
        Destroy(gameObject, 1f);
    }

    //Set myTransform as transform
    void SetInitialReferences()
    {
        myTransform = transform;
    }

    //Checks if the distance from player is larger than lookRadius and whether they are currently walking somewhere, then if all is good they walk towards the RandomWanderTarget.
    void CheckIfIShouldWander()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > lookRadius && !isOnRoute)
        {
            if (RandomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
            {
                agent.SetDestination(wanderTarget);
                isOnRoute = true;
            } else
            {
                isOnRoute = false;
            }
        }
    }

    //Sets the RandomWanderTarget on the NavMesh region by finding a point in a sphere, if that point is accessible by the NavMesh then it will return as true and allow the travel to the location
    //If not then it will result the location as being where the object already is.
    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 0.5f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        } else
        {
            result = centre;
            return false;
        }
    }

    //This draws the radius in the editor so we know how far they can see
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, unsafeDistance);
    }
}



///With wander should set up a timer so that it doesn't swap a destination when it is already moving
///Some locations on the nav-mesh don't work with wander. I've got no idea why????