using UnityEngine;

public class WeaponThrower : Weapon
{

   public EnemySpinDamager enemySpinDamager;
   private float throwCounter;
   void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
            {
                statsUpdated = false;
                
                SetStats();
            }
        
        throwCounter -= Time.deltaTime;
        // Debug.Log($"weaponLevel: {weaponLevel}, stats.Length: {stats.Count}");
        if (throwCounter <= 0 && stats.Count > 0)
        {
                if(stats[weaponLevel] != null && stats[weaponLevel].timeBetweenAttacks > 0)   
                {
                    throwCounter = stats[weaponLevel].timeBetweenAttacks;
                }
                
                for(int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    if(enemySpinDamager != null){
                        Instantiate(enemySpinDamager, enemySpinDamager.transform.position, enemySpinDamager.transform.rotation).gameObject.SetActive(true);
                    }
                }
        }
    }

    void SetStats()
    {
        enemySpinDamager.damageAmount = stats[weaponLevel].damage;
        enemySpinDamager.lifeTime = stats[weaponLevel].duration;
        
        enemySpinDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        
        throwCounter = 0f;
    }
}
