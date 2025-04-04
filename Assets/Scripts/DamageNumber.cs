using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float durationAppear;
    private float _durationAppear;
    public TMP_Text damageText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _durationAppear = durationAppear;
        
        // SetDamage(10);
    }


    // Update is called once per frame
    void Update()
    {
        if(_durationAppear > 0 && _durationAppear < durationAppear){
            _durationAppear -= Time.deltaTime;

            if(_durationAppear <= 0){
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(float damageDisplay)
    {
        _durationAppear = durationAppear;
        // damageText.text = damageDisplay.ToString();
    }
}
