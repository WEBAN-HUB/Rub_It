using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class CTitleMgr : MonoBehaviour
{
    CTitleUI titleUI = null;


    void Start()
    {
        SceneManager.LoadScene("ScenePlay", LoadSceneMode.Additive);

        titleUI = FindObjectOfType<CTitleUI>();

    }

    
    void Update()
    {
        
    }

    public void HideUI()
    {
        titleUI.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        titleUI.gameObject.SetActive(true);
    }
   

}
