using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{

    public TMP_Text upgrageDescText, nameLevelText;
    public Image weaponIcon;

    public Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        if (PlayerController.instance.assignedWeapons.Contains(theWeapon))
        {
            upgrageDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;

            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name + " - Lvl " + theWeapon.weaponLevel;
        }
        else
        {
            upgrageDescText.text = "Unlock " + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        

        assignedWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if(assignedWeapon != null)
        {
            if(PlayerController.instance.assignedWeapons.Contains(assignedWeapon))
            {
                assignedWeapon.LevelUp();                
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }

            UIController.instance.levelUpPanel.SetActive(false);

            Time.timeScale = 1f;

        }
    }
}
