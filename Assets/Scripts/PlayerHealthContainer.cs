using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        PlayerController Player = GetComponent<PlayerController>();
        if (Player != null)
        {
            maxHealth = Player.maxHealth;
            currentHealth = Player.currentHealth;
            // Debug.Log("currentHealth" + currentHealth);
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
            // Debug.Log("Mort " + currentHealth);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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