using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class InputHandResponse : MonoBehaviour
{
    [SerializeField]
    Material WhiteShader;

    // These all components should be above the referencing InputHandResponse component in Editor.
    [SerializeField]
    InputHandStrategyClickPolicy ClickPolicy;
    [SerializeField]
    InputHandStrategyTouchPolicy TouchPolicy;
    [SerializeField]
    StateMgmtHand HandSystem;

    private Camera mainCamera;
    private GameObject sceneDynamicContainer;
    private int inputCoordsPrevCount = 0;
    private Vector3? firstHandStartCoord = null;
    private Vector3? secondHandStartCoord = null;
    private bool isFirstHandSet
    {
        get
        {
            return firstHandStartCoord != null;
        }
        set
        {
            if (value == true) throw new System.InvalidOperationException();
            firstHandStartCoord = null;
        }
    }
    private bool isSecondHandSet
    {
        get
        {
            return secondHandStartCoord != null;
        }
        set
        {
            if (value == true) throw new System.InvalidOperationException();
            secondHandStartCoord = null;
        }
    }

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        //Camera[] allCameras = Camera.allCameras;
        sceneDynamicContainer = GameObject.Find("SceneDynamic");
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3?> inputCoords = ClickPolicy.InputPositionValues;

        if (inputCoordsPrevCount == 0 && inputCoords.Count > 0)
        {
            System.Diagnostics.Debug.Assert(inputCoords[0].HasValue);
            UnityEngine.Debug.Assert(inputCoords[0].HasValue);

            SetFirstHand(inputCoords[0].Value);
        }
        else if (inputCoordsPrevCount == 1 && inputCoords.Count > 1)
        {
            if (inputCoords[0].HasValue)
            {
                System.Diagnostics.Debug.Assert(inputCoords[1].HasValue);
                UnityEngine.Debug.Assert(inputCoords[1].HasValue);

                SetSecondHand(inputCoords[1].Value);
            }
        }
        else if (inputCoordsPrevCount > 1 && inputCoords.Count > inputCoordsPrevCount)
        {
            // do nothing
        }
        else if (inputCoords.Count == 0)
        {
            UnsetHands();
        }
        else
        {
            // inputCoordsPrevCount == inputCoords.Count
            // do nothing
        }
        inputCoordsPrevCount = inputCoords.Count;

        if (inputCoords.Count > 0 && inputCoords[0].HasValue)
        {
            CareFirstHand(inputCoords[0].Value);

            if (inputCoords.Count > 1 && inputCoords[1].HasValue)
            {
                CareBothHand(inputCoords[0].Value, inputCoords[1].Value);
            }
        }
    }

    void AddObjectToList(Vector3 handCoord, GameObject pGameObject)
    {
        //Debug.Log(pGameObject.GetComponent<TargetPropertySet>());

        TargetPropertySet targetPropertySet = pGameObject.GetComponent<TargetPropertySet>();
        StateMgmtHand.Type type;
        if (handCoord.x < Screen.width / 2)
            type = StateMgmtHand.Type.Left;
        else
            type = StateMgmtHand.Type.Right;

        HandSystem.AddToHand(type, targetPropertySet);
    }

    void CareFirstHand(Vector3 firstHandCoord)
    {
        if (!isFirstHandSet)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(firstHandCoord);
        ray.origin = mainCamera.transform.position;
        RaycastHit raycastHit;

        bool hitObjectExists = Physics.Raycast(ray.origin, ray.direction, out raycastHit);
        if (hitObjectExists)
        {
            var hitGameObject = raycastHit.transform.gameObject;
            hitGameObject.GetComponent<Renderer>().material = WhiteShader;
            AddObjectToList(firstHandStartCoord.Value, hitGameObject);
            UnsetHands();
        }
    }

    void CareBothHand(Vector2 firstHandStartCoord, Vector3 secondHandStartCoord)
    {
        if (!isSecondHandSet)
        {
            return;
        }
    }

    void SetFirstHand(Vector3 firstHandCurrCoord)
    {
        firstHandStartCoord = firstHandCurrCoord;
    }

    void SetSecondHand(Vector3 secondHandCurrCoord)
    {
        secondHandStartCoord = secondHandCurrCoord;
    }

    void UnsetHands()
    {
        isFirstHandSet = false;
        isSecondHandSet = false;
    }
}
