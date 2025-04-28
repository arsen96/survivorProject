using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float currentHealth;
    public float maxHealth = 100f;
    public float atkDamage = 5;
    public float armor = 0f;

    public Joystick joystick;

    public GameObject myPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        // Tu peux ajouter un Normalize() si tu veux une vitesse constante
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
            if (!HasChildWithName(myPrefab.name))
            {
                GameObject instance = Instantiate(myPrefab, transform.position, Quaternion.identity, transform);
                instance.name = myPrefab.name;
            }
        //}
    }

    bool HasChildWithName(string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name == name)
            {
                return true;
            }
        }
        return false;
    }
}
