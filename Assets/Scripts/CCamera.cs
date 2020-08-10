using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class CCamera : MonoBehaviour
{
    Vector3 TopVec = Vector3.zero;
    Vector3 SideVec = Vector3.zero;
    Vector3 TopRot = Vector3.zero;
    Vector3 SideRot = Vector3.zero;

    Vector3 CMoveV = Vector3.zero;

    public GameObject SideObject = null;
    CActor mPlayer = null;
    public GameObject LookPos = null;
 
    float CameraSpeed = 0.3f;


    public bool IsCameraMove = false;
    bool camerablocking = false;

    Tweener mpTweener_0 = null;

    void Start()
    {
        TopVec.x = -3;
        TopVec.y = 3;
        TopVec.z = 0;

        //SideVec.x = -3.5f;
        //SideVec.y = 0.85f;
        //SideVec.z = -2.2f;

        SideVec.x = -7f;
        SideVec.y = 4.15f;
        SideVec.z = -6.5f;

        TopRot.x = 150;
        TopRot.y = -90;
        TopRot.z = -180;

        SideRot.x = 6;
        SideRot.y = 65;
        SideRot.z = 8;

        CMoveV.x = 24f;
        CMoveV.y = -25;
        CMoveV.z = -8;

        CameraTitleSetting();

           //CameraSwitch();
    }


    void Update()
    {
        if (null != mPlayer)
        {
            if (/*mPlayer.tGVec.y > 0 &&*/ SgtGameData.GetInstance().CAMERAVIEW == CMVIEW.TOP)
            {
                //Debug.Log("dd");
                this.transform.LookAt(mPlayer.ChildLookpos.transform.position);
            }
            else
            {
                //Debug.Log("ee");
                this.transform.LookAt(LookPos.transform.position);
            }
        }
        else
        {
            mPlayer = FindObjectOfType<CActor>();
        }


    }

    public void CameraSwitch()
    {
        mPlayer = FindObjectOfType<CActor>();


        //this.Do

        switch (SgtGameData.GetInstance().CAMERAVIEW)
        {
            case CMVIEW.SIDE:

                CameraMoveTypeChange();
                //this.transform.DOLocalRotate(SideRot, CameraSpeed, RotateMode.FastBeyond360);
                this.transform.SetParent(SideObject.transform);
                this.transform.DOMove(SideVec, CameraSpeed, false);
                Invoke("CameraMoveTypeChange", 0.1f);



                break;

            case CMVIEW.TOP:

                //if (mPlayer.tGVec.y > 0)
                //{
                //    CameraMoveTypeChange();
                //    //this.transform.DOLocalRotate(TopRot, CameraSpeed, RotateMode.FastBeyond360);
                //    this.transform.SetParent(mPlayer.transform);

                //    this.transform.DOMove(mPlayer.transform.position, CameraSpeed, false);
                //    Invoke("CameraMoveTypeChange", 0.1f);
                //}
                //else
                //{
                //    CameraMoveTypeChange();
                //    //this.transform.DOLocalRotate(TopRot, CameraSpeed, RotateMode.FastBeyond360);
                //    this.transform.SetParent(mPlayer.transform);
                //    Vector3 tVec = mPlayer.transform.position;
                //    tVec.y = 0;
                //    tVec = tVec + TopVec;

                //    this.transform.DOMove(tVec, CameraSpeed, false);
                //    Invoke("CameraMoveTypeChange", 0.1f);
                //}

                CameraMoveTypeChange();
                this.transform.SetParent(mPlayer.transform);    
                this.transform.DOMove(mPlayer.transform.position, CameraSpeed, false);
                this.transform.DOLocalMove(Vector3.zero, 0.01f, false).SetDelay(0.3f);
                Invoke("CameraMoveTypeChange", 0.1f);

                break;

            case CMVIEW.TITLE:

                this.transform.SetParent(SideObject.transform);

                this.transform.DOMove(Vector3.up * 5 + Vector3.right * -2f + Vector3.forward * -5f, CameraSpeed, false);
                if(camerablocking == false)
                {
                    camerablocking = true;
                }
                {
                    Invoke("CameraTitleMovePlay", 0.5f);
                }
                break;
        }

    }


    void CameraMoveTypeChange()
    {
        if (IsCameraMove == true)
        {
            IsCameraMove = false;
        }
        else
        {
            IsCameraMove = true;
        }
    }

    void  CameraTitleSetting()
    {
        this.transform.SetParent(SideObject.transform);
        this.transform.position = Vector3.up * 5 + Vector3.right * -2f + Vector3.forward * -5f;


        Tweener tTweener = this.transform.DOMove(Vector3.up * 5 + Vector3.right * -8f + Vector3.forward * -2f, 5, false);
        mpTweener_0 = tTweener.SetLoops(-1, LoopType.Yoyo);
        mpTweener_0.SetEase(Ease.Linear);

    }

    public void CameraTitleMoveStop()
    {
        mpTweener_0.Pause<Tweener>();
    }

    public void CameraTitleMovePlay()
    {
        mpTweener_0.Restart();
    }

}
