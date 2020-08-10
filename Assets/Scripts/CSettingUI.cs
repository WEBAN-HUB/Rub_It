using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CSettingUI : MonoBehaviour
{
    public CChooseChar ChooseCharUI = null;
    public CStageSlectUI StageSlectUI = null;
    public CTitleUI TitleUI = null;
    public CAudioBundle AudioBundle = null;

    public Slider sliderEffect = null;
    public Slider sliderBGM = null;
    public Slider sliderGP = null;


    // Start is called before the first frame update
    void Start()
    {
        sliderBGM.value = CSoundMgr.Getinstance().MusicVolumeLevel * 100;
        sliderEffect.value = CSoundMgr.Getinstance().EffectVolume * 100;
        sliderGP.value = SgtGameData.GetInstance().GameSpeed * 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnStageSelect()
    {
        CSoundMgr.Getinstance().PlayBgm(5);
        StageSlectUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void DoBtnCharacterChoose()
    {
        CSoundMgr.Getinstance().PlayBgm(5);
        ChooseCharUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }

    public void DoBtnBack()
    {
        CSoundMgr.Getinstance().PlayBgm(5);
        TitleUI.gameObject.SetActive(true);
        HideUI();
    }

    public void SettingSoundBGM()
    {
        CSoundMgr.Getinstance().MusicVolumeLevel = sliderBGM.value / 100;
        CSoundMgr.Getinstance().SetMusicVolume();
    }

    public void SettingSoundEffect()
    {
        CSoundMgr.Getinstance().EffectVolume = sliderEffect.value / 100;
        CSoundMgr.Getinstance().SetEffectVolume();
    }

    public void SettingGameSpeed()
    {
        SgtGameData.GetInstance().GameSpeed = sliderGP.value / 100;
        SgtGameData.GetInstance().SetTimeScale();
    }
}
