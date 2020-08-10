using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CPlayUI : MonoBehaviour
{
    int Score = 0;
    float ScoreIncrease = 3;
    float mTime = 0;
    bool Playing = false;

    public Text Play_ScoreTxt = null;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ScoreUp();
        
        Play_ScoreTxt.text = "SCORE: "+Score.ToString();
    }

    void ScoreUp()
    {
        Score = Score + (int)Mathf.Floor(ScoreIncrease*SgtGameData.GetInstance().GameSpeed)+SgtGameData.GetInstance().Stage;
        mTime = mTime + Time.deltaTime;
    }

    public void SaveScore()
    {
        SgtGameData.GetInstance().Save_Score(Score, mTime);
        this.gameObject.SetActive(false);
    }
    public void ResetScore()
    {
        Score = 0;
        mTime = 0;
    }
}
