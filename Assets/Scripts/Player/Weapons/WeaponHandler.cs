using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    private Bindings bindings;
    public WeaponSO[] weapons; // Player inventory..
    public int currentWeaponIndex;
    public WeaponSO currentWeapon;
    private GameObject weaponInstantiated;
    public float currentAmmo;
    public bool isReloading;
    private Vector2 look;
    private Vector3 direction;
    private GameObject player;

    private void Awake()
    {
        currentWeapon = Instantiate(weapons[currentWeaponIndex]);
        Debug.Log(weapons.Length);
        currentWeapon.weaponPrefab.SetActive(true);
        bindings = new Bindings();
        bindings.Enable();
        bindings.Player.Fire.Enable();
        bindings.Player.Use.Enable();
        bindings.Player.Fire.started += ctx => currentWeapon.Fire(currentWeapon, Camera.main.ScreenToWorldPoint(bindings.Player.Look.ReadValue<Vector2>()) - weaponInstantiated.transform.position, weaponInstantiated.transform.position, transform.gameObject);
        player = GameObject.FindGameObjectWithTag("PlayerAsset");
        bindings.Player.Use.started += ctx => PickupWeapon(GameObject.Find("Player").GetComponent<ToolTip>().closestFocus.transform.parent.GetComponent<Weapon>());
        bindings.Player.Swap.started += ctx => SwitchWeapon();
        weaponInstantiated = Instantiate(currentWeapon.weaponPrefab, transform.position + Vector3.up * .5f, transform.rotation, player.transform);
    }

    private void Update()
    {
        Vector2 look = bindings.Player.Look.ReadValue<Vector2>();
        // Get world to screen point of the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(look);
        // Get target to mouse pos from transform
        Vector3 target = mousePos - weaponInstantiated.transform.position;
        // Get the rotation quaternion in Z axis
        Quaternion rot = Quaternion.LookRotation(target, Vector3.forward);
        weaponInstantiated.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(7, 180, rot.eulerAngles.z), Time.deltaTime * 2.5f);
    }

    // On collision with a specific Weapon
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


    // Pickup weapon
    public void PickupWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            return;
        }
        if (weapons.Any(x => x.weaponName == weapon.weaponSO.weaponName))
        {
            currentWeapon.weaponAmmo = weapon.weaponSO.weaponAmmo;
            currentAmmo = currentWeapon.weaponAmmo;
            Debug.Log("Replenished ammo");
            changeUIValues(2);
            return;
        }
        else
        {
            Debug.Log("Player does not have this yet");
            // Add weapon to player Weapon array
            weapons = weapons.Concat(new WeaponSO[] { weapon.weaponSO }).ToArray();
            return;
        }
    }

    public void changeUIValues(int caseSwitch)
    {
        GameObject.FindObjectOfType<PlayerUI>().UpdateValues(caseSwitch);
    }

    public void SwitchWeapon()
    {
        // Switch weapon based on currentindex and length of Weapons[] & if not reloading

        if (currentWeaponIndex < weapons.Length - 1 && !isReloading)
        {
            currentWeaponIndex++;
            WeaponSO temp = Instantiate(weapons[currentWeaponIndex]);
            currentWeapon = temp;
        }
        else
        {
            currentWeaponIndex = 0;
            WeaponSO temp = Instantiate(weapons[currentWeaponIndex]);
            currentWeapon = temp;
        }
        changeUIValues(1);
    }

    /*    public IEnumerator Reload()
        {
            isReloading = true;
            currentWeapon.StartReload();
            yield return new WaitForSeconds(currentWeapon.weaponReloadTime);
            currentWeapon.FinishReload();
            isReloading = false;
        }*/

}
