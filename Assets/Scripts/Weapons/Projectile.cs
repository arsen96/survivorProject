using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float moveSpeed;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collision is with player and hit cooldown is over

        if(collision.gameObject.tag == "Enemy" )
        {
            // Apply damage to player
            // get parent component ProjectileWeapon
            ProjectileWeapon parent = ProjectileWeapon.instance;
            if (parent == null)
            {
                Debug.LogError("ProjectileWeapon parent not found!");
                return;
            }
            if (PlayerHealthController.instance == null)
            {
                Debug.LogError("PlayerHealthController.instance is null!");
                return;
            }
            EnemyHealthContainer enemyHealth = collision.gameObject.GetComponent<EnemyHealthContainer>();
            // if (enemyHealth != null)
            // {
                enemyHealth.TakeDamage(parent.stats[parent.weaponLevel].damage, true);
            // }
            // else
            // {
            //     Debug.LogError("EnemyHealthContainer component not found on the collided object!");
            // }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
