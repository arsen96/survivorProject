using UnityEngine;

public class ZoneWeapon : Weapon
{

    public SpinDamager spinDamager;

    private float spawnCounter, spawnTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(statsUpdated == true){
            statsUpdated = false;
            SetStats();
        }

    
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0){
            spawnCounter = spawnTime;
            Instantiate(spinDamager, spinDamager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
        
        
    }

    void SetStats(){
        spinDamager.damageAmount = stats[weaponLevel].damage;
        spinDamager.timeBetweenDamage = stats[weaponLevel].speed;
        spinDamager.lifeTime = stats[weaponLevel].duration;
        spinDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        spawnTime = stats[weaponLevel].timeBetweenAttacks;

        spawnCounter = 0f;
    }
}
