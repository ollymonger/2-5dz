using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;


public class Weapon : MonoBehaviour
{
    // Take in a randon WeaponSO
    public WeaponSO weaponSO;
    // Create a UI element to display the weapon name
    public TextMeshProUGUI weaponName;

    UnityEvent inInventory;

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

        if (inInventory == null)
        {
            inInventory = new UnityEvent();
        }

        inInventory.AddListener(() =>
        {
            Debug.Log(weaponSO.weaponName + " in in inventory");
            state = States.RUN_INVENTORY_CHECK;
        });

    }

    enum States
    {
        IDLE_STATE,
        RUN_INVENTORY_CHECK
    }

    States state = States.IDLE_STATE;

    void FixedUpdate()
    {
        if (GameObject.Find("Player").GetComponent<WeaponHandler>().weapons.Where(x => x.weaponName == weaponSO.weaponName).Any() && inInventory != null && state != States.RUN_INVENTORY_CHECK)
        {
            inInventory.Invoke();
            // Coroutine to swap the state back to idle after 30 seconds
            StartCoroutine(SwapState());
        }
    }

    IEnumerator SwapState()
    {
        yield return new WaitForSeconds(30);
        state = States.IDLE_STATE;
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
        GameObject prefab = Instantiate(weaponSO.weaponPrefab, transform.position, Quaternion.identity);
        // Set the prefab's parent to the weapon
        prefab.transform.SetParent(transform);
    }
}
