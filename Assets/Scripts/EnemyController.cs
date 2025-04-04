using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;
    public float damage;

    public float hitCooldown = 1f;
    private float hitCounter;

    // Start is called before the first frame update
    void Start()
    {
        // target = FindObjectOfType<PlayerController>().transform;

        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate velocity towards the target
        theRB.linearVelocity = (target.position - transform.position).normalized * moveSpeed;

        // Manage hit cooldown
        if(hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collision is with player and hit cooldown is over
        if(collision.gameObject.tag == "Player" && hitCounter <= 0)
        {
            // Apply damage to player
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitCooldown;
        }
    }
}
