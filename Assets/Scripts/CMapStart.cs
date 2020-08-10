using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapStart : MonoBehaviour
{
    List<GameObject> mMapList = new List<GameObject>();

    Queue<GameObject> mMapSpawnedList = new Queue<GameObject>();

    GameObject mBackGround = null;

    Queue<GameObject> mBackGroundList = new Queue<GameObject>();

    GameObject mFirstMap = null;

    public bool Starting = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadMap();
    }

    public void StageSelect()
    {
        int M1 = mMapSpawnedList.Count;
        int M2 = mBackGroundList.Count;

        for (int i = 0; i < M1; i++)
        {
            //Debug.Log("mMapSpawnedList: " + mMapSpawnedList.Count);
            Destroy(mMapSpawnedList.Dequeue().gameObject);
        }
        for (int i = 0; i < M2; i++)
        {
            //Debug.Log("mBackGround List: " + mBackGroundList.Count);
            Destroy(mBackGroundList.Dequeue().gameObject);
        }


        if (mBackGround != null)
        {
            mBackGround = null;
        }
        if (mFirstMap != null)
        {
            mFirstMap = null;    
        }

        mMapList.Clear();
        
        //for(int i = 0; i < mMapList.Count; i++)
        //{
        //    mMapList.RemoveAt(0);
        //}

        //string tstringb = "Prefabs/BackGround/PFBackGround_";
        //tstringb = tstringb + SgtGameData.GetInstance().Stage;

        //mFirstMap = Resources.Load("Prefabs/PFMapStart") as GameObject;
        //mBackGround = Resources.Load(tstringb) as GameObject;

        //Object[] tPFMap = null;

        //string tstring = "Prefabs/MapST";
        //tstring = tstring + SgtGameData.GetInstance().Stage;

        //tPFMap = Resources.LoadAll(tstring);



        //foreach (var maps in tPFMap)
        //{
        //    GameObject tMap = maps as GameObject;
        //    mMapList.Add(tMap);
        //}


        //mMapSpawnedList.Enqueue(Instantiate<GameObject>(mFirstMap, Vector3.right * 25f, Quaternion.identity));
        //mBackGroundList.Enqueue(Instantiate<GameObject>(mBackGround, Vector3.right * 25f, Quaternion.identity));

        //for (int ti = 1; ti < 3; ti++)
        //{
        //    int r = Random.Range(0, mMapList.Count);
        //    float tX = 200 * ti + 25;
        //    mMapSpawnedList.Enqueue(Instantiate<GameObject>(mMapList[r], Vector3.right * tX, Quaternion.identity));
        //    mBackGroundList.Enqueue(Instantiate<GameObject>(mBackGround, Vector3.right * tX, Quaternion.identity));
        //}


    }
    public void LoadMap()
    {
        string tstringb = "Prefabs/BackGround/PFBackGround_";
        tstringb = tstringb + SgtGameData.GetInstance().Stage;

        mBackGround = Resources.Load(tstringb) as GameObject;
        mFirstMap = Resources.Load("Prefabs/PFMapStart_"+SgtGameData.GetInstance().Stage) as GameObject;

        Object[] tPFMap = null;
        string tstring = "Prefabs/MapST";
        tstring = tstring + SgtGameData.GetInstance().Stage;

        tPFMap = Resources.LoadAll(tstring);

        foreach (var maps in tPFMap)
        {
            GameObject tMap = maps as GameObject;
            mMapList.Add(tMap);
        }


        mMapSpawnedList.Enqueue(Instantiate<GameObject>(mFirstMap, Vector3.right * 25f, Quaternion.identity));
        mBackGroundList.Enqueue(Instantiate<GameObject>(mBackGround, Vector3.right * 25f, Quaternion.identity));

        for (int ti = 1; ti < 3; ti++)
        {
            int r = Random.Range(0, mMapList.Count);
            float tX = 200 * ti + 25;
            mMapSpawnedList.Enqueue(Instantiate<GameObject>(mMapList[r], Vector3.right * tX, Quaternion.identity));
            mBackGroundList.Enqueue(Instantiate<GameObject>(mBackGround, Vector3.right * tX, Quaternion.identity));
        }

    }

    
    void Update()
    {
        if (Starting == true)
        {
            StartCoroutine(UpdateMap());
            Starting = false;
        }
            
    }

    IEnumerator UpdateMap()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(6.7f);

            if (mMapSpawnedList.Count > 0)
            {
                Destroy(mMapSpawnedList.Dequeue().gameObject);
            }
            if (mBackGroundList.Count > 0)
            {
                Destroy(mBackGroundList.Dequeue().gameObject);
            }

            int r = Random.Range(0, mMapList.Count);

            //Debug.Log(mMapSpawnedList.Count);
            //Debug.Log(mBackGroundList.Count);
            mMapSpawnedList.Enqueue(Instantiate<GameObject>(mMapList[r], new Vector3(mMapSpawnedList.Peek().transform.position.x +400f,0f,0f), Quaternion.identity));
            mBackGroundList.Enqueue(Instantiate<GameObject>(mBackGround, new Vector3(mBackGroundList.Peek().transform.position.x + 400f, 0f, 0f), Quaternion.identity));

            //Debug.Log("<color='blue'>Spawned Block</color>");

        }
    }


}
