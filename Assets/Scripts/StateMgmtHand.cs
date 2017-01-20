using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void HandAddEvent(HandAddEventArgs args);

public struct HandAddEventArgs
{
    public String LhT;
    public String RhT;
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

    [SerializeField]
    public UnityEngine.UI.Text LhText;
    [SerializeField]
    public UnityEngine.UI.Text RhText;

    // Here, TargetPropertySet is used as a object reference class

    private List<TargetPropertySet> leftHand = new List<TargetPropertySet>();
    private List<TargetPropertySet> rightHand = new List<TargetPropertySet>();

    // Use this for initialization
    void Start()
    {
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
                    LhText.text += targetPropertySet.gameObject.name.Replace("(Clone)","").Replace("Target1","");
                }
                break;  
            case StateMgmtHand.Type.Right:
                {
                    rightHand.Add(targetPropertySet);
                    RhText.text += targetPropertySet.gameObject.name.Replace("(Clone)", "").Replace("Target1", "");
                }
                break;
            default:
                throw new NotImplementedException();
        }

        HandAddEventArgs args = new HandAddEventArgs();
        args.LhT = LhText.text;
        args.RhT = RhText.text;
        Addhandevent(args);
    }
}
