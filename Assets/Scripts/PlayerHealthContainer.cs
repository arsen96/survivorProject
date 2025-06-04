using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public Slider healthSlider;
    public Image healthImpact;

    public Image endGame;

    [Header("Effets Visuels")]
    public AnimationCurve damageCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float pulseSpeed = 2f;
    public float flashDuration = 0.3f;
    public Color bloodColor = Color.red;
    
    [HideInInspector]
    public float currentHealth, maxHealth;
    private bool damaged = false;
    private bool gameOver = false;
    private Coroutine damageEffectCoroutine;
    private Camera playerCamera;

    private void Awake()
    {
        instance = this;
        playerCamera = Camera.main;
        if (playerCamera == null)
            playerCamera = FindObjectOfType<Camera>();
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
                GameObject gameMaster = GameObject.FindGameObjectWithTag("GameController");
                gameMaster.GetComponent<GameMaster>().Finish("Perdu !");
                endGame.gameObject.SetActive(true);
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
            // StartDamageEffect();
        }
        UpdateHealthImpact();
    }

    // void StartDamageEffect()
    // {
    //     if (damageEffectCoroutine != null)
    //         StopCoroutine(damageEffectCoroutine);
        
    //     damageEffectCoroutine = StartCoroutine(DamageEffectCoroutine());
        
    //     if (useScreenShake)
    //         StartCoroutine(ScreenShake());
    // }

    // IEnumerator DamageEffectCoroutine()
    // {
    //     // Flash rouge intense au moment du dégât
    //     Color originalColor = healthImpact.color;
    //     Color flashColor = bloodColor;
    //     flashColor.a = 0.8f;
        
    //     healthImpact.color = flashColor;
    //     yield return new WaitForSeconds(0.1f);
        
    //     // Transition vers la couleur normale
    //     float elapsedTime = 0f;
    //     while (elapsedTime < flashDuration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         float t = elapsedTime / flashDuration;
    //         healthImpact.color = Color.Lerp(flashColor, originalColor, t);
    //         yield return null;
    //     }
    // }

    // IEnumerator ScreenShake()
    // {
    //     Vector3 originalPosition = playerCamera.transform.position;
    //     float shakeDuration = 0.2f;
    //     float shakeIntensity = 0.1f;
        
    //     float elapsedTime = 0f;
    //     while (elapsedTime < shakeDuration)
    //     {
    //         float x = Random.Range(-shakeIntensity, shakeIntensity);
    //         float y = Random.Range(-shakeIntensity, shakeIntensity);
            
    //         playerCamera.transform.position = originalPosition + new Vector3(x, y, 0);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
        
    //     playerCamera.transform.position = originalPosition;
    // }

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