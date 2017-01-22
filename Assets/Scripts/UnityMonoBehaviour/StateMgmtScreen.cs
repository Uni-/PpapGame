using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMgmtScreen : MonoBehaviour
{
    private GameObject SceneDynamic;
    private GameObject CountdownContainer;
    private GameObject LhImagesContainer;
    private GameObject RhImagesContainer;
    private GameObject MiscStatusContainer;
    private GameObject ResultContainer;

    // Use this for initialization
    void Start()
    {
        SceneDynamic = GameObject.Find("SceneDynamic");
        CountdownContainer = GameObject.Find("CountdownContainer");
        LhImagesContainer = GameObject.Find("LhImagesContainer");
        RhImagesContainer = GameObject.Find("RhImagesContainer");
        MiscStatusContainer = GameObject.Find("MiscStatusContainer");
        ResultContainer = GameObject.Find("ResultContainer");

        ResultContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCountdownScreen()
    {
        SceneDynamic.GetComponent<TargetAppearDrop>().IsDropping = false;
        CountdownContainer.SetActive(true);
        LhImagesContainer.SetActive(true);
        RhImagesContainer.SetActive(true);
        MiscStatusContainer.SetActive(false);
        ResultContainer.SetActive(false);
    }

    public void SetGameplayScreen()
    {
        SceneDynamic.GetComponent<TargetAppearDrop>().IsDropping = true;
        CountdownContainer.SetActive(false);
        LhImagesContainer.SetActive(true);
        RhImagesContainer.SetActive(true);
        MiscStatusContainer.SetActive(true);
        ResultContainer.SetActive(false);
    }
    
    public void SetGameEndScreen()
    {
        ResultContainer.SetActive(true);

        SceneDynamic.GetComponent<TargetAppearDrop>().IsDropping = false;
        CountdownContainer.SetActive(false);
        LhImagesContainer.SetActive(false);
        RhImagesContainer.SetActive(false);
        MiscStatusContainer.SetActive(true);
        ResultContainer.SetActive(true);
    }
}
