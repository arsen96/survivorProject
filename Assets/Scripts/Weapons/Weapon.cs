using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats; 

    public int weaponLevel = 0;

    [HideInInspector]
    public bool statsUpdated = false;

    public Sprite icon;

    public void LevelUpWeapon()
    {
        if (weaponLevel < stats.Count - 1)
        {

            weaponLevel++;
            statsUpdated = true;

            if(weaponLevel >= stats.Count - 1)
            {
                PlayerController.instance.fullyLeveledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }

    public void ResetWeapon()
    {
        // weaponLevel = 0;
    }
}



[System.Serializable]
public class WeaponStats
{
    public float speed;              // Vitesse (rotation, projectile, etc.)
    public float damage;             // Dégâts infligés
    public float range;              // Portée de l'arme
    public float timeBetweenAttacks; // Cadence de tir
    public float amount;             // Nombre de projectiles
    public float duration;           // Durée de vie des projectiles
    public string upgradeText;
    
}
