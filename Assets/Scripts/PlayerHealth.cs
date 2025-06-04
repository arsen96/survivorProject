using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public Image healthImpact;
    private float damageInterval = 1f; // Intervalle de temps pour infliger des dégâts
    private float nextDamageTime = 0f; // Temps pour le prochain dégât

    void Start()
    {
        playerHealth = 100;
    }

    void Update()
    {
        if (Time.time >= nextDamageTime && playerHealth > 0)
        {
            playerHealth -= 10;
            UpdateHealthImpact();
            nextDamageTime = Time.time + damageInterval;
        }
    }

    void UpdateHealthImpact()
    {
        float transparency = 1f - (playerHealth / 100f);
        Color imageColor = Color.white;
        imageColor.a = transparency;
        healthImpact.color = imageColor;
    }

    void PlayerTakingDamage(float damage)
    {
        if (playerHealth > 0)
        {
            playerHealth -= damage;
            Debug.Log("player is taking damage");
        }
    }
}
