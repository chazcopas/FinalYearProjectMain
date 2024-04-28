// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class TimeTravel : MonoBehaviour
// {
//     private bool isTimeTravelling = false;
//     
//     public GameObject gun;
//     public GameObject sword;
//
//     [System.Serializable]
//     public class ObjectMaterials
//     {
//         public GameObject gameObject;
//         public Material pastMaterial;
//         public Material presentMaterial;
//     }
//     
//     public List<ObjectMaterials> objectsToChange;
//
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             InitiateTimeTravel();
//         }
//     }
//     
//     void InitiateTimeTravel()
//     {
//         isTimeTravelling = !isTimeTravelling;
//         foreach (ObjectMaterials objMat in objectsToChange)
//         {
//             SetMaterials(objMat);
//         }
//
//         SetWeapon();
//         Debug.Log("Time Travel " + isTimeTravelling);
//     }
//     
//     void SetMaterials(ObjectMaterials objMat)
//     {
//         if (isTimeTravelling)
//         {
//             objMat.gameObject.GetComponent<Renderer>().material = objMat.pastMaterial;
//         }
//         else
//         {
//             objMat.gameObject.GetComponent<Renderer>().material = objMat.presentMaterial;
//         }
//     }
//     void SetWeapon()
//     {
//         WeaponInterface gunScript = gun.GetComponent<WeaponInterface>();
//         WeaponInterface swordScript = sword.GetComponent<WeaponInterface>();
//         if (isTimeTravelling)
//         {
//             switchToSword(swordScript, gunScript);
//         }
//         else
//         {
//             switchToGun(swordScript, gunScript);
//         }
//     }
//     void switchToSword(WeaponInterface sword, WeaponInterface gun)
//     {
//         sword.SetActive(true);
//         gun.SetActive(false);
//     }
//     
//     void switchToGun(WeaponInterface sword, WeaponInterface gun)
//     {
//         gun.SetActive(true);
//         sword.SetActive(false);
//     }
// }
