using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundMgr 
{
    private static CSoundMgr mInstance = null;

    public CAudioBundle mAudioBundle = null;

    public bool SoundLoadOnce = false;

    public float MusicVolumeLevel = 0.5f;
    public float EffectVolume = 0.5f;
    private CSoundMgr()
    {

    }
    public static CSoundMgr Getinstance()
    {
        if (mInstance == null)
        {
            mInstance = new CSoundMgr();
        }

        return mInstance;
    }

   
    public void SetAudioBundle(CAudioBundle tBundle)
    {
        mAudioBundle = tBundle;

        GameObject.DontDestroyOnLoad(mAudioBundle);
    }

    public void PlayBgm(int Index)
    {
        mAudioBundle.mArray[Index].Play();
    }

    public void StopBgm(int Index)
    {
        mAudioBundle.mArray[Index].Stop();
    }
    public void SetMusicVolume()
    {
        //MusicVolumeLevel = Level/100;

        mAudioBundle.mArray[0].volume = MusicVolumeLevel * 0.6f;
        mAudioBundle.mArray[4].volume = MusicVolumeLevel;

    }

    public void SetEffectVolume()
    {
        //EffectVolume = Level / 100;

        mAudioBundle.mArray[1].volume = EffectVolume;
        mAudioBundle.mArray[2].volume = EffectVolume;
        mAudioBundle.mArray[3].volume = EffectVolume;
        mAudioBundle.mArray[5].volume = EffectVolume;
    }


    public void MusicAllStop()
    {
        for (int ti = 0; ti < mAudioBundle.mArray.Length; ti++)
        {
            CSoundMgr.Getinstance().StopBgm(ti);

        }
    }

    public bool IsPlaying(int i)
    {
        return mAudioBundle.mArray[i].isPlaying;
    }

}
