using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandResponse : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject sceneDynamicContainer;

    [SerializeField]
    Material WhiteShader;
    [SerializeField]
    Material RedShader;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        Camera[] allCameras = Camera.allCameras;

        sceneDynamicContainer = GameObject.Find("SceneDynamic");
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3> relativeInputCoordinates = new List<Vector3>();

        for (int i = 0; i < 3; i++)
        {
            bool mouse = Input.GetMouseButton(i);
            Vector3 position = Input.mousePosition;
            if (mouse)
            {
                relativeInputCoordinates.Add(position);
                break;
            }
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            relativeInputCoordinates.Add(new Vector3(touch.position.x, touch.position.y));
            Debug.Log(touch);
        }

        for (int i = 0; i < sceneDynamicContainer.transform.childCount; i++)
        {
            GameObject gameObject = sceneDynamicContainer.transform.GetChild(i).gameObject;
        }

        foreach (Vector3 vectorItem in relativeInputCoordinates)
        {
            Ray ray = mainCamera.ScreenPointToRay(vectorItem);
            ray.origin = mainCamera.transform.position;
            RaycastHit raycastHit;
            bool hitObjectExists = Physics.Raycast(ray.origin, ray.direction, out raycastHit);
            if (hitObjectExists)
            {
                raycastHit.transform.gameObject.GetComponent<Renderer>().material = RedShader;
            }
        }
    }
}
