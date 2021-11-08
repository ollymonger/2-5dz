using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class Weapon : MonoBehaviour
{
    // Take in a randon WeaponSO
    public WeaponSO weaponSO;
    // Create a UI element to display the weapon name
    public TextMeshProUGUI weaponName;

    public void Awake()
    {
        // Find all objects of WeaponSO from path using assetbundle
        WeaponSO[] weapons = Resources.LoadAll<WeaponSO>("Assets");
        weaponSO = Instantiate(weapons[Random.Range(0, weapons.Length)]);
        transform.name = weaponSO.weaponName;
        GameObject prefab = Instantiate(weaponSO.weaponPrefab, transform.position, Quaternion.identity);
        // Set the prefab's parent to the weapon
        prefab.transform.SetParent(transform);
        weaponName = new TextMeshProUGUI();
        weaponName.text = weaponSO.weaponName;
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
        GameObject prefab = Instantiate(weaponSO.weaponPrefab, transform.position, Quaternion.identity);
        // Set the prefab's parent to the weapon
        prefab.transform.SetParent(transform);
    }
}
