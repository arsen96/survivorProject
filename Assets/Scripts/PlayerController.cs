using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic; 

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float currentHealth;
    public float maxHealth = 100f;
    public float atkDamage = 5;
    public float armor = 0f;

    public Joystick joystick;

    public GameObject myPrefab;

    public static PlayerController instance; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Weapon> unassignedWeapons, assignedWeapons;

    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLeveledWeapons = new List<Weapon>();
    void Start()
    {
        instance = this;
        if(assignedWeapons.Count == 0){
            Debug.Log("Adding weapon");
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
            if (!HasChildWithName(myPrefab.name))
            {
                Transform childWithNameWea = transform.Find("Weapons");
                if (childWithNameWea != null)
                {
                    // GameObject instance = Instantiate(myPrefab, transform.position, Quaternion.identity, childWithNameWea);
                    // instance.name = myPrefab.name;
                }
            }
        //}
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

    bool HasChildWithName(string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name == name)
            {
                return true;
            }
            foreach (Transform grandChild in child)
            {
                if (grandChild.name == name)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
