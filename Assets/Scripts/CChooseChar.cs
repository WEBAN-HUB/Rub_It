using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CChooseChar : MonoBehaviour
{

    CActor mActor = null;
    int mIndex = 0;
    public Button RightButton = null;
    public Button LeftButton = null;
    public CSettingUI SettingUI = null;

    // Start is called before the first frame update
    void Start()
    {
        mIndex = SgtGameData.GetInstance().CharIndex;
        if(mIndex <= 0)
        {
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(true);
        }
        else if(mIndex >= mActor.BodyArray.Count-1)
        {
            RightButton.gameObject.SetActive(false);
            LeftButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }

    void DoFindActor()
    {
        mActor = FindObjectOfType<CActor>();
    }

    public void DoBtnRight()
    {
        DoFindActor();
        mActor.BodyArray[mIndex].gameObject.SetActive(false);
        mIndex++;
        mActor.BodyArray[mIndex].gameObject.SetActive(true);
        if(mIndex >= mActor.BodyArray.Count-1)
        {
            RightButton.gameObject.SetActive(false);
        }
        LeftButton.gameObject.SetActive(true);

        DoChangeColor();
    }

    public void DoBtnLeft()
    {
        DoFindActor();
        mActor.BodyArray[mIndex].gameObject.SetActive(false);
        mIndex--;
        mActor.BodyArray[mIndex].gameObject.SetActive(true);
        if(mIndex <= 0)
        {
            LeftButton.gameObject.SetActive(false);
        }
        RightButton.gameObject.SetActive(true);

        DoChangeColor();
    }
    
    void DoChangeColor()
    {
        SgtGameData.GetInstance().CharIndex = mIndex;
        CLight tLight = FindObjectOfType<CLight>();
        tLight.ChangeColor();
    }

    public void DoBtnBack()
    {
        SettingUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

        CSoundMgr.Getinstance().PlayBgm(5);
    }

}
