using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; // Import if necessary

public class ProjectileWeapon : Weapon
{
    public SpinDamager spinDamager;
    public Projectile projectile;
    
    private float shotCounter;
    
    public float weaponRange;
    public LayerMask whatIsEnemy;

    public static ProjectileWeapon instance;
    
    void Start()
    {
        SetStats();
        instance = this;
    }
    
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            shotCounter = stats[weaponLevel].timeBetweenAttacks;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            if(enemies.Length > 0)
            {
                for(int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                    
                    Vector3 direction = (targetPosition - transform.position).normalized; 
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
                    Projectile newProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
                    newProjectile.gameObject.SetActive(true);
                    newProjectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectile.moveSpeed;
                }
            }

        }

     
    }

  
    
    void SetStats()
    {
        spinDamager.damageAmount = stats[weaponLevel].damage;
        spinDamager.lifeTime = stats[weaponLevel].duration;
        spinDamager.timeBetweenDamage = stats[weaponLevel].timeBetweenAttacks;
        
        spinDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        
        shotCounter = 0f;
        
        projectile.moveSpeed = stats[weaponLevel].speed;

    }
}
