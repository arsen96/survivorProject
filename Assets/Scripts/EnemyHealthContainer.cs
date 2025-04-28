using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthContainer : MonoBehaviour
{
    public static EnemyHealthContainer instance;
    public GameObject crystal;
    
    public float currentHealth, maxHealth;
    public Slider healthSlider;

    private Vector3 currentPos;
    private EnemyController enemyController;

    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.gameObject.SetActive(false);
        currentHealth = maxHealth;

        if (healthSlider != null) {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if(currentHealth <= 0)
        {
            currentPos = gameObject.transform.position;
            Transform parent = transform.parent;
            Destroy(gameObject);

            GameObject instance = Instantiate(crystal, currentPos, Quaternion.identity, parent.GetChild(0).transform);
        }else{
             if (!healthSlider.gameObject.activeSelf)
             {
                 healthSlider.gameObject.SetActive(true);
             }

           healthSlider.value = currentHealth;
        }

        DamageServiceController.instance.MakeDamage(damageToTake, transform.position);
    }


    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);
        
        if(shouldKnockback == true && enemyController != null)
        {
            enemyController.pushCounter = enemyController.pushDuration;
        }
    }
}
