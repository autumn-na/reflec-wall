using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHWall :NMHObject
{
    public GameObject PlayerObj;

    public int nWallType;

    public enum WallType
    {
        GREEN,
        PURPLE,
        BLUE,
        RED
    }

    private void Start()
    {
        PlayerObj = GameObject.Find("NMHPlayer");
    }

    private void LateUpdate()
    {
        CheckCurPosVec2();

    }

    private void Update()
    {
        if (Mathf.Abs(PlayerObj.GetComponent<NMHPlayer>().CurPosVec2.y - CurPosVec2.y) >= 30)
        {
            Destroy(this.gameObject);
        }
    }
}
