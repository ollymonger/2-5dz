using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI MaxClip;
    // Start is called before the first frame update
    void Start()
    {
        weaponName.text = GameObject.FindObjectOfType<WeaponHandler>().currentWeapon.weaponName;
        ammo.text = GameObject.FindObjectOfType<WeaponHandler>().currentWeapon.weaponAmmo.ToString();
        MaxClip.text = GameObject.FindObjectOfType<WeaponHandler>().currentWeapon.weaponAmmoMax.ToString();
    }

    // Update is called once per frame
    public void UpdateValues(int caseSwitch)
    {
        switch (caseSwitch)
        {
            case 1:
                weaponName.text = GameObject.FindObjectOfType<WeaponHandler>().currentWeapon.weaponName;
                ammo.text = GameObject.FindObjectOfType<WeaponHandler>().currentWeapon.weaponAmmo.ToString();
                MaxClip.text = GameObject.FindObjectOfType<WeaponHandler>().currentWeapon.weaponAmmoMax.ToString();
                break;
            case 2:
                ammo.text = GameObject.FindObjectOfType<WeaponHandler>().currentWeapon.weaponAmmo.ToString();
                break;
            default:
                break;
        }
    }
}
