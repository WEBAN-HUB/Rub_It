using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class CGooglePlay : MonoBehaviour
{

    public Canvas mCanvas = null;
    public Text mTxt = null;

    string AuthCode = "";

    // Start is called before the first frame update
    private void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate();

        DontDestroyOnLoad(this);
    }


    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignIn()
    {
        if (!LoginCheck())
        {
            Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                        StartCoroutine(ShowLog("Login Success!"));
                    }
                    else
                    {
                        StartCoroutine(ShowLog("Login Failed..."));
                    }
                }
            );
        }
        else
        {
            StartCoroutine(ShowLog("Login Failed..."));
        }
    }

    public void SignOut()
    {
        if (LoginCheck())
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
        }
    }

    public void OpenReaderBoard()
    {
        if(LoginCheck())
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Social.ShowLeaderboardUI();
                    return;
                }
                else
                {
                    return;
                }
            });
        }
        else
        {
            SignIn();
        }
    }

    public void ScoreReaderBoard()
    {
        if (LoginCheck())
        { 
            Social.ReportScore(SgtGameData.GetInstance().Get_Best_Score(), GPGSIds.leaderboard_score,(bool success) =>
            {
                if(success)
                {
                    StartCoroutine(ShowLog("Save Score : "+SgtGameData.GetInstance().Get_Best_Score()));
                }
                else
                {
                    StartCoroutine(ShowLog("Not Save Score"));
                }
            });
        }
        else
        {
            StartCoroutine(ShowLog("Not Save Score"));
        }
    }

    bool LoginCheck()
    {
        bool tResult = false;

        if(!Social.localUser.authenticated)
        {
            tResult = false;
        }
        else
        {
            tResult = true;
        }
        return tResult;
    }

    IEnumerator ShowLog(string Log)
    {
        mTxt.text = Log;
        yield return new WaitForSeconds(3f);
        mTxt.text = "";
        StopCoroutine("ShowLog");
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            CSaveFile.GetInstance().SaveFile();
        }
    }
}
