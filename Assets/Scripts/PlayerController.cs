using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic; 

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Joystick joystick;

    [Header("Stats")]
    public float currentHealth;
    public float maxHealth = 100f;

    [Header("Weapons")]
    public List<Weapon> unassignedWeapons, assignedWeapons;
    public int maxWeapons = 3;
    
    [HideInInspector]
    public List<Weapon> fullyLeveledWeapons = new List<Weapon>();

    // Components
    private Rigidbody2D rb;
    
    // Movement input storage
    private Vector2 moveInput;

    // Singleton
    public static PlayerController instance; 

    void Start()
    {
        // Initialize singleton
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        
        // Add initial weapon if none assigned
        if(assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
    }

    void Update()
    {
        HandleMovementInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovementInput()
    {
        // Get joystick input
        Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical);
        
        // Clamp magnitude to prevent faster diagonal movement
        moveInput = Vector2.ClampMagnitude(input, 1f);
    }

    private void HandleMovement()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void AddWeapon(int weaponNumber)
    {
        if(weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}