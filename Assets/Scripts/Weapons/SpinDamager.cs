using UnityEngine;
using System.Collections.Generic;
public class SpinDamager : MonoBehaviour
{

    public float lifeTime;

    private float growSpeed = 1.5f;
    private Vector3 targetSize;

    // [HideInInspector]
    public float damageAmount;

    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;

    private List<EnemyController> enemiesInRange = new List<EnemyController>();

    public bool makeSlowBigger;

    public bool destroyOnImpact;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Destroy(gameObject, lifeTime);
        if(makeSlowBigger){
            targetSize = transform.localScale;
            transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(makeSlowBigger){
                transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

            lifeTime -= Time.deltaTime;
            if(lifeTime <= 0){
                targetSize = Vector3.zero;

                if(transform.localScale.x == 0f){
                    Destroy(gameObject);
                }
            }
        }


        if(damageOverTime == true){
            // damageCounter -= Time.deltaTime;
            // if(damageCounter <= 0){
                damageCounter = timeBetweenDamage;
                for(int i = 0; i < enemiesInRange.Count; i++){
                    if(enemiesInRange[i] != null){
                        
                        enemiesInRange[i].GetComponent<EnemyHealthContainer>().TakeDamage(damageAmount, true);
                        if(destroyOnImpact == true){
                                Destroy(gameObject);
                        }  
                    }else{
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            // }
        }
    }

      private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the weapon holder

        if (other.gameObject.tag == "Enemy")
        {
            // Debug.Log("damageOverTime", damageOverTime);
            if(damageOverTime == false){
                EnemyController enemyController = other.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    SpinController grandparent = GetComponentInParent<SpinController>();
                    if(grandparent != null){
                        bool enablePushback = enemyController.enablePushback;
                        float damage = 0;
                        int statsIndex = Mathf.Min(grandparent.secondParent.level - 1, grandparent.stats.Count - 1);
                        damage = grandparent.stats[statsIndex].damage;
                        other.GetComponent<EnemyHealthContainer>().TakeDamage(damage, enablePushback);
                    }
                }
            }else{
                enemiesInRange.Add(other.GetComponent<EnemyController>());
                 
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if(damageOverTime == true){
            if(other.gameObject.tag == "Enemy"){
                enemiesInRange.Remove(other.GetComponent<EnemyController>());
            }
        }
    }

}
