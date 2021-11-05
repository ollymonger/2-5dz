using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Bullet class
public class Bullet : MonoBehaviour
{
    // Bullet speed
    public float speed = 20f;
    // Bullet life time
    public float lifeTime = 3f;
    // Bullet damage
    public int damage = 40;
    // Bullet force
    public float force = 700f;
    // Bullet rigid body
    private Rigidbody2D rb;
    // sprite
    private Sprite sprite;
    // Player input
    // Player game object
    private GameObject player;
    // Player transform
    private Transform playerTransform;

    // Initialize bullet
    public void Initialize(GameObject bulletPrefab)
    {
        sprite = bulletPrefab.GetComponent<SpriteRenderer>().sprite;
        // Get bullet rigid body
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.mass = 0f;
        // Get player game object
        player = GameObject.FindGameObjectWithTag("Player");
        // Get player transform
        playerTransform = player.transform;
    }

}
