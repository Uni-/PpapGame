using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandResponse : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject sceneDynamicContainer;

    [SerializeField]
    public HandState HandSystem;

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
                Debug.Log(position.ToString());
                break;
            }
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            relativeInputCoordinates.Add(new Vector3(touch.position.x, touch.position.y));
            Debug.Log(touch.position.ToString());
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
                var hitGameObject = raycastHit.transform.gameObject;
                hitGameObject.GetComponent<Renderer>().material = RedShader;
                AddObjectToList(hitGameObject);
            }
        }
    }

    void AddObjectToList(GameObject pGameObject)
    {
        Debug.Log(pGameObject);
        Debug.Log(pGameObject.GetComponent<TargetPropertySet>());

        TargetPropertySet targetPropertySet = pGameObject.GetComponent<TargetPropertySet>();
        HandSystem.AddToHand(HandState.Type.Left, targetPropertySet);
    }
}
