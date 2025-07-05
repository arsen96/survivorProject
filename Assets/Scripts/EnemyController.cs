using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;
    public float damage;

    public float hitCooldown = 1f;
    private float hitCounter;
    public float pushDuration = .5F;
    public float pushCounter;
    [SerializeField] public bool enablePushback = true;

    public float levelDamageMultiplier;
    public float levelMoveSpeedMultiplier;


    [HideInInspector]
    public float _levelDamageMultiplier = 1f;
    [HideInInspector]
    public float _levelMoveSpeedMultiplier = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                
                if(moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if(pushCounter < 0){
                    moveSpeed = Mathf.Abs(moveSpeed * .5f);
                }
            }


        if (target.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if(hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        theRB.linearVelocity = (target.position - transform.position).normalized * (moveSpeed * _levelMoveSpeedMultiplier);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MainPlayer" && hitCounter <= 0)
        {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitCooldown;
        }
    }
   
}
