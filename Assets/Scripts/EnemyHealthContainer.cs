using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthContainer : MonoBehaviour
{
    public static event System.Action OnBossDeath;
    public static EnemyHealthContainer instance;
    public GameObject crystal;
    
    public float currentHealth, maxHealth;
    public Slider healthSlider;

    private Vector3 currentPos;
    private EnemyController enemyController;
    
    // private List<GameObject> collectables = new List<GameObject>();

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
        // Debug.Log("damageToTake: " + damageToTake);
        if(currentHealth <= 0)
        {
            currentPos = gameObject.transform.position;

            if(gameObject.GetComponent<BossController>() != null){
                OnBossDeath?.Invoke();
                DestroyAllCrystals();
            }

            Transform parent = transform.parent;
            Destroy(gameObject);
            GameObject instance = Instantiate(crystal, currentPos, Quaternion.identity, parent.GetChild(0).transform);
            // collectables.Add(instance);
        }else{
             if (!healthSlider.gameObject.activeSelf)
             {
                 healthSlider.gameObject.SetActive(true);
             }

           healthSlider.value = currentHealth;
        }

        DamageServiceController.instance.MakeDamage(damageToTake, transform.position);
    }

    private void DestroyAllCrystals()
    {
        foreach (Transform child in transform.parent.GetChild(0))
        {
            if (child.gameObject.CompareTag("xp"))
            {
                Destroy(child.gameObject);
            }
        }
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
