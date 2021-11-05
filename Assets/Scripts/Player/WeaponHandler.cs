using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    private Bindings bindings;
    public WeaponSO[] weapons;
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
        currentWeapon = weapons[0];
        Debug.Log(weapons.Length);
        currentWeapon.weaponPrefab.SetActive(true);
        bindings = new Bindings();
        bindings.Enable();
        bindings.Player.Fire.Enable();
        bindings.Player.Fire.started += ctx => currentWeapon.Fire(currentWeapon, Camera.main.ScreenToWorldPoint(bindings.Player.Look.ReadValue<Vector2>()) - weaponInstantiated.transform.position, weaponInstantiated.transform.position, transform.gameObject);
        player = GameObject.FindGameObjectWithTag("PlayerAsset");

        weaponInstantiated = Instantiate(currentWeapon.weaponPrefab, transform.position + Vector3.up * .5f, transform.rotation, player.transform);
    }

    // Position the weaponPrefab infront of the player

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