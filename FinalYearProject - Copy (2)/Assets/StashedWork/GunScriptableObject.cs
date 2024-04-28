// using UnityEngine;
// using UnityEngine.Pool;
//
// [CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]
// public class GunScriptableObject : ScriptableObject
// {
//     public GunType Type;
//     public string Name;
//     public GameObject ModelPrefab;
//     public Vector3 SpawnPosition;
//     public Vector3 SpawnRotation;
//     
//     public ShootConfigurationScripatableObject ShootConfig;
//     public TrailConfigScriptableObject TrailConfig;
//     
//     private MonoBehaviour ActiveMonoBehaviour;
//     private GameObject Model;
//     private float LastShootTime;
//     private ParticleSystem ShootSystem;
//     private ObjectPool<TrailRenderer> TrailPool;
//     
//     public void Spawn(Transform parent, MonoBehaviour monoBehaviour)
//     {
//         this.ActiveMonoBehaviour = ActiveMonoBehaviour;
//         LastShootTime = 0;
//         // TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);
//         Model = Instantiate(ModelPrefab);
//         Model.transform.SetParent(parent,false);
//         Model.transform.localPosition = SpawnPosition;
//         Model.transform.localRotation = Quaternion.Euler(SpawnRotation);
//         ShootSystem = Model.GetComponentInChildren<ParticleSystem>();
//     }
//     
//     // finish this after the report and time travel basic is done
// }
