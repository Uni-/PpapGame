using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMgmtTimer : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text CountdownText;
    [SerializeField]
    UnityEngine.UI.Text TimerText;

    float timeStarted, timeLeft;

    public bool GameEnd = false;

    // Use this for initialization
    void Start()
    {
        timeStarted = Time.time;
        timeLeft = StateMgmtStageGlobalDriver.PlayDuration + StateMgmtStageGlobalDriver.CountdownDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnd == false)
        {
            float f = timeLeft - (Time.time - timeStarted);
            if (f < 0) f = 0;

            TimerText.text = f.ToString("F1");

            if (f == 0f)
            {
                GameEnd = true;
            }
        }
    }

    public void SetCountdownRemainSeconds(int remainSeconds)
    {
        CountdownText.text = remainSeconds.ToString();
    }
}
