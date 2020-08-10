using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
public class CStageSlectUI : MonoBehaviour
{
    public CSettingUI SettingUI = null;
    public Camera mCamera = null;
    public RectTransform mRect = null;

    public Material MtlBack = null;
    
 
    // Start is called before the first frame update
    void Start()
    {
        int stage = SgtGameData.GetInstance().Stage;

        mRect.anchoredPosition = new Vector3(1080 - (SgtGameData.GetInstance().Stage * 1080), -8, 0);

   
    }
    void Update()
    {
       

    }


    public void BtnStage(int i)
    {
        if (SgtGameData.GetInstance().Stage != i)
        {
            SgtGameData.GetInstance().Stage = i;

            CMapStart tMapstart = FindObjectOfType<CMapStart>();

            tMapstart.StageSelect();
            tMapstart.LoadMap();


            if (SgtGameData.GetInstance().Stage == 1)
            {
                mCamera.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0);
                MtlBack.color = new Color(0f, 0f, 0f, 0);
            }
            else if (SgtGameData.GetInstance().Stage == 2)
            {
                mCamera.backgroundColor = new Color(0.35f, 0.25f, 0.55f, 0);
                MtlBack.color = new Color(0.35f, 0.25f, 0.55f, 0);
            }
            else if (SgtGameData.GetInstance().Stage == 3)
            {
                mCamera.backgroundColor = new Color(0, 0.3f, 0.5f, 0);
                MtlBack.color = new Color(0, 0.3f, 0.5f, 0);
            }
            else if (SgtGameData.GetInstance().Stage == 4)
            {
                mCamera.backgroundColor = new Color(0.32f, 0.32f, 0.32f, 0);
                MtlBack.color = new Color(0.32f, 0.32f, 0.32f, 0);

            }

        }
    }
    


    public void BtnBacktoSettingUI()
    {
        SettingUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
