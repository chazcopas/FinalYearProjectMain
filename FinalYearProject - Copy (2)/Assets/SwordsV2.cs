using System;
using UnityEngine;

public class SwordsV2 : MonoBehaviour
{
    // sword stats
    public bool active;
    public float damage;
    public float attackRate;
    public float attackRange;
    public float attackCooldown;
    public float staminaCost;
    public bool isAttacking;
    public bool canAttack;
    public bool isBlocking;
    public bool canBlock;
    public Player player;
    
    // for sword animations
    private bool attack1;
    private bool attack2;
    
    public bool playerSword;

    public bool inPast;
    
    private void Awake()
    {
        // set sword states
        canAttack = true;
        canBlock = true;
        active = true;
        attack1 = true;
        attack2 = false;
    }

    private void Start()
    {
        // set sword to be inactive as player is spawned in present
        if (playerSword)
        {
            gameObject.SetActive(false);
        }

        Renderer rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    
    private void Update()
    {
        // if player uses left click, attack
        if (Input.GetMouseButtonDown(0) & canAttack)
        {
            isAttacking = true;
            if (playerSword)
            {
                Attack();
            }
        }
        // if player pressed right click down, start block
        if (Input.GetMouseButtonDown(1) & canBlock)
        {
            // Debug.Log("Blocking");
            isBlocking = true;
            if (playerSword)
            {
                Block();
            }
        }
        // if player releases right click, end block
        if (Input.GetMouseButtonUp(1) && isBlocking)
        {
            // Debug.Log("End Blocking");
            if (playerSword)
            {
                EndBlock();
            }
        }
    }
    
    // transitions to animation that ends the block and returns to idle and changes canBlock to true so player can block again
    // and is blocking to false
    public void EndBlock()
    {
        if (!active) return;
        if (isBlocking)
        {
            // Debug.Log("End Blocking");
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("EndBlock");
            canBlock = true;
            isBlocking = false;
        }
    }
    // transitions to animation that starts the block and changes canBlock to false so player cannot start block again while blocking
    public void Block()
    {
        if (!active) return;
        if (isBlocking)
        {
            // Debug.Log("Blocking");
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("StartBlock");
            canBlock = false;
            player.reduceStamina(staminaCost);
        }
    }

    // handles enemy attack with sword
    public void EnemyAttack()
    {
        // Debug.Log("Enemy Sword Attacking");
        Debug.Log(isAttacking);
        // check if enemy is attacking
        if (isAttacking)
        {
            // Debug.Log("Sword Attacking");
            Animator anim = GetComponent<Animator>();
            // tigger attack animation
            anim.SetTrigger("Attack");
            canAttack = false;
            // check for objects hit by sword
            Collider[] whosHit = Physics.OverlapSphere(transform.position, attackRange);
            // iterate through objects hit and check if any are enemies from opposite time period or if any are the player
            foreach (Collider enemy in whosHit)
            {
                // Debug.Log(gameObject + " hit " + enemy.name);
                if (enemy.CompareTag("Player"))
                {
                    player.TakeDamage(damage);
                }
                else if (enemy.CompareTag("Enemy"))
                {
                    // Debug.Log("Enemy taking damage");
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (enemy.CompareTag("EnemyPast"))
                {
                    // Debug.Log("Enemy Past taking damage");
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
        // reset attack after cooldown
        Invoke("ResetAttack", attackCooldown);
    }
    // handles player attack with sword
    public void Attack()
    {
        // Debug.Log("Sword Attacking");
        // checks if sword is active
        if (!active) return;
        // get player stamina
        int stamina = player.returnStamina();
        // check if player has enough stamina to attack
        if (isAttacking && stamina > 0)
        {
            // Debug.Log("Attacking");
            Animator anim = GetComponent<Animator>();
            // cycle through animations
            if (attack1)
            {
                anim.SetTrigger("Attack");
                attack1 = false;
                attack2 = true;
            }
            else if (attack2)
            {
                anim.SetTrigger("Attack2");
                attack2 = false;
                attack1 = true;
            }
            // reduce player stamina
            player.reduceStamina(staminaCost);
            canAttack = false;
            // check for objects hit by sword
            Collider[] enemiesHit = Physics.OverlapSphere(transform.position, attackRange);
            // iterate through objects hit and check if any are enemies and deal damage if they are
            foreach (Collider enemy in enemiesHit)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
                if (enemy.CompareTag("EnemyPast"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            Invoke("ResetAttack", attackCooldown);
        }
    }
    // reset attack after cooldown
    void ResetAttack()
    {
        // Debug.Log("Enemy Resetting attack");
        isAttacking = false;
        canAttack = true;
    }
    // upgrade sword stats
    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }
    public void ReduceStaminaCost(float amount)
    {
        staminaCost = staminaCost / amount;
    }
    public void ReduceTimeBetweenAttacks(float amount)
    {
        attackCooldown = attackCooldown / amount;
    }
}
