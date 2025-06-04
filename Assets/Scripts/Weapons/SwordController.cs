using UnityEngine;

public class SwordController : MonoBehaviour
{
    // public float swordDamage;
    // public float currentDamage;
    
    public float spawnCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // SetStats();
        // if (transform.parent != null)
        // {
        //     RotatingAttackController grandparent = GetComponentInParent<RotatingAttackController>();
        //     GetComponentInParent<RotatingAttackController>();
        //     if (grandparent != null)
        //     {
        //         currentDamage = grandparent.atkDamage + swordDamage;
        //     };
        // }

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                RotatingAttackController grandparent = GetComponentInParent<RotatingAttackController>();

                bool enablePushback = enemyController.enablePushback;
                float damage = 0;
                int statsIndex = Mathf.Min(grandparent.secondParent.level - 1, grandparent.stats.Count - 1);
                damage = grandparent.stats[statsIndex].damage;
                other.GetComponent<EnemyHealthContainer>().TakeDamage(damage, enablePushback);
                // if(isBossController && other.gameObject.GetComponent<BossController>() == null){
                //     Debug.Log("Killeddd");
                // }
            }
        }
    }

}



//     public void SetStats()
//         {


//             // Debug.Log("stats"+ stats);

    
//             //  Debug.Log("Counttt" + stats.Count);

//             // Debug.Log("stats[0] " + stats[weaponLevel]);
//             // currentDamage = stats[weaponLevel].damage;  
            
//             // transform.localScale = Vector3.one * stats[weaponLevel].range;
            
//             // timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
            
//             // damager.lifetime = stats[weaponLevel].duration;
            
//             spawnCounter = 0f;
//         }
// }
