using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D theRB;
    public float rotateSpeed;

    public float throwPower;
    void Start()
    {
        theRB.linearVelocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + 
                (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(theRB.linearVelocity.x)));
    }

}
