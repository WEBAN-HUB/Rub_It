using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLight : MonoBehaviour
{
    public Light mLight = null;
    public GameObject TitleObject = null;
    public CActor mPlayer = null;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeColor()
    {
        switch(SgtGameData.GetInstance().CharIndex)
        {
            case 0:
                mLight.color = new Color(0.72f, 0.5f, 0.45f, 0.4f);
                break;
            case 1:
                mLight.color = new Color(0.18f, 0.54f, 0.93f, 0.39f);
                break;
            case 2:
                mLight.color = new Color(0, 1, 0, 0.39f);
                break;
            case 3:
                mLight.color = new Color(1, 0, 0, 0.39f);
                break;
            case 4:
                mLight.color = new Color(1, 1, 1, 0.39f);
                break;
            case 5:
                mLight.color = new Color(1, 0.5f, 0, 0.39f);
                break;
            case 6:

                break;
        }
    }
    public void DoAlive()
    {
        mPlayer = FindObjectOfType<CActor>();

        this.transform.SetParent(mPlayer.transform);
        

    }

    public void DoDead()
    {
      

        this.transform.SetParent(this.TitleObject.transform);
        this.transform.position = Vector3.zero;
        this.mLight.gameObject.SetActive(false);

    }
}
