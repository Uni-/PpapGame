using System.Collections.Generic;
using UnityEngine;

public class InputHandResponse : MonoBehaviour
{
    // For per-frame update order, this component should be *above* the current referencing InputHandResponse component
    private InputHandStrategyClickPolicy ClickPolicy;
    // For per-frame update order, this component should be *below* the current referencing InputHandResponse component
    private InputResponseStrategyFromWhereverPolicy FromWhereverPolicy;

    void Start()
    {
        ClickPolicy = GetComponent<InputHandStrategyClickPolicy>();
        FromWhereverPolicy = GetComponent<InputResponseStrategyFromWhereverPolicy>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3?> inputCoords = ClickPolicy.InputPositionValues;

        FromWhereverPolicy.InputCoords = inputCoords;
    }

}
