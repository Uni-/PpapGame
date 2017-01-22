using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMgmtStageGlobalDriver : MonoBehaviour
{

    private StateMgmtHand HandStateManager;
    private StateMgmtScoring ScoringStateManager;
    private StateMgmtTimer TimerStateManager;
    private StateMgmtScreen ScreenStateManager;

    public float sceneStartTime;

    public const int CountdownDuration = 3;
    public const int PlayDuration = 40;

    // Use this for initialization
    void Start()
    {
        HandStateManager = gameObject.GetComponent<StateMgmtHand>();
        ScoringStateManager = gameObject.GetComponent<StateMgmtScoring>();
        TimerStateManager = gameObject.GetComponent<StateMgmtTimer>();
        ScreenStateManager = gameObject.GetComponent<StateMgmtScreen>();

        sceneStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - sceneStartTime <= 3f)
        {
            ScreenStateManager.SetCountdownScreen();
            TimerStateManager.SetCountdownRemainSeconds(CountdownDuration - (int)Mathf.Floor(Time.time - sceneStartTime));
        }
        else if (!TimerStateManager.GameEnd)
        {
            ScreenStateManager.SetGameplayScreen();
        }
        else
        {
            ScreenStateManager.SetGameEndScreen();
        }
    }
}
