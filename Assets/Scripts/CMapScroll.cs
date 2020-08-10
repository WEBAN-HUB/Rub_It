using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapScroll : MonoBehaviour
{
    float mScrollSpeed = 30;
    GameObject makeMap = null;

    // Start is called before the first frame update
    void Start()
    {
        makeMap = GameObject.FindGameObjectWithTag("GameController");

        this.transform.parent = makeMap.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (SgtGameData.GetInstance().GetIsPlaying())
        {
            this.gameObject.transform.Translate(Vector3.left * Time.fixedDeltaTime * mScrollSpeed);
        }
    }

}
