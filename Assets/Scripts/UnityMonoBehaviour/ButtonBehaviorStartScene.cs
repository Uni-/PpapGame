using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviorStartScene : MonoBehaviour
{
    [SerializeField]
    string SceneName;

    // Use this for initialization
    void Start()
    {
        UnityEngine.UI.Button btn = gameObject.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(OnStartButtonClick);
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(SceneName);
    }
}
