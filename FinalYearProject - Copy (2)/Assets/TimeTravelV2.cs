using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TimeTravelV2 : MonoBehaviour
{
    // bools for deciding if the player is time travelling and if they are in the present as well as if time travel is ready
    private bool isTimeTravelling = false;
    private bool inPresent = true;
    private bool timeTravelReady;
    // player object to move
    public GameObject playerObj;
    public float enemyRadius;
    public float playerRadius;
    // damage stats
    public float timeTravelPlayerDamage;
    public float timeTravelEnemyDamage;
    // size of the bubble around the player for time travel
    public float bubbleRadius;
    // layer mask for collisions when checking if objects can be placed after time travel
    public LayerMask collisionLayer;
    // distance to move player and enemies randomly if they cannot be placed at first position
    public float maxMoveDistance;
    public float moveDistance;
    // time travel cooldown time
    public float timeTravelCooldown;
    public int reduceBy;
    public float timeLeft;
    public GameObject playerCapsule;
    
    //player weapons to disable and activate depending on time period
    public GameObject gun;
    public GameObject sword;
    
    //text to display time travel cooldown
    public TMP_Text timeTravelText;

    private void Start()
    {
        // set time travel to be ready at the start of each level
        timeTravelReady = true;
        timeTravelText.text = "Ready to Time Travel (T)";
    }

    private void Update()
    {
        bool grounded = playerObj.GetComponent<PlayerMove>().IsGrounded();
        // check if T is pressed and time travel is ready
        if (Input.GetKeyDown(KeyCode.T) && timeTravelReady)
        {
            Debug.Log("Time Travel initiated");
            // initiate time travel
            InitiateTimeTravel();
        }
    }
    
    void InitiateTimeTravel()
    {
        // Debug.Log("pre travel position: " + playerObj.transform.position);
        isTimeTravelling = false;
        float newHeight;
        // check if player is in present or past and set new height accordingly
        if (inPresent)
        {
            newHeight = playerObj.transform.position.y + moveDistance + 1;
            gun.SetActive(false);
            sword.SetActive(true);
        }
        else
        {
            newHeight = playerObj.transform.position.y - moveDistance +2;
            gun.SetActive(true);
            sword.SetActive(false);
        }
        Vector3 newPlayerPosition = new Vector3(playerObj.transform.position.x, newHeight, playerObj.transform.position.z);
        // draw a capsule around the player object to check for collisions
        
        if (Physics.OverlapCapsule(newPlayerPosition, newPlayerPosition, playerRadius, collisionLayer).Length == 0)
        {
            InitiateEnemyTimeTravel();
            // Debug.Log("Player will be moved");
            Debug.Log(playerObj.transform.position);
            playerObj.transform.position = newPlayerPosition;
            Debug.Log(playerObj.transform.position);
            dealPlayerTimeTravelDamage();
        }
        else
        {
            // find random position along x and z axis within maxMoveDistance where the player can be placed
            Vector3 randomPosition = new Vector3(Random.Range(-maxMoveDistance, maxMoveDistance), newHeight, Random.Range(-maxMoveDistance, maxMoveDistance));
            if (Physics.OverlapCapsule(randomPosition, randomPosition, playerRadius, collisionLayer).Length == 0)
            {
                dealPlayerTimeTravelDamage();
                InitiateEnemyTimeTravel();
                // Debug.Log("Player will be moved randomly");
                Debug.Log(playerObj.transform.position);
                playerObj.transform.position = newPlayerPosition;
                Debug.Log(playerObj.transform.position);
            }
            else
            {
                // Debug.Log("Player could not be placed after time travel");
                return;
            }
        }
        // Debug.Log("post travel position: " + playerObj.transform.position);
        // start counter that will display the time travel cooldown
        timeTravelReady = false;
        inPresent = !inPresent;
        StartCoroutine(TimeTravelCooldown());
        // Invoke("ResetTimeTravel", timeTravelCooldown);
    }

    IEnumerator TimeTravelCooldown()
    {
        // Debug.Log("Time travel cooldown initiated"); 
        // Debug.Log(timeTravelReady);
        float storeTimeCooldown = timeTravelCooldown;
        // decrease time travel cooldown by 1 every second
        while (timeTravelCooldown > 0)
        {
            timeLeft = timeTravelCooldown;
            String timeLeftString = timeLeft.ToString();
            // Debug.Log(timeLeftString);
            timeTravelText.text = timeLeftString;
            // Debug.Log("Time left: " + timeLeft);
            yield return new WaitForSeconds(1);
            timeTravelCooldown -= 1;
        }
        timeTravelCooldown = storeTimeCooldown;
        Debug.Log(timeTravelCooldown);
        ResetTimeTravel();
    }
    // reset time travel after cooldown
    void ResetTimeTravel()
    {
        timeTravelText.text = "Ready to Time Travel";
        timeTravelReady = true;
    }
    
    void ReduceTimeTravelCooldown(int reduceBy)
    {
        timeTravelCooldown -= reduceBy;
    }
     
    void dealEnemyTimeTravelDamage(GameObject enemy)
    {
        // deal damage to enemy
        Debug.Log("Enemy taking damage");
        enemy.GetComponent<Enemy>().TakeDamage(timeTravelEnemyDamage);
    }
    
    void dealPlayerTimeTravelDamage()
    {
        // deal damage to player
        playerCapsule.GetComponent<Player>().TakeDamage(timeTravelPlayerDamage);
    }
    
    void InitiateEnemyTimeTravel()
    {
        // find all enemies with the tags enemy and enemypast and collect them into one array
        GameObject[] enemiesPast = GameObject.FindGameObjectsWithTag("EnemyPast");
        GameObject[] enemiesPresent = GameObject.FindGameObjectsWithTag("Enemy");
        
        GameObject[] allEnemies = new GameObject[enemiesPast.Length + enemiesPresent.Length];
        enemiesPast.CopyTo(allEnemies, 0);
        enemiesPresent.CopyTo(allEnemies, enemiesPast.Length);
        
        // Debug.Log(allEnemies.Length);
        // foreach (GameObject enemies in allEnemies)
        // {
        //     Debug.Log(enemies.name);
        // }
        
        foreach (GameObject enemy in allEnemies)
        {
            NavMeshAgent navMeshAgent = enemy.GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
            float newEnemyHeight;
            float distance = Vector3.Distance(playerObj.transform.position, enemy.transform.position);
            // Debug.Log(distance);       
            if (distance <= bubbleRadius)
            {
                // Debug.Log("enemy will be moved " + enemy.name);
                if (inPresent)
                {
                    newEnemyHeight = enemy.transform.position.y + moveDistance;
                }
                else
                {
                    newEnemyHeight = enemy.transform.position.y - (moveDistance-3);
                }
                Vector3 newEnemyPosition = new Vector3(enemy.transform.position.x, newEnemyHeight, enemy.transform.position.z);
                if (Physics.OverlapCapsule(newEnemyPosition, newEnemyPosition, enemyRadius, collisionLayer).Length == 0)
                {
                    enemy.transform.position = newEnemyPosition;
                    enemy.GetComponent<EnemyAI>().TimeTravelled();
                    dealEnemyTimeTravelDamage(enemy);
                }
                else
                {
                    // find random position along x and z axis within maxMoveDistance wheere the enemy can be placed
                    Vector3 randomPosition = new Vector3(Random.Range(-maxMoveDistance, maxMoveDistance), newEnemyHeight, Random.Range(-maxMoveDistance, maxMoveDistance));
                    if (Physics.OverlapCapsule(randomPosition, randomPosition, enemyRadius, collisionLayer).Length == 0)
                    {
                        enemy.transform.position = randomPosition;
                        enemy.GetComponent<EnemyAI>().TimeTravelled();
                        dealEnemyTimeTravelDamage(enemy);
                    }
                    else
                    {
                        Debug.Log("Enemy could not be placed after time travel");
                        Destroy(enemy);
                    }
                }
            }
            navMeshAgent.enabled = true;
        }
    }
    
    //upgrade methods
    public void ReduceTimeTravelCooldownPeriod(float reduceBy)
    {
        timeTravelCooldown = timeTravelCooldown / reduceBy;
    }
    
    public void ReducePlayerTimeTravelDamage(float reduceBy)
    {
        timeTravelPlayerDamage = timeTravelPlayerDamage / reduceBy;
        timeTravelEnemyDamage = timeTravelEnemyDamage / reduceBy;
    }
    
    public bool InPresentQuery()
    {
        return inPresent;
    }
}
