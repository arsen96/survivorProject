using UnityEngine;

public class EnemySpinDamager : MonoBehaviour
{
   public float lifeTime;

    private float growSpeed = 1.5f;
    private Vector3 targetSize;

    // [HideInInspector]
    public float damageAmount;
    public float hitCooldown = 1f;
    private float hitCounter;
    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;

    public bool destroyOnImpact;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0){
            Destroy(gameObject);
        }

        if(hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
      
    }

      private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the weapon holder
        if (other.gameObject.tag == "MainPlayer" && hitCounter <= 0)
        {
             PlayerHealthController.instance.TakeDamage(damageAmount);
             hitCounter = hitCooldown;
        }
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {

    //     if(damageOverTime == true){
    //         if(other.gameObject.tag == "Enemy"){
    //             enemiesInRange.Remove(other.GetComponent<EnemyController>());
    //         }
    //     }
    // }

}
