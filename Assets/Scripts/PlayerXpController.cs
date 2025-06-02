using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq; // Import for LINQ
using Random = UnityEngine.Random; // Ensure Unity's Random is used

public class PlayerXpController : MonoBehaviour
{
    public BarSliderController bar;

    public float currentXp = 0f, maxXp = 10f;
    public int level = 1;

    public Weapon weapon;

    public List<Weapon> weaponsToUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        bar.max = maxXp;
        bar.label.text = "Nv " + level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        bar.current = currentXp;
        if (currentXp >= maxXp)
        {
            level += 1;

            Weapon[] weapons = GetComponentsInChildren<Weapon>();

            foreach (Weapon w in weapons)
            {
                UIController.instance.levelUpPanel.SetActive(true);
            }

            Time.timeScale = 0f;

            weaponsToUpgrade.Clear();
            List<Weapon> availableWeapons = new List<Weapon>();
            availableWeapons.AddRange(PlayerController.instance.assignedWeapons);
            if (availableWeapons.Count > 0)
            {
                int selectedWeapon = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selectedWeapon]);
                availableWeapons.RemoveAt(selectedWeapon);
            }

            if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLeveledWeapons.Count < PlayerController.instance.maxWeapons)
            {
                availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
            }

            for (int i = weaponsToUpgrade.Count; i < 3; i++)
            {
                if (availableWeapons.Count > 0)
                {
                    int selectedWeapon = Random.Range(0, availableWeapons.Count);
                    weaponsToUpgrade.Add(availableWeapons[selectedWeapon]);
                    availableWeapons.RemoveAt(selectedWeapon);
                }
            }
            for (int i = 0; i < weaponsToUpgrade.Count; i++)
            {
                UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
            }


            for(int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
            {
                if(i < weaponsToUpgrade.Count)
                {
                    UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
                }
            }

            currentXp = currentXp - maxXp;
            maxXp *= 1.5f;
            if (bar != null)
            {
                bar.max = maxXp;
                bar.current = currentXp;
                bar.label.text = "Nv " + level.ToString();
            }
        }
    }
}