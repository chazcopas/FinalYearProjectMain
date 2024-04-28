using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    // public float maxHealth;
    // private float currentHealth;
    // public GameObject healthBarUI;
    // public Slider slider;
    
    // weapon objects
    public GameObject gunObject;
    public GameObject swordObject;
    
    // weapon scripts
    public GunV2 gun;
    public SwordsV2 sword;
    
    // nav mesh agent for enemy movement
    public NavMeshAgent agent;
    // player object position
    public Transform player;
    // layers for ray casting and collision checks
    public LayerMask whatIsGround, whatIsPlayer, enemyLayer, enemyPastLayer;
    
    
    
    // enemy from past or present
    public bool fromPresent;
    // enemy currently in past or present
    public bool inPresent;
    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    
    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool enemyInSightRange, enemyInAttackRange;
    
    private void Awake()
    {
        // animator = GetComponent<Animator>();
        // check on creation if enemy is from present or past and activate the correct weapon
        if (fromPresent)
        {
            Debug.Log("Enemy from present gun set active");
            inPresent = true;
            gunObject.SetActive(true);
            swordObject.SetActive(false);
        }
        else
        {
            Debug.Log("Enemy from past sword set active");
            inPresent = false;
            gunObject.SetActive(false);
            swordObject.SetActive(true);
        }
        // store player object
        player = GameObject.Find("Player").transform;
        // store nav mesh agent
        agent = GetComponent<NavMeshAgent>();
        // currentHealth = maxHealth;
        // slider.value = CalculateHealth();
    }
    
    private void Update()
    {
        // slider.value = CalculateHealth();
        // if (currentHealth < maxHealth)
        // {
        //     healthBarUI.SetActive(true);
        // }
        // if (currentHealth <= 0.0)
        // {
        //     Destroy(gameObject);
        // }
        // if (currentHealth > maxHealth)
        // {
        //     currentHealth = maxHealth;
        // }
        
        // Check if player is in sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        // check an enemy, depending on time this enemy originates, is in sight and attack range
        if (fromPresent)
        {
            enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, enemyPastLayer );
            enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyPastLayer);
        }
        else
        {
            enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, enemyLayer);
            enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyLayer);
        }
        // check if player or enemy is in sight and attack range
        if ((!playerInSightRange && !playerInAttackRange) || (!enemyInSightRange && !enemyInAttackRange)) Patroling();
        if ((playerInSightRange && !playerInAttackRange) || (enemyInSightRange && !enemyInAttackRange)) ChasePlayer();
        if ((playerInSightRange && playerInAttackRange) || (enemyInSightRange && enemyInAttackRange)) AttackPlayer();
    }
    
    // has enemy moving about randomly in a set range
    private void Patroling()
    {
        // animator.SetTrigger("Running");
        GetComponent<NavMeshAgent>().speed = 3;
        if (!walkPointSet) SearchWalkPoint();
        
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    
    // finds a random point in a set range for the enemy to walk to
    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }
    
    // has enemy chase the player or another enemy
    private void ChasePlayer()
    {
        GetComponent<NavMeshAgent>().speed = 3;
        GameObject closestEnemy = FindClosestEnemy();
        agent.SetDestination(closestEnemy.transform.position);
    }
    
    // finds the closest enemy or player to the enemy this script is attached to
    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies;
        if (fromPresent)
        {
            // Debug.Log("Enemy from present " + name);
            enemies = GameObject.FindGameObjectsWithTag("EnemyPast");
        }
        else
        {   
            // Debug.Log("Enemy from past " + name);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        // find closest object to enemy, either player or another enemy from different time
        float currentClosestDistance = Vector3.Distance(player.position, transform.position);
        GameObject closestEnemy = player.gameObject;
        // iterate through all enemies and find the closest one
        foreach (GameObject enemy in enemies)
        {
            if (enemy != gameObject)
            {
                float distance = Vector3.Distance(enemy.transform.position, transform.position);
                float playerDistance = Vector3.Distance(player.position, transform.position);
                
                if (distance < currentClosestDistance)
                {
                    currentClosestDistance = distance;
                    closestEnemy = enemy;
                }
                if (playerDistance < currentClosestDistance)
                {
                    currentClosestDistance = playerDistance;
                    closestEnemy = player.gameObject;
                }
            }
        }
        // Debug.Log("Closest enemy: " + closestEnemy.name);
        return closestEnemy;
    }
    
    private void AttackPlayer()
    {
        // decrease movement speed
        if (inPresent)
        {
            GetComponent<NavMeshAgent>().speed = 2;
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 3;
        }
        // find closest enemy or player
        GameObject closestEnemy = FindClosestEnemy();
        // Debug.Log("Closest enemy: " + closestEnemy.name);
        
        agent.SetDestination(closestEnemy.transform.position);
        // look at the closeest enemy but only in the x and z axis
        Vector3 lookAtPoint = new Vector3(closestEnemy.transform.position.x, transform.position.y, closestEnemy.transform.position.z);
        transform.LookAt(lookAtPoint);
        // checks if the enemy has already attacked
        if (!alreadyAttacked)
        {
            // calls relevant attack function depending on if the enemy is in the present or past
            alreadyAttacked = true;
            if (inPresent)
            {
                gun.EnemyShoot();
            }
            else
            {
                Debug.Log("Enemy attacking with sword");
                sword.isAttacking = true;
                sword.EnemyAttack();
            }   
            // resets the attack after a set time
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    // changes the weapon the enemy is using and sets the enemy to be in the past or present
    public void TimeTravelled()
    {
        if (inPresent)
        {
            inPresent = false;
            gunObject.SetActive(false);
            swordObject.SetActive(true);
        }
        else
        {
            inPresent = true;
            gunObject.SetActive(true);
            swordObject.SetActive(false);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
