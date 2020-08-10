using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudioBundle : MonoBehaviour
{
    public AudioSource[] mArray = new AudioSource[6];
    void Start()
    {
        if (false == CSoundMgr.Getinstance().SoundLoadOnce)
        {
            CSoundMgr.Getinstance().SoundLoadOnce = true;
            CSoundMgr.Getinstance().SetAudioBundle(this);
        }

        CSoundMgr.Getinstance().MusicAllStop();
        CSoundMgr.Getinstance().PlayBgm(4);
        CSoundMgr.Getinstance().SetEffectVolume();
        CSoundMgr.Getinstance().SetMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if(SgtGameData.GetInstance().GetIsPlaying() == true && CSoundMgr.Getinstance().IsPlaying(0) == false && SgtGameData.GetInstance().CAMERAVIEW != CMVIEW.TITLE)
        {
            CSoundMgr.Getinstance().StopBgm(4);
            CSoundMgr.Getinstance().PlayBgm(0);
        }
        
        if(SgtGameData.GetInstance().GetIsPlaying() == false && CSoundMgr.Getinstance().IsPlaying(4) == false)
        {
            CSoundMgr.Getinstance().StopBgm(0);
            CSoundMgr.Getinstance().PlayBgm(4);
        }
    }
}
