using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class CActor : MonoBehaviour
{

    float mSpeed = 0.15f;

 

    bool Isground = true;
    bool RayShielder = false;
    int GravityType = 1;
    int RScalar = 1;
    int LREdge = 0;
    float JumpScalar = 15f;

    int layermask = -1;
   
    Touch mTouch;
    Vector3 GCheckV = Vector3.zero;
    Vector3 JumpVector = Vector3.up;
    public Vector3 tGVec = Vector3.zero;
    Vector3 VectorforLerp = Vector3.zero;

    CCamera mCamera = null;
    public GameObject ChildLookpos = null;
    Rigidbody mRigidbody;

    public CDeadUI DeadUI = null;
    public GameObject mBody = null;
    public GameObject DeadParticle = null;
    public CPlayUI PlayUI = null;

    public GameObject GroundHit = null;
    public GameObject GroundHit_u = null;
    public GameObject JumpHit = null;
    public GameObject JumpHit_u = null;
   
    bool IsAlive = true;

    public List<CBody> BodyArray = new List<CBody>();

    
    void Start()
    {
        BodyArray.Clear();

        mCamera = FindObjectOfType<CCamera>();
        mRigidbody = this.GetComponent<Rigidbody>();
        //layermask = (-1) -1<< LayerMask.GetMask("Actor");
        //layermask = ~layermask;

        //Debug.Log("<color=red>" + LayerMask.GetMask("Actor") + "</color>");

        layermask = 1 << 9; // LayerMask.GetMask("Actor");
        layermask = ~layermask;

        CBody[] tArray = FindObjectsOfType<CBody>();

        SortedDictionary<int, CBody> tSortedArray = new SortedDictionary<int, CBody>();

        foreach(var body in tArray)
        {
            tSortedArray.Add(body.index, body);
        }

        foreach(var body in tSortedArray)
        {
            BodyArray.Add(body.Value);
        }

        for(int i = 0; i < BodyArray.Count; i++)
        {
            //Debug.Log(BodyArray[i].index);
            BodyArray[i].gameObject.SetActive(false);
        }

        BodyArray[SgtGameData.GetInstance().CharIndex].gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
      
     
        
        if(SgtGameData.GetInstance().GetIsPlaying() == true)
        {
            FireRaycast();
        }
     


        if (Input.GetKeyDown(KeyCode.P))
        {
            DoDead();
        }

        
        //if (mCamera.IsCameraMove == false && IsAlive == true && Isground == true) 
        //{
        //    switch(SgtGameData.GetInstance().CAMERAVIEW)
        //    {
        //        case CMVIEW.SIDE:


        //            //touch
        //            //if(Input.touchCount > 0)
        //            //{
        //            //    mTouch = Input.GetTouch(0);
        //            //    if( mTouch.phase == TouchPhase.Began)
        //            //    {
        //            //        DoJump();
        //            //    }
        //            //}

        //            // keyboard
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
                        
        //                DoJump();
        //            }
        //            break;

        //        case CMVIEW.TOP:


        //            //touch
        //            //if (Input.touchCount > 0)
        //            //{
        //            //    mTouch = Input.GetTouch(0);
        //            //    if (mTouch.phase == TouchPhase.Began)
        //            //    {
        //            //        DoJump();
        //            //    }
        //            //}


        //            // keyboard
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                DoMove();
        //            }
        //            break;
        //    }
        //}

        if(Input.GetKeyDown(KeyCode.LeftControl) && Isground == true)
        {
            CameraSwitch();
        }
    }


    public void CameraSwitch()
    {
        if (SgtGameData.GetInstance().CAMERAVIEW == CMVIEW.TOP)
        {
            SgtGameData.GetInstance().CAMERAVIEW = CMVIEW.SIDE;
            //Debug.Log("SIDE VIEW");
        }
        else if (SgtGameData.GetInstance().CAMERAVIEW == CMVIEW.SIDE)
        {
            SgtGameData.GetInstance().CAMERAVIEW = CMVIEW.TOP;
            //Debug.Log("TOP VIEW");
        }

        mCamera.CameraSwitch();

    }

    void FireRaycast()
    {
        GCheckV = this.transform.position;
        GCheckV.x = this.transform.position.x + 1.5f;

        if (Physics.Raycast(this.transform.position, Vector3.up, out RaycastHit tHit_g2, Mathf.Infinity, layermask))
        {

            //Debug.Log("ray2");
            //Debug.Log(tHit_g2.point);
            GroundHit_u.transform.position = tHit_g2.point;
        }


        if (Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit tHit_g1, Mathf.Infinity, layermask))
        {
            //Debug.Log("ray1");
            //Debug.Log(tHit_g1.point);
            GroundHit.transform.position = tHit_g1.point;
        }

        if (Physics.Raycast(GCheckV, Vector3.down, out RaycastHit tHit_j1, Mathf.Infinity, layermask))
        {
            JumpHit.transform.position = tHit_j1.point;
        }

        if (Physics.Raycast(GCheckV, Vector3.up, out RaycastHit tHit_j2, Mathf.Infinity, layermask))
        {
            JumpHit_u.transform.position = tHit_j2.point;
        }


    }
    public void DoJump()
    {
        //Debug.Log("INPUT SPACE1");

        if (mCamera.IsCameraMove == false && IsAlive == true && Isground == true)
        {
            mBody.transform.localScale = Vector3.one;

            //Debug.Log("JUMP");
            Isground = false;
            
            //Invoke("ReverseGravity", 0.15f);
            ReverseGravity();

            CSoundMgr.Getinstance().PlayBgm(1);

            //mRigidbody.AddForce(Vector3.up * JumpScalar,ForceMode.Impulse);

            //this.transform.DOMove(this.transform.position + (JumpVector), 0.05f, false).SetEase(Ease.Linear);

            if(tGVec.y > 0)
            {
                VectorforLerp = this.transform.position;
                VectorforLerp.y = JumpHit_u.transform.position.y;

                this.transform.DOMove(Vector3.Lerp(this.transform.position,VectorforLerp,0.7f), 0.05f, false).SetEase(Ease.Linear);
                //Debug.Log("CHECK : " + this.transform.position + "\n" + VectorforLerp + "\n" + JumpHit_u.transform.position);
                //Debug.Log("up" + Vector3.Lerp(this.transform.position, GroundHit_u.transform.position, 0.5f));
            }
            else
            {
                VectorforLerp = this.transform.position;
                VectorforLerp.y = JumpHit.transform.position.y;

           
                this.transform.DOMove(Vector3.Lerp(this.transform.position,VectorforLerp, 0.7f), 0.05f, false).SetEase(Ease.Linear);
                //Debug.Log("down" + Vector3.Lerp(this.transform.position, GroundHit.transform.position, 0.5f));
            }


            JumpVector = JumpVector * -1;

            if (mBody.transform.localScale == Vector3.one)
            {
                mBody.transform.DOScaleY(1.4f, 0.025f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
                mBody.transform.DOScaleZ(0.6f, 0.025f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
                mBody.transform.DOScaleX(0.6f, 0.025f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
            }

        }
    }

    
    public void ReverseGravity()
    {
       
        tGVec.y = Physics.gravity.y * -1f;

        Physics.gravity = tGVec;

        

    }

    public void DoMove()
    {
        if (mCamera.IsCameraMove == false && IsAlive == true && Isground == true)
        {
            Vector3 MoveVector = Vector3.zero;

            MoveVector.y = this.transform.position.y;

            //Debug.Log("INPUT SPACE2");
            CSoundMgr.Getinstance().PlayBgm(2);

            LREdge = LREdge + RScalar;
            if (LREdge >= 1)
            {
                RScalar = -1;
            }
            else if (LREdge <= -1)
            {
                RScalar = 1;
            }

            switch (LREdge)
            {

                case -1:
                    MoveVector = MoveVector + (Vector3.forward * 2);
                    this.transform.DOMove(MoveVector, mSpeed, false).SetEase(Ease.InOutQuart);

                    break;

                case 0:

                    this.transform.DOMove(MoveVector, mSpeed, false).SetEase(Ease.InOutQuart);


                    break;

                case 1:
                    MoveVector = MoveVector + (Vector3.forward * -2);
                    this.transform.DOMove(MoveVector, mSpeed, false).SetEase(Ease.InOutQuart);

                    break;
            }
            if (mBody.transform.localScale == Vector3.one)
            {
                mBody.transform.DOScaleZ(1.4f, 0.1f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
                mBody.transform.DOScaleY(0.6f, 0.1f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
                mBody.transform.DOScaleX(0.6f, 0.1f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
            }
            //Debug.Log("TOP VIEW POSITION: " + LREdge);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("tagGround") && RayShielder == false)
        {
            //Debug.Log("Ground");
            Isground = true;
            RayShielder = true;
        }

       

        if (collision.gameObject.CompareTag("tagDeadObject"))
        {

            DoDead();
        }

    }

    private void OnTriggerStay(Collider collision)
    {

        if (collision.gameObject.CompareTag("tagGround") && RayShielder == false)
        {
            //Debug.Log("Ground");
            Isground = true;
            RayShielder = true;
        }


    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("tagGround"))
        {
            //Debug.Log("noGround");
            Isground = false;
            RayShielder = false;
        }


    }


    public void DoDead()
    {
        if (IsAlive == true)
        {
            CLight tLight = FindObjectOfType<CLight>();
            tLight.DoDead();

            CSoundMgr.Getinstance().PlayBgm(3);
            CSoundMgr.Getinstance().StopBgm(0);
            //Debug.Log("으앙쥬금");
            IsAlive = false;
            
            //mBody.SetActive(false);

            DeadParticle.transform.position = this.transform.position;
            DeadParticle.SetActive(true);
            SgtGameData.GetInstance().CAMERAVIEW = CMVIEW.TITLE;
            mCamera.CameraSwitch();
            DeadUI.gameObject.SetActive(true);
         
            DeadUI.UpdateDeadUI();
        }
    }

    public void DoAlive()
    {
        RScalar = 1;
        LREdge = 0; 
        IsAlive = true;
        tGVec = SgtGameData.GetInstance().GetGVec();
        this.transform.position = Vector3.zero;
        //mBody.SetActive(true);
        mBody.SetActive(false);

        DeadParticle.SetActive(false);

        PlayUI.gameObject.SetActive(true);
        PlayUI.ResetScore();

        SgtGameData.GetInstance().CAMERAVIEW = CMVIEW.TOP;
        mCamera.CameraSwitch();
    }
}
