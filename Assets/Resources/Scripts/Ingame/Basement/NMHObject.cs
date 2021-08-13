using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHObject : MonoBehaviour
{
    public Vector2 CurPosVec2;

	void Start ()
    {

    }
	
	void Update ()
    {

    }

    void InitializeObj()
    {

    }

    public void CheckCurPosVec2()
    {
        CurPosVec2 = (Vector2)transform.position;
    }
}
