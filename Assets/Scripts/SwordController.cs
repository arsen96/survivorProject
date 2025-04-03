using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float swordDamage = 3f;
    public float currentDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.parent != null)
        {
            Debug.Log("j'ai un parent");
            RotatingAttackController grandparent = GetComponentInParent<RotatingAttackController>();
            GetComponentInParent<RotatingAttackController>();
            if (grandparent != null)
            {
                Debug.Log("j'ai le component");
                currentDamage = grandparent.atkDamage + swordDamage;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentDamage" + currentDamage);
    }
}
