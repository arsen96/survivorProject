using UnityEngine;

public class SpinController : Weapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float rotateSpeed;
    private float defaultSpeed; // Ajout pour stocker la valeur par défaut
    public Transform holder, fireBallToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;
    public SpinDamager spinDamager;
    // private Transform target;

    [HideInInspector]
    public PlayerXpController secondParent;

    private int lastLevel;

    // public delegate void BossDeathHandler();

    // private void HandleBossDeath()
    // {
    //     ResetWeapon();
    //     // rotateSpeed = defaultSpeed;
    //     // ResetWeapon
    //     // ResetNumberSwords();
    // }

    // private void OnEnable()
    // {
    //     EnemyHealthContainer.OnBossDeath += HandleBossDeath;
    // }

    private void OnDisable()
    {
        // EnemyHealthContainer.OnBossDeath -= HandleBossDeath;
    }

    void Start()
    {
       SetStats(); 
       defaultSpeed = rotateSpeed;
        PlayerController parentPlayer = transform.parent.GetComponent<PlayerController>();
            if (parentPlayer != null)
            {
                // atkDamage = parentPlayer.atkDamage;
            }

            Transform grandparentTransform = transform.parent?.parent;
            if (grandparentTransform != null)
            {
                secondParent = grandparentTransform.GetComponent<PlayerXpController>();
                if (secondParent != null)
                {
                    lastLevel = secondParent.level;
                }
            }
    }

   

    public void SetStats()
    {

        rotateSpeed = stats[weaponLevel].speed;

        spinDamager.damageAmount = stats[weaponLevel].damage;
        
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        spinDamager.lifeTime = stats[weaponLevel].duration;
        
        spawnCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(holder != null){
                    // transform.Rotate(0,0,rotateSpeed *  Time.deltaTime) ; 
            holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (360 * Time.deltaTime * rotateSpeed));
            spawnCounter -= Time.deltaTime;
            if(spawnCounter <= 0){
                spawnCounter = timeBetweenSpawn;
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    float angle = (360f / stats[weaponLevel].amount) * i;
                    Instantiate(fireBallToSpawn, fireBallToSpawn.position, Quaternion.Euler(0f, 0f, angle), holder).gameObject.SetActive(true);
                }
            }
        }

        if(statsUpdated == true){
            SetStats();
            statsUpdated = false;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

       if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                RotatingAttackController parent = GetComponentInParent<RotatingAttackController>();
                if(parent != null){
                    bool enablePushback = enemyController.enablePushback;
                    float damage = 0;
                    int statsIndex = Mathf.Min(parent.secondParent.level - 1, parent.stats.Count - 1);
                    damage = parent.stats[statsIndex].damage;
                    other.GetComponent<EnemyHealthContainer>().TakeDamage(damage, enablePushback);
                }
            }
        }else if(other.gameObject.tag == "Player"){
            // boule de feu
           PlayerHealthController.instance.TakeDamage(10);
        }
    }


    // void ResetNumberSwords()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         Destroy(child.gameObject);
    //     }
    // }
}
