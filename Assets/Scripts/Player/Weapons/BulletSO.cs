using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Generate a bulletSO class

[CreateAssetMenu(fileName = "New BulletSO", menuName = "BulletSO")]
public class BulletSO : ScriptableObject
{
    // The bullet prefab
    public GameObject bulletPrefab;

    public Bullet bullet;

    public Rigidbody2D rigidbody2D;

    // Initialise Bullet object
    void Start()
    {
        bullet = new Bullet();
        bullet.Initialize(bulletPrefab);
    }

}
