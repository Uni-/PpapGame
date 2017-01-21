using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzLogger : MonoBehaviour
{
    [SerializeField]
    string LogContent;

    private int count = 0;

    public void Log()
    {
        Debug.Log(string.Format(LogContent, count));
    }
}
