using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHGameMng : MonoBehaviour
{
    public static NMHGameMng Instance;

    public NMHWallInfo WallInfo;

    public GameObject PlayerObj;


    public Camera MainCam;

    public int nGameMode = 0;

    public int nBestScore = 0;
    public int nCurScore = 0;

    public bool bIsFirst = true;

    public enum GameMode
    {
        TITLE,
        PLAYING,
        GAMEOVER
    }

    void Awake()
    {
        if (Instance == null)
        {
            NMHGameMng.Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        initializeGame();
    }

    public void initializeGame()
    {
        GetData();

        MainCam.transform.position = new Vector3(0, 0, -10);
        nGameMode = (int)GameMode.TITLE;

        PlayerObj.SetActive(true);
        PlayerObj.transform.position = new Vector3(0, -5, 0);
        PlayerObj.transform.rotation = Quaternion.identity;
        PlayerObj.GetComponent<NMHPlayer>().bIsSelectingAngle = true;

        bIsFirst = true;

        nCurScore = -5;

        WallInfo.DestroyAllWall();

        WallInfo.CreateWallAtFirst();
    }
	
	void Update ()
    {
        CheckStartGame();
        CheckCreateWall();
    }

    void CheckCreateWall()
    {
        if (Mathf.Abs(PlayerObj.GetComponent<NMHPlayer>().CurPosVec2.y - WallInfo.fNextWallY) <=15f)
        {
            WallInfo.CreateWall();
        }
    }

    void CheckStartGame()
    {
        if(nGameMode == (int)GameMode.TITLE)
        {
            if(Input.GetMouseButtonDown(0))
            {
                nGameMode = (int)GameMode.PLAYING;
            }
        }
    }

    void GetData()
    {
        nBestScore = PlayerPrefs.GetInt("BestScore");
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("BestScore", nBestScore);
    }
}
