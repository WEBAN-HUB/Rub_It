using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTitleUI : MonoBehaviour
{
    CActor mPlayer = null;
    CTitleMgr TitleMgr = null;
    CSettingUI SettingUI = null;

    CChooseChar ChooseCharUI = null;

    CMapStart Mapstart = null;
  
    CCamera mCamera = null;

    public Camera mCamera2 = null;

    public Material MtlBack = null;

    void Start()
    {
        TitleMgr = FindObjectOfType<CTitleMgr>();
        SettingUI = FindObjectOfType<CSettingUI>();
        ChooseCharUI = FindObjectOfType<CChooseChar>();

        ChooseCharUI.HideUI();
        SettingUI.HideUI();

        CSaveFile.GetInstance().LoadFile();


        if (SgtGameData.GetInstance().Stage == 1)
        {
            mCamera2.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0);
        }
        else if (SgtGameData.GetInstance().Stage == 2)
        {
            mCamera2.backgroundColor = new Color(0.35f, 0.25f, 0.55f, 0);
        }
        else if (SgtGameData.GetInstance().Stage == 3)
        {
            mCamera2.backgroundColor = new Color(0, 0.3f, 0.5f, 0);
        }
        else if (SgtGameData.GetInstance().Stage == 4)
        {
            mCamera2.backgroundColor = new Color(0.32f, 0.32f, 0.32f, 0);

        }

        //SettingUI.sliderBGM.value = CSoundMgr.Getinstance().MusicVolumeLevel;
        //SettingUI.sliderEffect.value = CSoundMgr.Getinstance().EffectVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnPlay()
    {
        mPlayer = FindObjectOfType<CActor>();
        mCamera = FindObjectOfType<CCamera>();
        Mapstart = FindObjectOfType<CMapStart>();

        CLight tLight = FindObjectOfType<CLight>();
        tLight.DoAlive();


        mCamera.CameraTitleMoveStop();
        mPlayer.DoAlive();
        TitleMgr.HideUI();
        ChooseCharUI.HideUI();
        SettingUI.HideUI();

        SgtGameData.GetInstance().SetTimeScale();
        CSoundMgr.Getinstance().PlayBgm(5);
        SgtGameData.GetInstance().SetIsPlaying(true);
        Mapstart.Starting = true;

        CSoundMgr.Getinstance().StopBgm(4);
        //Invoke("BGMPlay", 0.5f);
    }

    public void BtnSetting()
    {
        CSoundMgr.Getinstance().PlayBgm(5);
        SettingUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void BGMPlay()
    {
        CSoundMgr.Getinstance().PlayBgm(0);
    }


    public void BtnExit()
    {
        CSaveFile.GetInstance().SaveFile();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");
#else
        Application.Quit();
#endif

    }
}
