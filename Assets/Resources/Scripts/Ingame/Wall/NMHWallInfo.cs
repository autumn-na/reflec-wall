using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NMHWallInfo
{
    public GameObject[] WallPrefab;

    public GameObject LeftWallParent;
    public GameObject RightWallParent;

    public int nNextWallNum = 0;

    public float fLeftWallX = -3.35f;
    public float fRightWallX = 3.35f;
    public float fNextWallY = -5.4f;

    void Start()
    {
        InitializeObj();
    }

    void InitializeObj()
    {
       
    }

    public void CreateWall()
    {
        int nLeftWallType = Random.Range(0, 4);
        int nRightWallType = Random.Range(0, 4);

        //Create LEFT Wall
        GameObject CloneLeftWallObj = GameObject.Instantiate(WallPrefab[nLeftWallType], new Vector3(fLeftWallX, fNextWallY, 0), Quaternion.identity, LeftWallParent.transform);

        //Create Right Wall
        GameObject CloneRightWallObj = GameObject.Instantiate(WallPrefab[nRightWallType], new Vector3(fRightWallX, fNextWallY, 0), Quaternion.identity, RightWallParent.transform);

        fNextWallY += 2f;
    }

    public void CreateWallAtFirst()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateWall();
        }
    }

    public void DestroyAllWall()
    {
        foreach(Transform child in LeftWallParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in RightWallParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        fNextWallY = -5.4f;
    }
}
