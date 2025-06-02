using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
  public TMP_Text upgradeDescText, nameLevelText;
   public Image weaponIcon;
   private Weapon assignedWeapon;
   
   // References
   public void UpdateButtonDisplay(Weapon theWeapon)
   {

    if(theWeapon.gameObject.activeSelf == true)
    {
       upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
       weaponIcon.sprite = theWeapon.icon;
       
       nameLevelText.text = theWeapon.name + " - Niveau " + theWeapon.weaponLevel;
    }else{
        upgradeDescText.text = "Déverrouiller " + theWeapon.name;
        weaponIcon.sprite = theWeapon.icon;
        nameLevelText.text = theWeapon.name;
    }
       assignedWeapon = theWeapon;
   }

   public void SelectUpgrade()
   {
    if (assignedWeapon != null)
    {
        if(assignedWeapon.gameObject.activeSelf == true){
            assignedWeapon.LevelUpWeapon();
        }else{
            PlayerController.instance.AddWeapon(assignedWeapon);
        }

        UIController.instance.levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
   }
}


