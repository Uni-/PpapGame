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

    // Here, ObjectPropertySet is used as a object reference class

    private List<ObjectPropertySet> leftHand = new List<ObjectPropertySet>();
    private List<ObjectPropertySet> rightHand = new List<ObjectPropertySet>();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddToHand(HandState.Type handType, ObjectPropertySet objectPropertySet)
    {
        switch (handType)
        {
            case HandState.Type.Left:
                {
                    leftHand.Add(objectPropertySet);
                    LhText.text += objectPropertySet.ToString();
                }
                break;
            case HandState.Type.Right:
                {
                    rightHand.Add(objectPropertySet);
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
