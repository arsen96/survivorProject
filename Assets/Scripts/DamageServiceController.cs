using UnityEngine;

public class DamageServiceController : MonoBehaviour
{
    public static DamageServiceController instance;
    public DamageNumber damageNumberElement;
    public Transform canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeDamage(float damageNumber, Vector3 position)
    {
        DamageNumber newDamage = Instantiate(damageNumberElement, position, Quaternion.identity, canvas);
        if(newDamage){
            newDamage.SetDamage(damageNumber);
            newDamage.gameObject.SetActive(true);
        }
    }
}
