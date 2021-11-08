using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Weapon : MonoBehaviour
{
    // Take in a randon WeaponSO
    public WeaponSO weaponSO;
    // Vector3 for the current weapon's pos

    public void Awake()
    {
        WeaponSO[] weapons = Resources.FindObjectsOfTypeAll<WeaponSO>();
        weaponSO = Instantiate(weapons[Random.Range(0, weapons.Length)]);
        transform.name = weaponSO.weaponName;
        GameObject prefab = Instantiate(weaponSO.weaponPrefab, transform.position, Quaternion.identity);
        // Set the prefab's parent to the weapon
        prefab.transform.SetParent(transform);
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
        GameObject prefab = Instantiate(weaponSO.weaponPrefab, transform.position, Quaternion.identity);
        // Set the prefab's parent to the weapon
        prefab.transform.SetParent(transform);
    }
}
