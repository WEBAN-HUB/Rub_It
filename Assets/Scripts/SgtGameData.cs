using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CMVIEW
{
    SIDE = 0,

    TOP = 1,

    TITLE = 2
};

public class SgtGameData 
{
    Vector3 GVec = Vector3.zero;

    public float GameSpeed = 0.80f;

    static private SgtGameData mpInstance = null;

    public CMVIEW CAMERAVIEW = CMVIEW.TITLE;

    bool IsPlaying = false;

    public bool Pause = false;

    public int CharIndex = 0;

    public int Stage = 1;


    private SgtGameData()
    {
        GVec = Physics.gravity;
    }

    static public SgtGameData GetInstance()
    {
        if(mpInstance == null)
        {
            mpInstance = new SgtGameData();
        }

        return mpInstance;
    }

    int Play_Score = 0;
    int Best_Score = 0;
    
    float Play_Time = 0;
    float Best_Time = 0;

    public void Save_Score(int tScore, float tTime)
    {
        Play_Score = tScore;
        Play_Time = tTime;

        if(Play_Score > Best_Score)
        {
            Best_Score = Play_Score;
        }
        if(Play_Time > Best_Time)
        {
            Best_Time = Play_Time;
        }
    }

    public int Get_Play_Score()
    {
        return Play_Score;
    }
    public int Get_Best_Score()
    {
        return Best_Score;
    }
    public float Get_Play_Time()
    {
        return Play_Time;
    }
    public float Get_Best_Time()
    {
        return Best_Time;
    }

    public void TimeChange()
    {
        if (Pause == true)
        {
            Time.timeScale = 1f;
            Pause = false;
        }
        else if (Pause == false)
        {
            Time.timeScale = 0f;
            Pause = true;
        }
    }

    public bool GetIsPlaying()
    {
        return IsPlaying;
    }

    public void SetIsPlaying(bool SetBool)
    {
        IsPlaying = SetBool;
    }

    public Vector3 GetGVec()
    {
        return GVec;
    }

    public void SetTimeScale()
    {
        Time.timeScale = GameSpeed;
    }

}
