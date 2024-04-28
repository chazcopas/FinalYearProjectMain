using UnityEngine;

public class GunV2 : MonoBehaviour
{
    // gun shot audio
    public AudioSource gunShot;
    // gun stats
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public int magazineSize;
    public int bulletsPerTap;
    public int bulletsLeft;
    public int bulletsShot;
    public float damage;
    public bool allowButtonHold;
    // booleans for gun states
    bool shooting;
    bool readyToShoot;
    bool reloading;
    // camera for raycasting
    public Camera fpsCam;
    // bool for checking if it is player's gun
    public bool playerGun;
    
    public bool inPresent;
    // public Transform attackPoint;
    
    private void Awake()
    {
        // on creation set all variables to default values and set gun to ready to shoot
        inPresent = true;
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    
    private void Update()
    {
        // check if gun is automatic or single fire
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        
        // activates reload if R is pressed and there are bullets left in the magazine or if the magazine is empty
        if ((Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) || (bulletsLeft == 0 && !reloading))
        {
            Reload();
        }
        
        // shoot if ready to shoot, shooting and not reloading
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            // Debug.Log(playerGun);
            bulletsShot = 0;
            if (playerGun)
            {
                Shoot();
            }
        }
    }
    
    // starts reload function
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    // ends reload function
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
    
    // handles enemy shooting
    public void EnemyShoot()
    {
        readyToShoot = false;
        //loop to create multiple bullets/rays with one tap
        for (int i = 0; i < bulletsPerTap; i++)
        {
            if (bulletsLeft <= 0)
            {
                break;
            }

            // ray from center of enemy object
            Ray ray = new Ray(transform.position, transform.forward);
            // draw ray in same way to use as debug ray
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 10f);

            //Apply spread to the ray, doubles if moving
            
            ray.direction += new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
            // Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 10f);
            RaycastHit hit;
            // check if ray hits player or enemy and applies damage to relevant object
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Debug.Log("Enemy hit player");
                    hit.collider.GetComponent<Player>().TakeDamage(damage);
                }
                if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("EnemyPast"))
                {
                    hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                }
                
            }

            bulletsLeft--;
            bulletsShot++;
        }
        // cooldwon for shooting
        Invoke("ResetShot", timeBetweenShooting);
    }
    
    // handles player shooting
    private void Shoot()
    {
        gunShot.Play();
        // Debug.Log("PLayer Shooting");
        readyToShoot = false;
        //loop to create multiple bullets/rays with one tap
        for (int i = 0; i < bulletsPerTap; i++)
        {
            // Debug.Log("Shooting bullet");
            if (bulletsLeft <= 0)
            {
                break;
            }

            // ray from center of screen
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            // draw ray in same way to use as debug ray
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 10f);

            //Apply spread to the ray, doubles if moving
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Vector3 spreadDirection = new Vector3(Random.Range(-spread * 2, spread * 2), Random.Range(-spread * 2, spread * 2), 0);
                // Debug.Log("Spread direction: " + spreadDirection);
                ray.direction += spreadDirection;
            }
            else
            {
                Vector3 spreadDirection = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
                // Debug.Log("Spread direction: " + spreadDirection);
                ray.direction += spreadDirection;
            }
            // Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 10f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // check if ray hits enemy and apply damage
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                }
                if (hit.collider.CompareTag("EnemyPast"))
                {
                    hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            bulletsLeft--;
            bulletsShot++;
        }
        Invoke("ResetShot", timeBetweenShooting);
    }
    
    private void ResetShot()
    {
        readyToShoot = true;
    }
    
    // upgrade functions
    public void IncreaseDamage(int amount)
    {
        Debug.Log("Increasing damage");
        damage += amount;
    }
    public void ReduceSpread(float amount)
    {
        spread = spread / amount;
    }
    public void ReduceReloadTime(float amount)
    {
        Debug.Log(reloadTime);
        reloadTime = reloadTime / amount;
        Debug.Log("new " + reloadTime);
    }
}
