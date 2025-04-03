using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public Slider healthSlider;

    private float currentHealth, maxHealth;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController parentPlayer = transform.parent.GetComponent<PlayerController>();
        if (parentPlayer != null)
        {
            maxHealth = parentPlayer.maxHealth;
            currentHealth = parentPlayer.currentHealth;
        };

        if (healthSlider != null) {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.T))
        // {
        //     TakeDamage(10f);
        // }
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

           healthSlider.value = currentHealth;
    }
}