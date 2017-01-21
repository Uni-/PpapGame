using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HandItemEventArgs
{
    public HandItemEventType eventType;

    public HandType handType;
    public string entered;

    public string LhT;
    public string RhT;
    public string Purged;
}

public class StateMgmtHand : MonoBehaviour
{
    public HandItemEvent OnHandItemChange;

    [SerializeField]
    UnityEngine.UI.Button LhPurgeButton;
    [SerializeField]
    UnityEngine.GameObject LhImages;
    [SerializeField]
    UnityEngine.UI.RawImage LhNonceImage;
    [SerializeField]
    public UnityEngine.UI.Text LhMoreText;

    UnityEngine.UI.RawImage GetLhImage(int index)
    {
        return LhImages.transform.GetChild(index).GetComponent<UnityEngine.UI.RawImage>();
    }

    [SerializeField]
    UnityEngine.UI.Button RhPurgeButton;
    [SerializeField]
    UnityEngine.GameObject RhImages;
    [SerializeField]
    UnityEngine.UI.RawImage RhNonceImage;
    [SerializeField]
    public UnityEngine.UI.Text RhMoreText;

    UnityEngine.UI.RawImage GetRhImage(int index)
    {
        return RhImages.transform.GetChild(index).GetComponent<UnityEngine.UI.RawImage>();
    }

    [SerializeField]
    Texture Target1P;
    [SerializeField]
    Texture Target1Ap;
    [SerializeField]
    Texture Target1Pap;

    [SerializeField]
    Dictionary<string, Texture> TargetTextures
    {
        get
        {
            return new Dictionary<string, Texture>
            {
                { "1P", Target1P },
                { "1Ap", Target1Ap },
                { "1Pap", Target1Pap },
            };
        }
    }

    // Here, TargetPropertySet is used as a object reference class

    private List<TargetPropertySet> leftHand = new List<TargetPropertySet>();
    private List<TargetPropertySet> rightHand = new List<TargetPropertySet>();
    
    private Camera mainCamera;

    private string LhText = "";
    private string RhText = "";

    private Vector3? firstHandStartCoord = null;
    private Vector3? secondHandStartCoord = null;
    private bool isFirstHandSetStart
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
    private bool isSecondHandSetStart
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
    private Vector3? firstHandCurrCoord = null;
    private Vector3? secondHandCurrCoord = null;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        //Camera[] allCameras = Camera.allCameras;

        PurgeLh();
        PurgeRh();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstHandCurrCoord.HasValue)
        {
            CareFirstHand(firstHandCurrCoord.Value);
            if (secondHandCurrCoord.HasValue)
            {
                CareBothHand(firstHandCurrCoord.Value, secondHandCurrCoord.Value);
            }
        }
    }

    void AddObjectToList(Vector3 handCoord, GameObject pGameObject)
    {
        //Debug.Log(pGameObject.GetComponent<TargetPropertySet>());

        TargetPropertySet targetPropertySet = pGameObject.GetComponent<TargetPropertySet>();
        HandType type;
        if (handCoord.x < Screen.width / 2)
            type = HandType.Left;
        else
            type = HandType.Right;

        AddToHand(type, targetPropertySet);
    }

    void CareFirstHand(Vector3 firstHandCoord)
    {
        if (!isFirstHandSetStart)
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
            AddObjectToList(firstHandStartCoord.Value, hitGameObject);
            UnsetHandsStart();
            GotObject(hitGameObject);
        }
    }

    void CareBothHand(Vector2 firstHandStartCoord, Vector3 secondHandStartCoord)
    {
        if (!isSecondHandSetStart)
        {
            return;
        }
    }

    public void SetFirstHandStart(Vector3 firstHandCurrCoord)
    {
        firstHandStartCoord = firstHandCurrCoord;
    }

    public void SetSecondHandStart(Vector3 secondHandCurrCoord)
    {
        secondHandStartCoord = secondHandCurrCoord;
    }

    public void UnsetHandsStart()
    {
        isFirstHandSetStart = false;
        isSecondHandSetStart = false;
    }

    public void SetFirstHand(Vector3 firstHandCoord)
    {
        firstHandCurrCoord = firstHandCoord;
    }

    public void SetSecondHand(Vector3 secondHandCoord)
    {
        secondHandCurrCoord = secondHandCoord;
    }

    void GotObject(GameObject hitGameObject)
    {
        hitGameObject.SetActive(false);
        hitGameObject.GetComponent<TargetDisappear>().DestroySelf();
    }

    public void AddToHand(HandType handType, TargetPropertySet targetPropertySet)
    {
        switch (handType)
        {
            case HandType.Left:
                {
                    leftHand.Add(targetPropertySet);
                    LhText += targetPropertySet.gameObject.name.Replace("(Clone)","").Replace("Target1","");

                    if (leftHand.Count > 4)
                    {
                        LhMoreText.text = "+" + (leftHand.Count - 4).ToString();
                        LhNonceImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        UnityEngine.UI.RawImage lhImage = GetLhImage(leftHand.Count - 1);
                        lhImage.texture = TargetTextures[targetPropertySet.Name];
                        lhImage.gameObject.SetActive(true);
                    }
                }
                break;  
            case HandType.Right:
                {
                    rightHand.Add(targetPropertySet);
                    RhText += targetPropertySet.gameObject.name.Replace("(Clone)", "").Replace("Target1", "");

                    if (rightHand.Count > 4)
                    {
                        RhMoreText.text = "+" + (rightHand.Count - 4).ToString();
                        RhNonceImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        UnityEngine.UI.RawImage rhImage = GetRhImage(rightHand.Count - 1);
                        rhImage.texture = TargetTextures[targetPropertySet.Name];
                        rhImage.gameObject.SetActive(true);
                    }

                }
                break;
            default:
                throw new NotImplementedException();
        }

        HandItemEventArgs args = new HandItemEventArgs()
        {
            eventType = HandItemEventType.Add,
            handType = handType,
            entered = targetPropertySet.Name,
            LhT = LhText,
            RhT = RhText
        };

        if (OnHandItemChange != null)
            OnHandItemChange(args);
    }

    public void PurgeLh()
    {
        for (int i = 0; i < 4; i++)
        {
            GetLhImage(i).gameObject.SetActive(false);
        }
        LhNonceImage.gameObject.SetActive(false);

        string purged_string = LhText;
        LhText = "";
        leftHand.Clear();
        
        HandItemEventArgs args = new HandItemEventArgs()
        {
            eventType = HandItemEventType.Purge,
            handType = HandType.Left,
            entered = "",
            LhT = LhText,
            RhT = RhText,
            Purged = purged_string
        };

        if (OnHandItemChange != null)
            OnHandItemChange(args);
    }

    public void PurgeRh()
    {
        for (int i = 0; i < 4; i++)
        {
            GetRhImage(i).gameObject.SetActive(false);
        }
        RhNonceImage.gameObject.SetActive(false);

        string purged_string = RhText;
        RhText = "";
        rightHand.Clear();

        HandItemEventArgs args = new HandItemEventArgs()
        {
            eventType = HandItemEventType.Purge,
            handType = HandType.Right,
            entered = "",
            LhT = LhText,
            RhT = RhText,
            Purged = purged_string
        };

        if (OnHandItemChange != null)
            OnHandItemChange(args);
    }
}
