using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

// this class is created for duct-tape first, refactoring later
public class InputResponseStrategyFromWhereverPolicy : MonoBehaviour
{
    [SerializeField]
    StateMgmtHand HandSystem;
    [SerializeField]
    ScreenInputTrailer screenInputTrailer;

    private int inputCoordsPrevCount = 0;

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
        }
        else if (inputCoordsPrevCount == 1 && InputCoords.Count > 1)
        {
            if (InputCoords[0].HasValue)
            {
                System.Diagnostics.Debug.Assert(InputCoords[1].HasValue);
                UnityEngine.Debug.Assert(InputCoords[1].HasValue);

                HandSystem.SetSecondHandStart(InputCoords[1].Value);
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
        if (inputCoordsPrevCount != 0)
        {
            Vector3 tmp = Input.mousePosition;
            tmp.z = Camera.main.transform.localPosition.z * -1f;
            Vector3 v = Camera.main.ScreenToWorldPoint(tmp);
            v.z = 0f;
            screenInputTrailer.gameObject.transform.localPosition = v;
            screenInputTrailer.TurnOn(v);
        }
        else
        {
            screenInputTrailer.TurnOff();
        }
    }
}
