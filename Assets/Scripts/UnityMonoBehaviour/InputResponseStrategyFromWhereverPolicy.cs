﻿using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class InputResponseStrategyFromWhereverPolicy : MonoBehaviour
{
    [SerializeField]
    StateMgmtHand HandSystem = null;
    [SerializeField]
    ScreenInputTrailer ScreenInputTrailerLh = null;
    [SerializeField]
    ScreenInputTrailer ScreenInputTrailerRh = null;

    private int inputCoordsPrevCount = 0;
    private Vector3? firstHandStartCoord = null;
    private Vector3? secondHandStartCoord = null;

    public List<Vector3?> InputCoords = new List<Vector3?>();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CareHandSystem();
        CareScreenTrailer();
    }

    private void CareHandSystem()
    {
        if (inputCoordsPrevCount == 0 && InputCoords.Count > 0)
        {
            System.Diagnostics.Debug.Assert(InputCoords[0].HasValue);
            UnityEngine.Debug.Assert(InputCoords[0].HasValue);

            HandSystem.SetFirstHandStart(InputCoords[0].Value);
            firstHandStartCoord = InputCoords[0].Value;
        }
        else if (inputCoordsPrevCount == 1 && InputCoords.Count > 1)
        {
            if (InputCoords[0].HasValue)
            {
                System.Diagnostics.Debug.Assert(InputCoords[1].HasValue);
                UnityEngine.Debug.Assert(InputCoords[1].HasValue);

                HandSystem.SetSecondHandStart(InputCoords[1].Value);
                secondHandStartCoord = InputCoords[1].Value;
            }
        }
        else if (inputCoordsPrevCount > 1 && InputCoords.Count > inputCoordsPrevCount)
        {
            // do nothing
        }
        else if (InputCoords.Count == 0)
        {
            HandSystem.UnsetHandsStart();
        }
        else
        {
            // inputCoordsPrevCount == inputCoords.Count
            // do nothing
        }
        inputCoordsPrevCount = InputCoords.Count;

        if (InputCoords.Count > 0 && InputCoords[0].HasValue)
        {
            HandSystem.SetFirstHand(InputCoords[0].Value);

            if (InputCoords.Count > 1 && InputCoords[1].HasValue)
            {
                HandSystem.SetSecondHand(InputCoords[1].Value);
            }
        }
    }

    private void CareScreenTrailer()
    {
        if (inputCoordsPrevCount > 0)
        {
            Vector3 tmp1 = Input.mousePosition;
            tmp1.z = Camera.main.transform.localPosition.z * -1f;
            Vector3 v = Camera.main.ScreenToWorldPoint(tmp1);
            v.z = 0f;

            if (firstHandStartCoord.Value.x < Screen.width / 2)
            {
                ScreenInputTrailerLh.gameObject.transform.localPosition = v;
                ScreenInputTrailerLh.TurnOn(v);
            }
            else
            {
                ScreenInputTrailerRh.gameObject.transform.localPosition = v;
                ScreenInputTrailerRh.TurnOn(v);
            }

            if (inputCoordsPrevCount > 1)
            {
                Vector3 tmp2 = Input.mousePosition;
                tmp2.z = Camera.main.transform.localPosition.z * -1f;
                Vector3 u = Camera.main.ScreenToWorldPoint(tmp2);
                u.z = 0f;

                if (secondHandStartCoord.Value.x < Screen.width / 2)
                {
                    ScreenInputTrailerLh.gameObject.transform.localPosition = u;
                    ScreenInputTrailerLh.TurnOn(v);
                }
                else
                {
                    ScreenInputTrailerRh.gameObject.transform.localPosition = u;
                    ScreenInputTrailerRh.TurnOn(v);
                }
            }
        }
        else
        {
            ScreenInputTrailerLh.TurnOff();
            ScreenInputTrailerRh.TurnOff();
        }
    }
}
