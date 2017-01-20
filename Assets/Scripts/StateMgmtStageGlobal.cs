using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMgmtStageGlobal : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text ScoreText;

    private int savedscore=0;
    private int handscore=0;

    private Dictionary<string, int> combo;

    // Use this for initialization
    void Start()
    {
        combo = new Dictionary<string, int>();
        combo.Add("P", 10);
        combo.Add("Ap", 20);
        combo.Add("Pap", 20);
        combo.Add("PAp", 60);
        combo.Add("ApP", 40);
        combo.Add("PPap", 70);
        combo.Add("PapP", 45);
        combo.Add("PApPapP", 450);
        combo.Add("PPapApP", 300);

        GameObject.Find("SceneStatic").GetComponent<StateMgmtHand>().Addhandevent += ScoreCal;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score : ".Insert(8, ""+(savedscore + handscore));  
    }


    void ScoreCal(HandAddEventArgs args)
    {
        handscore = 0;
        if (combo.ContainsKey(args.LhT))
        {
            handscore += combo[args.LhT];
        }
        if (combo.ContainsKey(args.RhT))
        {
            handscore += combo[args.RhT];
        }
        Debug.Log("args : " + args.LhT);
        Debug.Log("handscore : " + handscore.ToString());
    }
}
