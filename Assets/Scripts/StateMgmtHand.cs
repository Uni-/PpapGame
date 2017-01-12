using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMgmtHand : MonoBehaviour
{
    public enum Type
    {
        None,
        Left,
        Right,
        Tee,
    }

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
                    LhText.text += targetPropertySet.ToString();
                }
                break;
            case StateMgmtHand.Type.Right:
                {
                    rightHand.Add(targetPropertySet);
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
