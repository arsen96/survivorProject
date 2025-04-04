using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public Slider healthSlider;

    private float currentHealth, maxHealth;
    private bool damaged = false;

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

    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (healthSlider != null)
            {
                if (!healthSlider.gameObject.activeSelf)
                {
                    healthSlider.gameObject.SetActive(true);
                }
                healthSlider.value = currentHealth;
            }
        }
    }
}