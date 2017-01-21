using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMgmtStageGlobalDriver : MonoBehaviour {

    private StateMgmtHand HandStateManager;
    private StateMgmtScoring ScoringStateManager;
    private StateMgmtTimer TimerStateManager;
    private StateMgmtScreen ScreenStateManager;

	// Use this for initialization
	void Start () {
        HandStateManager = gameObject.GetComponent<StateMgmtHand>();
        ScoringStateManager = gameObject.GetComponent<StateMgmtScoring>();
        TimerStateManager = gameObject.GetComponent<StateMgmtTimer>();
        ScreenStateManager = gameObject.GetComponent<StateMgmtScreen>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
