using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    public float xp = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponentInParent<PlayerXpController>().currentXp += xp;
            Destroy(gameObject);
        }
    }
}
