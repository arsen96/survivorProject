using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float durationAppear;
    private float _durationAppear;
    public TMP_Text damageText;

    private bool isAlreadyEnemyKilled = false;
    private bool animateNumbers = false;
    public float speedUpAppear = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _durationAppear = durationAppear;
        // SetDamage(10);
    }


    // Update is called once per frame
    void Update()
    {
        if(_durationAppear > 0 && isAlreadyEnemyKilled){
            _durationAppear -= Time.deltaTime;
            if(_durationAppear <= 0){
                Destroy(gameObject);
            }
        }

        transform.position += Vector3.up * speedUpAppear * Time.deltaTime;
    }





    public void SetDamage(float damageDisplay)
    {
         isAlreadyEnemyKilled = true;
        _durationAppear = durationAppear;
        damageText.text = damageDisplay.ToString();
    }
}
