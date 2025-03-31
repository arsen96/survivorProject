using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float currenthealth, maxhealth;
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        currenthealth -= damageToTake;

        if(currenthealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}