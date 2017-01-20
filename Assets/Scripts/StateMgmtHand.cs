using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void HandAddEvent(HandAddEventArgs args);

public struct HandAddEventArgs
{
    public string LhT;
    public string RhT;
}

public class StateMgmtHand : MonoBehaviour
{
    public enum Type
    {
        None,
        Left,
        Right,
        Tee,
    }
    

    public HandAddEvent Addhandevent;
    private string LhText = "";
    private string RhText = "";

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

    // Use this for initialization
    void Start()
    {
        PurgeLh();
        PurgeRh();
    }

    // Update is called once per frame
    void Update()
    {
    }


    
    
    public void AddToHand(StateMgmtHand.Type handType, TargetPropertySet targetPropertySet)
    {
        switch (handType)
        {
            case StateMgmtHand.Type.Left:
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
            case StateMgmtHand.Type.Right:
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
        HandAddEventArgs args = new HandAddEventArgs();
        args.LhT = LhText;
        args.RhT = RhText;
        Addhandevent(args);
    }

    public void PurgeLh()
    {
        for (int i = 0; i < 4; i++)
        {
            GetLhImage(i).gameObject.SetActive(false);
        }
        LhNonceImage.gameObject.SetActive(false);

        leftHand.Clear();
    }

    public void PurgeRh()
    {
        for (int i = 0; i < 4; i++)
        {
            GetRhImage(i).gameObject.SetActive(false);
        }
        RhNonceImage.gameObject.SetActive(false);

        rightHand.Clear();
    }
}
