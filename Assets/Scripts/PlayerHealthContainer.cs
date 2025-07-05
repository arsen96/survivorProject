using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
        
    // Event declaration for game over
    public static event System.Action OnGameOver;
    
    public Slider healthSlider;
    public Image healthImpact;

    [Header("Effets Visuels")]
    public AnimationCurve damageCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float pulseSpeed = 2f;
    public Color bloodColor = Color.red;
    
    [HideInInspector]
    public float currentHealth, maxHealth;
    private bool gameOver = false;
    public PlayerController Player;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartGame();
    }


    public void StartGame()
    {
        gameOver = false;
        PlayerController Player = GetComponent<PlayerController>();
        if (Player != null)
        {
            maxHealth = Player.maxHealth;
            currentHealth = Player.currentHealth;
            UpdateHealthImpact();
        }

        if (healthSlider != null) 
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        Handheld.Vibrate();
        if (currentHealth <= 0)
        {
            if(!gameOver)
            {
                currentHealth = 0;
                healthSlider.value = 0;
                
                PlayerPrefs.SetInt("perdu", 1);
                PlayerPrefs.Save();
                UIController.instance.gameOverPanel.SetActive(true);
                UIController.instance.UpdateText("Perdu !");
                gameOver = true;
                Time.timeScale = 0;
                
                OnGameOver?.Invoke();
            }
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
        UpdateHealthImpact();
    }

    void UpdateHealthImpact()
    {
        if (healthImpact == null) return;
        
        float healthPercentage = currentHealth / maxHealth;
        float transparency = 1f - healthPercentage;
        
        transparency = damageCurve.Evaluate(transparency);
        
        Color imageColor = bloodColor;
        
        if (healthPercentage <= 0.3f)
        {
            float pulse = Mathf.Sin(Time.time * pulseSpeed) * 0.3f + 0.7f;
            transparency *= pulse;
            imageColor = Color.Lerp(bloodColor, Color.black, 0.3f);
        }
    
        imageColor.a = Mathf.Clamp01(transparency);
        healthImpact.color = imageColor;
    }

    void Update()
    {
        if (currentHealth > 0)
        {
            UpdateHealthImpact();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        if (healthSlider != null)
            healthSlider.value = currentHealth;
        UpdateHealthImpact();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
            healthSlider.value = currentHealth;
        UpdateHealthImpact();
    }
}