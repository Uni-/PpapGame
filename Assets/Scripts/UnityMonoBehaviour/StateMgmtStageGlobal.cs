using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMgmtStageGlobal : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text ScoreText;

    private int savedscore=0;
    private int handscore=0;

    private Dictionary<string, int> combo = new Dictionary<string, int>()
    {
        { "P", 10},
        { "Ap", 20},
        { "Pap", 20},
        { "PAp", 60},
        { "ApP", 40},
        { "PPap", 70},
        { "PapP", 45},
        { "PApPapP", 450},
        { "PPapApP", 300},
    };

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<StateMgmtHand>().OnHandItemChange += ScoreCal;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score : ".Insert(8, ""+(savedscore + handscore));  
    }

    void ScoreCal(HandItemEventArgs args)
    {
        switch(args.eventType)
        {
            case HandItemEventType.Add:
                // recompute handscore
                handscore = 0;
                if (combo.ContainsKey(args.LhT))
                {
                    handscore += combo[args.LhT];
                }
                if (combo.ContainsKey(args.RhT))
                {
                    handscore += combo[args.RhT];
                }
                break;
            case HandItemEventType.Purge:
                // add savedscore
                if (combo.ContainsKey(args.Purged))
                {
                    Debug.Log("args : " + args.Purged);
                    savedscore += combo[args.Purged];
                }

                // recompute handscore
                handscore = 0;
                if (combo.ContainsKey(args.LhT))
                {
                    handscore += combo[args.LhT];
                }
                if (combo.ContainsKey(args.RhT))
                {
                    handscore += combo[args.RhT];
                }

                break;
        }
    }
}
