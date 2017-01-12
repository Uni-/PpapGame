using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour, System.Collections.Generic.IEnumerator<UnityEngine.Object>
{
    public UnityEngine.Object[] targets;

    private int count = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public UnityEngine.Object Current
    {
        get
        {
            if (count < targets.Length)
            {
                return targets[count];
            }
            else
            {
                return targets[new System.Random().Next(0, targets.Length)];
            }
        }
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        ++count;
        return true;
    }

    public void Reset()
    {
    }

}
