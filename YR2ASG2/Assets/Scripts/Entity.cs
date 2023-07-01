/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The Entity class is used for enemy AI
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    /// <summary>
    /// enemy starting health
    /// </summary>
    int layerMask = 1 << 7;
    [SerializeField] private float StartingHealth;

    /// <summary>
    /// navmesh enemy ai
    /// </summary>
    public NavMeshAgent agent;

    /// <summary>
    /// player
    /// </summary>
    public Transform player;

    /// <summary>
    /// layermask for enemy to walk on ground and detect player
    /// </summary>
    public LayerMask whatIsGround, whatIsPlayer;

    /// <summary>
    /// enemy shoot and attack
    /// </summary>
    [SerializeField] private float timer = 5;
    private float bulletTime;
    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;

    /// <summary>
    /// patrolling 
    /// </summary>
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    /// <summary>
    /// attacking
    /// </summary>
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    /// <summary>
    /// check range for enemies
    /// </summary>
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    /// <summary>
    /// health
    /// </summary>
    private float health;

    /// <summary>
    /// detect player
    /// </summary>
    public TriggerPlayer triggerRange;
    public TriggerPlayer attackTrigger;

    /// <summary>
    /// on play get enemies
    /// </summary>
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        //check for sight & attack range
        playerInSightRange = triggerRange.playerInRange;
        playerInAttackRange = attackTrigger.playerInRange;
        Debug.Log("in sight : " + playerInSightRange + ", in attack : " + playerInAttackRange);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    /// <summary>
    /// patrol around when player not in sight
    /// </summary>
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    /// <summary>
    /// search for a walkpoint
    /// </summary>
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    /// <summary>
    /// chase when player in sight but not in attack range
    /// </summary>
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    /// <summary>
    /// attack player when in sight & attack range
    /// </summary>
    private void AttackPlayer()
    {
        //make sure enemy don't move
        agent.SetDestination(transform.position);

        //look at player
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //attack 
            ShootAtPlayer();
        }
    }

    /// <summary>
    /// attack player by shooting
    /// </summary>
    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce((player.transform.position - bulletRig.transform.position).normalized * 20f, ForceMode.Impulse);
        Destroy(bulletObj, 2f);
    }

    /// <summary>
    /// enemy health
    /// </summary>
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            Debug.Log(health);
        }
    }

    /// <summary>
    /// when collide with bullet tag enemy take 5 damage
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            TakeDamage(5);
        }
    }

    /// <summary>
    /// when enemy health = 0 will destroy enemy
    /// </summary>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        Health = StartingHealth;
    }


}