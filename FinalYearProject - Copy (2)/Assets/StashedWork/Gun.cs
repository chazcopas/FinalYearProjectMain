// using System;
// using UnityEngine;
//
// public class Gun : MonoBehaviour, WeaponInterface
// {
//     private PlayerMove _playerMove;
//     [SerializeField]
//     private GameObject bulletPreFab;
//     [SerializeField]
//     private GameObject bulletPoint;
//     [SerializeField]
//     private float bulletSpeed = 10000000f;
//     [SerializeField]
//     private float bulletLife = 1f; 
//     public ParticleSystem muzzleFlash;
//     private bool active;
//     public GameObject gunMagazine;
//     private Renderer rend;
//     private Renderer rendChild;
//
//     private void Awake()
//     {
//         muzzleFlash.Stop();
//         active = true;
//     }
//
//     private void Start()
//     {
//         _playerMove = transform.root.GetComponent<PlayerMove>();
//         rend = GetComponent<Renderer>();
//         rendChild= gunMagazine.GetComponent<Renderer>();
//         rend.enabled = true;
//         rendChild.enabled = true;
//     }
//
//     private void Update()
//     {
//         if (_playerMove.fire && active)
//         {
//             Attack();
//             _playerMove.fire = false;
//         }
//     }
//
//     public void Attack()
//     {
//         if (!active) return;
//         muzzleFlash.Play();
//         // Perform raycasting for hit detection
//         RaycastHit hit;
//
//         if (Physics.Raycast(transform.position, transform.forward, out hit))
//         {
//             // Check if the hit object has an Enemy script
//             Enemy enemy = hit.collider.GetComponent<Enemy>();
//
//             if (enemy != null)
//             {
//                 // Apply damage to the enemy (you can pass the damage amount as needed)
//                 enemy.TakeDamage(10); // Example damage amount
//             }
//         }
//     }
//
//
//     
//     public void SetActive(bool active)
//     {
//         this.active = active;
//         Debug.Log("Gun active: " + this.active);
//         if (this.active)
//         {
//             rend.enabled = true;
//             rendChild.enabled = true;
//         }
//         else
//         {
//             rend.enabled = false;
//             rendChild.enabled = false;
//         }
//     }
// }
