using UnityEngine;

public class CloseAttackWeapon : Weapon
{

    public SpinDamager spinDamager;
    private float attackCounter, direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }   

        
        attackCounter -= Time.deltaTime;
        if(attackCounter <= 0)
        {
            attackCounter = stats[weaponLevel].timeBetweenAttacks;

            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                if(Input.GetAxisRaw("Horizontal") > 0)
                {
                    spinDamager.transform.rotation = Quaternion.identity;
                } else
                {
                    spinDamager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
            }

            Instantiate(spinDamager, spinDamager.transform.position, spinDamager.transform.rotation, transform).gameObject.SetActive(true);

            for (int i = 1; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;
                
                Instantiate(spinDamager, spinDamager.transform.position, Quaternion.Euler(0f, 0f, rot), transform).gameObject.SetActive(true);
            }
        }
    }

    void SetStats()
    {
        spinDamager.damageAmount = stats[weaponLevel].damage;
        spinDamager.lifeTime = stats[weaponLevel].duration;
        spinDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        attackCounter = 0f;
        // spinDamager.timeBetweenDamage = stats[weaponLevel].timeBetweenAttacks;
    }
}
