// using System;
// using UnityEngine;
//
// public class Sword : MonoBehaviour, WeaponInterface
// {
//     private PlayerMove playerMove;
//     public int slashDamage;
//     public float timeBetweenAttacks;
//     private bool readyToAttack = true;
//     private bool active;
//     private Renderer rend;
//     public Animator animator;
//     public Transform attackPoint;
//     public float attackRange = 0.5f;
//     public LayerMask enemyLayers;
//     
//
//     private void Awake()
//     {
//         active = false;
//     }
//
//     private void Start()
//     {
//         playerMove = transform.root.GetComponent<PlayerMove>();
//         rend = GetComponent<Renderer>();
//         rend.enabled = false;
//     }
//     
//     private void Update()
//     {
//         if (playerMove.fire && readyToAttack && active)
//         {
//             Attack();
//             playerMove.fire = false;
//         }
//     }
//     
//     public void Attack()
//     {
//         if (!active) return;
//         animator.SetTrigger("Attack");
//         readyToAttack = false;
//         Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
//         foreach (Collider enemy in enemiesHit)
//         {
//             Debug.Log("We hit " + enemy.name);
//             enemy.GetComponent<Enemy>().TakeDamage(slashDamage);
//         }
//         {
//             
//         }
//         // Perform raycasting for hit detection
//         // RaycastHit hit;
//         //
//         // if (Physics.Raycast(transform.position, transform.forward, out hit))
//         // {
//         //     // Check if the hit object has an Enemy script
//         //     Enemy enemy = hit.collider.GetComponent<Enemy>();
//         //
//         //     if (enemy != null)
//         //     {
//         //         // Apply damage to the enemy (you can pass the damage amount as needed)
//         //         enemy.TakeDamage(slashDamage); // Example damage amount
//         //     }
//         // }
//         
//         Invoke("ResetAttack", timeBetweenAttacks);
//     }
//     
//     void OnDrawGizmosSelected()
//     {
//         if (attackPoint == null) return;
//         Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//     }
//     
//     void ResetAttack()
//     {
//         readyToAttack = true;
//     }
//     
//     public void SetActive(bool active)
//     {
//         this.active = active;
//         if (this.active)
//         {
//             rend.enabled = true;
//         }
//         else
//         {
//             rend.enabled = false;
//         }
//     }
// }
