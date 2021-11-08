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

        float offset = cam.transform.position.z;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float dividableOffset = (screenWidth + screenHeight) / 200;
        Vector3 mousePosOffset = new Vector3(mousePos.x / dividableOffset, mousePos.y / dividableOffset, 0);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, offset), 3f * Time.deltaTime);


        if (look.x > 2 || look.x < -2 && look.y > 2 || look.y < -2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(7, 180, rot.eulerAngles.z), Time.deltaTime * 10);
            //Vector3 offsetPosition = new Vector3(mousePosOffset.x += transform.position.x, mousePosOffset.y += transform.position.y, offset);
            //cam.transform.position = Vector3.Lerp(cam.transform.position, offsetPosition, 3f * Time.deltaTime);

        }

        if (m != new Vector2(0, 0))
        {
            transform.position = Vector2.Lerp(transform.position, transform.position += (Vector3)m, 5f * Time.deltaTime);

        }
    }

}
