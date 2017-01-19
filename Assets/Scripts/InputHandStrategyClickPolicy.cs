using System.Collections.Generic;
using UnityEngine;

// this class is created for duct-tape first, refactoring later
public class InputHandStrategyClickPolicy : MonoBehaviour
{
    private Vector3 inputPositionRawValue;

    private readonly List<int?> positionalInputIndices = new List<int?>();

    /// <summary>
    /// Maps from positionalInputIndeces elt? or null to inputPositionRawValue or null
    /// </summary>
    public List<Vector3?> InputPositionValues
    {
        get
        {
            List<Vector3?> inputPositionValues = new List<Vector3?>();
            foreach (int? i in positionalInputIndices)
            {
                if (i.HasValue)
                {
                    inputPositionValues.Add(inputPositionRawValue);
                }
                else
                {
                    inputPositionValues.Add(null);
                }
            }
            return inputPositionValues;
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputPositionRawValue = Input.mousePosition;

        var positionalInputIndicesPrev = new List<int?>(positionalInputIndices);

        for (int i = 0; i < 3; i++)
        {
            bool mouseButton = Input.GetMouseButton(i);
            if (mouseButton)
            {
                if (!positionalInputIndices.Contains(i))
                {
                    positionalInputIndices.Add(i);
                }
            }
            else
            {
                if (positionalInputIndices.Contains(i))
                {
                    positionalInputIndices[positionalInputIndices.IndexOf(i)] = null;
                }
            }
        }

        // IHR measures input list by its size.
        // so clear the disjoint part nulls, e.g. [1] then next frame [0] if not [null, 0], make it [1] -> [] -> [0].
        do
        {
            bool disjoint = true;
            foreach (int? x in positionalInputIndicesPrev)
            {
                if (x.HasValue && positionalInputIndices.Contains(x.Value))
                {
                    disjoint = false;
                    break;
                }
            }
            if (disjoint)
            {
                // for concept,
                // positionalInputIndices.RemoveRange(0, positionalInputIndicesPrev.Count);
                // for compatibility,
                if (positionalInputIndicesPrev.Count > 0)
                {
                    positionalInputIndices.Clear();
                }
            }
        }
        while (false);

        do
        {
            bool allNull = true;
            foreach (int? x in positionalInputIndices)
            {
                if (x.HasValue)
                {
                    allNull = false;
                    break;
                }
            }
            if (allNull)
            {
                positionalInputIndices.Clear();
            }
        }
        while (false);

        #region DUMP_positionalInputIndices
        do
        {
            var dump_s = "";
            dump_s += "[";
            bool first = true;
            foreach (var x in positionalInputIndices)
            {
                if (!first)
                {
                    dump_s += ", ";
                }
                if (!x.HasValue)
                {
                    dump_s += "null";
                }
                else
                {
                    dump_s += x.Value;
                }
                first = false;
            }
            dump_s += "]";
            Debug.Log(dump_s);
        }
        while (false);
        #endregion
    }
}
