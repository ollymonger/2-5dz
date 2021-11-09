using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Generate a weapon object from a weapon SO

[CreateAssetMenu(fileName = "New WeaponSO", menuName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [Tooltip("The name of the weapon")]
    public string weaponName;
    public GameObject weaponPrefab;
    public Sprite weaponIcon;
    // bullet
    public BulletSO bulletPrefab;
    public float bulletSpeed;

    public int weaponDamage;
    public float weaponFireRate;
    public float weaponRange;
    public float weaponReloadTime;
    public float weaponAmmo;
    public float weaponAmmoMax;
    public ParticleSystem muzzleFlash;


    public WeaponSO(WeaponSO playerWeapon)
    {
        playerWeapon.weaponName = weaponName;
        playerWeapon.weaponPrefab = weaponPrefab;
        playerWeapon.weaponIcon = weaponIcon;
        playerWeapon.weaponDamage = weaponDamage;
        playerWeapon.weaponFireRate = weaponFireRate;
        playerWeapon.weaponRange = weaponRange;
        playerWeapon.weaponReloadTime = weaponReloadTime;
        playerWeapon.weaponAmmo = weaponAmmo;
    }

    // Reload the weapon
    public void Reload(WeaponSO playerWeapon)
    {
        playerWeapon.weaponAmmo = playerWeapon.weaponAmmoMax;
    }

    // Fire the weapon
    public void Fire(WeaponSO playerWeapon, Vector3 direction, Vector3 position, GameObject player)
    {
        // Check if the weapon has ammo
        if (playerWeapon.weaponAmmo > 0)
        {
            // clone bullet prefab
            GameObject bullet = Instantiate(playerWeapon.bulletPrefab.bulletPrefab, direction, Quaternion.identity);
            bullet.GetComponent<SpriteRenderer>().sortingOrder = 1;
            // Fire bullet away from position
            bullet.transform.position = position;
            // Set bullet speed
            bullet.GetComponent<Rigidbody2D>().velocity = direction * playerWeapon.bulletSpeed;
            // As bullet reaches range, destroy the gameobject
            Destroy(bullet, playerWeapon.weaponRange * Time.deltaTime);
            playerWeapon.weaponAmmo--;
            // Play playerweapon bullet particle effect once
            ParticleSystem muzzle = Instantiate(playerWeapon.muzzleFlash, position, new Quaternion(-90, 0, 0, 0));
            muzzle.transform.position = position;
            muzzle.transform.SetParent(player.transform);
            muzzle.Play();
            Destroy(muzzle.gameObject, 0.5f);
        }
    }
}
