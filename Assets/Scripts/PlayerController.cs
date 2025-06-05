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

    public GameObject swordPrefab;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        // Tu peux ajouter un Normalize() si tu veux une vitesse constante
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        rb.linearVelocity = moveInput * moveSpeed;

        if (!HasChildWithName(swordPrefab.name))
        {
            GameObject instance = Instantiate(swordPrefab, transform.position, Quaternion.identity, transform);
            instance.name = swordPrefab.name;
        }
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
