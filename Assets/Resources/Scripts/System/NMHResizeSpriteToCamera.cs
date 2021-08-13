using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHResizeSpriteToCamera : MonoBehaviour
{
	void Awake ()
    {
        Screen.SetResolution(720, 1280, true);

        ResizeSprite();
    }

    void ResizeSprite()
    {
        Screen.SetResolution(720, 1280, true);

        Camera.main.orthographicSize = 1280 / (2 * 100f);
    }
}
