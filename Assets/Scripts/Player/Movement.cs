using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    private Bindings bindings;
    private InputAction movement;
    private Camera cam;

    private Vector3 spawn;
    private GameObject obj;

    public void SetSpawn(Vector3 spawn)
    {
        this.spawn = spawn;
    }

    private void Awake()
    {
        bindings = new Bindings();
        bindings.Enable();
        movement = bindings.Player.Move;
        movement.Enable();
        cam = Camera.main;

        transform.position = (Vector3)spawn;

        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = spawn;
    }

    private void Update()
    {
        Vector2 m = movement.ReadValue<Vector2>();
        // Read the look quaternion binding
        Vector2 look = bindings.Player.Look.ReadValue<Vector2>();
        // Get world to screen point of the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(look);
        // Get target to mouse pos from transform
        Vector3 target = mousePos - transform.position;
        // Get the rotation quaternion in Z axis
        Quaternion rot = Quaternion.LookRotation(target, Vector3.forward);

        if (look.x > 0.5 || look.x < -0.5 && look.y > 0.5 || look.y < -0.5)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(7, 180, rot.eulerAngles.z), Time.deltaTime * 10);
        }

        if (m != new Vector2(0, 0))
        {
            transform.position = Vector2.Lerp(transform.position, transform.position += (Vector3)m, 5f * Time.deltaTime);

            //Move the camera with this gameobject

            float offset = cam.transform.position.z;

            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, offset), 3f * Time.deltaTime);
        }
    }
}
