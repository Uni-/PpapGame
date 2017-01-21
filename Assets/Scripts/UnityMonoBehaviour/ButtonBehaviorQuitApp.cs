using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviorQuitApp : MonoBehaviour
{
   // Use this for initialization
    void Start()
    {
        UnityEngine.UI.Button btn = gameObject.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(OnQuitButtonClick);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
