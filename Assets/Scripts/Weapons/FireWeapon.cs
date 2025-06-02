using UnityEngine;

public class FireWeapon : Weapon
{
    // public EnemySpinDamager spinDamager;
    private float shotCounter;

      private Transform target;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStats();

        target = PlayerHealthController.instance.transform;
    }

    // // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        //  if (target.position.x < transform.position.x)
        // {
        //     transform.rotation = Quaternion.Euler(0, 0, 0);
        // }
        // else
        // {
        //     transform.rotation = Quaternion.Euler(0, 180, 0);
        // }

    }

    void SetStats()
    {
        // spinDamager.damageAmount = stats[weaponLevel].damage;
        // spinDamager.lifeTime = stats[weaponLevel].duration;
        // spinDamager.timeBetweenDamage = stats[weaponLevel].timeBetweenAttacks;
        
        // spinDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        
        // shotCounter = 0f;
        // currentDuration = stats[weaponLevel].duration;
    }
}
