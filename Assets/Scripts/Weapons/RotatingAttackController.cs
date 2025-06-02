using UnityEngine;

public class RotatingAttackController : Weapon
{
    public float rotationSpeed = 100f;

    public GameObject sword;


    public float atkDamage;

    [HideInInspector]
    public PlayerXpController secondParent;

    // public PlayerController parentPlayer;
    private int lastLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float spawnCounter;

    public delegate void BossDeathHandler();
    // public static event BossDeathHandler OnBossDefeated;

    private float defaultSpeed;


    private void HandleBossDeath()
        {

            ResetWeapon();
            resetNumberSwords();
        }


    private void OnEnable()
    {
        EnemyHealthContainer.OnBossDeath += HandleBossDeath;
    }

    private void OnDisable()
    {
        // EnemyHealthContainer.OnBossDeath -= HandleBossDeath;
    }
    void Start()
    {
        SetStats();
        if (transform.parent != null)
        {
            PlayerController parentPlayer = transform.parent.GetComponent<PlayerController>();
            // PlayerController parentPlayer = PlayerController.instance;
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

    }

    // Update is called once per frame
    void Update()
    {

        // if (secondParent != null && secondParent.level > lastLevel && transform.childCount <= 6)
        // {
        //     lastLevel = secondParent.level;
        //     AddNewSword();
        // }

        // spawnCounter -= Time.deltaTime;
        // if(spawnCounter <= 0){
        //     spawnCounter = stats[weaponLevel].timeBetweenAttacks;
        //     AddNewSword();
        // }


         if (statsUpdated == true || transform.childCount == 0)
        {
            statsUpdated = false;
            SetStats();
            AddNewSword();
        }
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

 
    void resetNumberSwords(){
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        rotationSpeed = defaultSpeed; 
    }

    void AddNewSword()
    {
        Vector3 pos = transform.position;
        // int nbrOfSwords = transform.childCount + 1; 
        float nbrOfSwords = stats[weaponLevel].amount;

        float angleStep = 360f / nbrOfSwords;
        rotationSpeed *= stats[weaponLevel].speed;

        Vector3 refAngle = transform.childCount > 0 ? transform.GetChild(0).rotation * Vector3.forward : Vector3.forward;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < nbrOfSwords; i++)
        {
            float angle = i * angleStep;

            Quaternion rota = Quaternion.AngleAxis(angle, refAngle);

            GameObject instance = Instantiate(sword, pos, rota, transform);
            instance.name = sword.name;
            // instance.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        
    }

    


    public void SetStats()
        {
            defaultSpeed = rotationSpeed;
            spawnCounter = 0f;
        }




 
}
