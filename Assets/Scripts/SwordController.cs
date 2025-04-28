using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float swordDamage = 3;
    public float currentDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.parent != null)
        {
            RotatingAttackController grandparent = GetComponentInParent<RotatingAttackController>();
            GetComponentInParent<RotatingAttackController>();
            if (grandparent != null)
            {
                currentDamage = grandparent.atkDamage + swordDamage;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("currentDamage " + currentDamage);

        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                bool enablePushback = enemyController.enablePushback;
                other.GetComponent<EnemyHealthContainer>().TakeDamage(currentDamage, enablePushback);
            }
        }
    }
}
