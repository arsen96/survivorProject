using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthContainer : MonoBehaviour
{
    public static EnemyHealthContainer instance;
    
    public float currentHealth, maxHealth;
    public Slider healthSlider;

    private bool damaged = false;

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
            gameObject.SetActive(false);
        }else{
             if (!healthSlider.gameObject.activeSelf)
             {
                 healthSlider.gameObject.SetActive(true);
             }

           healthSlider.value = currentHealth;
        }

        DamageServiceController.instance.MakeDamage(damageToTake, transform.position);
    }
}
