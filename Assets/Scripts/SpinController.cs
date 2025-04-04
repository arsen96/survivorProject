using UnityEngine;

public class SpinController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotateSpeed = 2f;

     private Transform target;

    void Start()
    {
    //    target = EnemyHealthContainer.instance.transform;
       target = FindObjectOfType<EnemyHealthContainer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotateSpeed *  Time.deltaTime) ; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // FindObjectOfType<EnemyHealthContainer>().transform;
           other.GetComponent<EnemyHealthContainer>().TakeDamage(5);
        }
    }
}
