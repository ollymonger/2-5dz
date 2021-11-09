using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using TMPro;

public class ToolTip : MonoBehaviour
{
    // Create a GameObject[] to store UI objects
    public Dictionary<GameObject, int> toolTipObjects = new Dictionary<GameObject, int>();

    public GameObject closestFocus = null;

    public TMP_FontAsset fontAsset;
    Dictionary<GameObject, int> toolTipCanvases = new Dictionary<GameObject, int>();

    // On update, CircleCast2D around the player's transform and debug whether a Weapon object is in range of the player
    private void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 3f, transform.forward);
        RaycastHit2D[] hitArray = Physics2D.CircleCastAll(transform.position, 3f, transform.forward, 3f, LayerMask.GetMask("Weapon"));
        // Make closest focus the first object in the array that is not on the player or the player's weapon and return the gameobject or null
        closestFocus = hitArray.Where(x => x.transform.gameObject != transform.gameObject && x.transform.gameObject != transform.GetChild(0).GetChild(0).gameObject).Select(x => x.transform.gameObject).FirstOrDefault();
        Debug.Log(closestFocus);
        for (int i = 0; i < hitArray.Length; i++)
        {
            // Create a gameObject with the weaponSO frfom the parent Weapon
            GameObject weapon = hitArray[i].transform.parent.gameObject;


            // Check to see if weapon is already in toolTipObjects using Linq
            if (!toolTipObjects.Keys.Contains(weapon))
            {
                // If not, add it to the toolTipObjects dictionary
                toolTipObjects.Add(weapon, 0);
                Debug.Log(hitArray[i].collider.gameObject.name + "Added");
            }

            if (toolTipObjects.Keys.Contains(weapon) && toolTipObjects[weapon] == 0)
            {
                // If the weapon is in the toolTipObjects dictionary, set the int to 1
                toolTipObjects[weapon] = 1;
                Debug.Log(hitArray[i].collider.gameObject.name + "Set to 1");
            }

            if (toolTipObjects.Keys.Contains(weapon) && toolTipObjects[weapon] == 1)
            {
                // Get key in list from weapon
                CreateToolTip(weapon);
                toolTipObjects[weapon] = 2;
            }
            /*// Create a gameobject for canvas
            GameObject canvas = new GameObject();
            canvas.transform.SetParent(hit.transform);
            // Attach Canvas component to the gameobject
            canvas.AddComponent<Canvas>();
            // Set panel to be in WorldSpace
            canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            // Set the size of the canvas
            canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            // Set the position of the canvas
            canvas.transform.position = hit.transform.position;
            // Set the Event Camera to be Main Camera;
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;

            // Attach a Text component to the canvas
            GameObject text = new GameObject();
            text.transform.SetParent(canvas.transform);
            text.AddComponent<TextMeshProUGUI>();
            text.GetComponent<TextMeshProUGUI>().text = "Press E to pick up";
            text.GetComponent<TextMeshProUGUI>().fontSize = 20;*/

        }


    }

    // Create a tooltip function to display tooltip
    public void CreateToolTip(GameObject weapon)
    {
        // Create a gameobject for canvas
        GameObject canvas = new GameObject();
        // Attach Canvas component to the gameobject
        canvas.AddComponent<Canvas>();
        // Set panel to be in WorldSpace
        canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        // Set the size of the canvas
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
        canvas.GetComponent<RectTransform>().position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, 0);
        // Set the position of the canvas
        // Set the Event Camera to be Main Camera;
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        canvas.GetComponent<Canvas>().sortingOrder = 1;
        canvas.transform.SetParent(weapon.transform);

        // Attach a Text component to the canvas
        GameObject text = new GameObject();
        text.AddComponent<TextMeshProUGUI>();
        // Set the width and height of the text component
        text.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta = new Vector2(5, 5);
        text.GetComponent<TextMeshProUGUI>().text = weapon.GetComponent<Weapon>().weaponSO.weaponName;
        text.GetComponent<TextMeshProUGUI>().color = Color.white;
        text.GetComponent<TextMeshProUGUI>().font = fontAsset;
        text.GetComponent<TextMeshProUGUI>().fontSize = 0.25f;
        text.GetComponent<RectTransform>().position = new Vector3(weapon.transform.position.x + 2.1f, weapon.transform.position.y + -2f, 0);
        text.transform.SetParent(canvas.transform);

        GameObject text2 = new GameObject();
        text2.AddComponent<TextMeshProUGUI>();
        // Set the width and height of the text component
        text2.GetComponent<TextMeshProUGUI>().rectTransform.sizeDelta = new Vector2(2f, 5);
        if (GameObject.Find("Player").GetComponent<WeaponHandler>().weapons.Any(x => x.name == weapon.GetComponent<Weapon>().weaponSO.weaponName))
        {
            text2.GetComponent<TextMeshProUGUI>().text = "Press E to purchase ammo";
            Debug.Log("player has weapon");
        }
        else
        {
            text2.GetComponent<TextMeshProUGUI>().text = "Press E to purchase weapon";
            Debug.Log("player does not have weapon");
        }

        text2.GetComponent<TextMeshProUGUI>().color = Color.white;
        text2.GetComponent<TextMeshProUGUI>().font = fontAsset;
        text2.GetComponent<TextMeshProUGUI>().fontSize = 0.20f;
        text2.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        text2.GetComponent<RectTransform>().position = new Vector3(weapon.transform.position.x, weapon.transform.position.y + -0.5f, 0);
        text2.transform.SetParent(canvas.transform);

        StartCoroutine(DisableToolTip(weapon, canvas));
    }

    // Create a coroutine to disable the toolTip after a set amount of time
    IEnumerator DisableToolTip(GameObject weapon, GameObject canvas)
    {
        yield return new WaitForSeconds(3f);
        toolTipObjects.Remove(weapon);
        Destroy(canvas);
        toolTipCanvases.Remove(canvas);
    }

}
