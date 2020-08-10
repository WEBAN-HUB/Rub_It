using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CDeadUI : MonoBehaviour
{
    CPlayUI PlayUI = null;
    CMapStart MapStart = null;

    CGooglePlay mGooglePlay = null;

    public Text ScoreTxt = null;
    public Text BScoreTxt = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDeadUI()
    {
        PlayUI = FindObjectOfType<CPlayUI>();
        PlayUI.SaveScore();
        ScoreTxt.text = "SCORE: " + SgtGameData.GetInstance().Get_Play_Score().ToString();
        BScoreTxt.text = "BEST SCORE: "+SgtGameData.GetInstance().Get_Best_Score().ToString();

        CSaveFile.GetInstance().SaveFile();

        mGooglePlay = FindObjectOfType<CGooglePlay>();
        mGooglePlay.ScoreReaderBoard();
    }

    public void BtnGotoTitle()
    {


        CTitleMgr TitleMgr = FindObjectOfType<CTitleMgr>();
        MapStart = FindObjectOfType<CMapStart>();
        TitleMgr.ShowUI();

        CActor mPlayer = FindObjectOfType<CActor>();
        mPlayer.transform.position = Vector3.zero;

        mPlayer.mBody.SetActive(true);

        this.gameObject.SetActive(false);

        CLight tLight = FindObjectOfType<CLight>();
        tLight.mLight.gameObject.SetActive(true);
   
        MapStart.Starting = false;

        CSoundMgr.Getinstance().PlayBgm(5);

        CSoundMgr.Getinstance().PlayBgm(4);

        Physics.gravity = SgtGameData.GetInstance().GetGVec();

        SgtGameData.GetInstance().SetIsPlaying(false);

        SceneManager.UnloadSceneAsync("ScenePlay");
        SceneManager.LoadScene("ScenePlay", LoadSceneMode.Additive);

    }
}
