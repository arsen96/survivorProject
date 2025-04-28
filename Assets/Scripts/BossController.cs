using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject firePrefab;
    public Transform firePoint;
    public float launchCooldown = 2f;
    private float _launchCooldown;
    private Transform target;

    private float duration = 3f;

    

    public float fireballSpeed;
    public float moveSpeed;

    public float knockBackTime = .5F;
    private float knockBackCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _launchCooldown = launchCooldown;
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_launchCooldown > 0)
        {
            _launchCooldown -= Time.deltaTime;
        }
        else
        {
            GameObject fireball = Instantiate(firePrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (target.position - firePoint.position).normalized;
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

            duration -= Time.deltaTime;
            if(duration > 0){
                rb.linearVelocity = direction * fireballSpeed;
            }
            _launchCooldown = launchCooldown;
        }
    }
        
}
