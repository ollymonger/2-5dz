using System.Collections;
using System.Collections.Generic;
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
        currentWeapon = weapons[currentWeaponIndex];
        Debug.Log(weapons.Length);
        currentWeapon.weaponPrefab.SetActive(true);
        bindings = new Bindings();
        bindings.Enable();
        bindings.Player.Fire.Enable();
        bindings.Player.Fire.started += ctx => currentWeapon.Fire(currentWeapon, Camera.main.ScreenToWorldPoint(bindings.Player.Look.ReadValue<Vector2>()) - weaponInstantiated.transform.position, weaponInstantiated.transform.position, transform.gameObject);
        player = GameObject.FindGameObjectWithTag("PlayerAsset");

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
        if (collision.transform.tag == "Weapon")
        {
            PickupWeapon(collision.transform.parent.GetComponent<Weapon>());
        }
    }


    // Pickup weapon
    public void PickupWeapon(Weapon weapon)
    {
        if (weapon.weaponSO.weaponName == currentWeapon.weaponName)
        {
            return;
        }


    }

    public void SwitchWeapon(int newWeaponIndex)
    {
        if (newWeaponIndex != currentWeaponIndex && !isReloading)
        {
            weapons[currentWeaponIndex].weaponPrefab.SetActive(false);
            currentWeaponIndex = newWeaponIndex;
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.weaponPrefab.SetActive(true);
        }
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
