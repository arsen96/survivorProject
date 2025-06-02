
// Start of Selection
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
    
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        instance = this;
    }
    
    // Update is called once per frame
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
                    
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    // projectile.transform.localScale = Vector3.one * 2;
                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
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
