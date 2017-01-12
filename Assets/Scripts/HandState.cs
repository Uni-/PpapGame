using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandState : MonoBehaviour
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

    public void AddToHand(HandState.Type handType, TargetPropertySet targetPropertySet)
    {
        switch (handType)
        {
            case HandState.Type.Left:
                {
                    leftHand.Add(targetPropertySet);
                    LhText.text += targetPropertySet.ToString();
                }
                break;
            case HandState.Type.Right:
                {
                    rightHand.Add(targetPropertySet);
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
