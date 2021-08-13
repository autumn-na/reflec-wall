using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHPlayer : NMHUnit
{
    public GameObject ArrowPrefab;

    public GameObject CamParent;
    public Camera MainCam;

    public GameObject InvisibleWall;

    public GameObject HItEffectPrefab;

    public Vector2 TargetDirVec2;

    public SpriteRenderer AirplainSprR;

    public Rigidbody2D PlayerRid2d;

    public float fPlayerMoveForce = 100f;
    public float fPlayerMoveSpeed = 10f;
    public float fAngle;

    public bool bIsSelectingAngle = true;

    Vector3 StartVec3;
    Vector3 EndVec3;



    void Start ()
    {
        InitializeObjs();
    }

    private void LateUpdate()
    {
        CheckCurPosVec2();
        MoveCamera();

        InvisibleWall.transform.rotation = Quaternion.identity;
        InvisibleWall.transform.localRotation = Quaternion.identity;
    }

    void Update ()
    {
        MovePlayer();
        CheckScore();
        CheckInvisibleWall();
        SelectAngle();
    }

    void InitializeObjs()
    {
        TargetDirVec2 = new Vector2(0, 0);

        PlayerRid2d = GetComponent<Rigidbody2D>();
    }

    void MoveCamera()
    {
        if(CurPosVec2.y > 0 && TargetDirVec2.y >= 0 && PlayerRid2d.velocity.y > 0)
        {
            MainCam.transform.position = new Vector3(0, CurPosVec2.y, MainCam.transform.position.z);
        }
    }

    void CheckInvisibleWall()
    {
        if (CurPosVec2.y > 0 && TargetDirVec2.y >= 0 && PlayerRid2d.velocity.y > 0)
        {
            InvisibleWall.transform.parent = transform;
            InvisibleWall.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            InvisibleWall.transform.parent = null;

            InvisibleWall.transform.rotation = Quaternion.identity;
            InvisibleWall.transform.localRotation = Quaternion.identity;
        }

        InvisibleWall.transform.rotation = Quaternion.identity;
        InvisibleWall.transform.localRotation = Quaternion.identity;
    }

    void CheckScore()
    {
        NMHGameMng.Instance.nCurScore = 5 + (int)CurPosVec2.y;

        if(NMHGameMng.Instance.nCurScore < 0)
        {
            NMHGameMng.Instance.nCurScore = 0;
        }
    }

    void MovePlayer()
    {
        if (NMHGameMng.Instance.nGameMode == (int)NMHGameMng.GameMode.PLAYING)
        {
            fAngle = (Mathf.Atan2(TargetDirVec2.y, TargetDirVec2.x) * Mathf.Rad2Deg) - 90;

            if (AirplainSprR != null)
            {
                AirplainSprR.transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);
            }
        }
    }

    public void SelectAngle()
    {
        if (bIsSelectingAngle && NMHGameMng.Instance.nGameMode == (int)NMHGameMng.GameMode.PLAYING)
        {
            if (Input.GetMouseButtonDown(0))
            {
                NMHGameMng.Instance.bIsFirst = false;

                StartVec3 = Input.mousePosition;

                PlayerRid2d.gravityScale = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndVec3 = Input.mousePosition;

                TargetDirVec2 = (Vector2)Vector3.Normalize(EndVec3 - StartVec3);

                bIsSelectingAngle = false;

                ForcePlayer();
            }
        }


        if (NMHGameMng.Instance.bIsFirst)
        {
            PlayerRid2d.gravityScale = 0f;

            Debug.Log("asdfasf");
        }
    }

    void ForcePlayer()
    {
        PlayerRid2d.AddForce(TargetDirVec2 * fPlayerMoveForce);

        PlayerRid2d.gravityScale = 0.1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            GetComponent<AudioSource>().Play();
            Reflect(collision);
        }
    }

    void GameOver()
    { 
        if(NMHGameMng.Instance.nCurScore >= NMHGameMng.Instance.nBestScore)
        {
            NMHGameMng.Instance.nBestScore = NMHGameMng.Instance.nCurScore;
        }

        NMHGameMng.Instance.nGameMode = (int)NMHGameMng.GameMode.GAMEOVER;

        NMHGameMng.Instance.SaveData();

        gameObject.SetActive(false);
    }

    void Reflect(Collision2D _Wall)
    {
        HItEffect();

        switch (_Wall.gameObject.GetComponent<NMHWall>().nWallType)
        {
            case (int)NMHWall.WallType.GREEN:
                GreenWall();
                break;
            case (int)NMHWall.WallType.PURPLE:
                PurpleWall();
                break;
            case (int)NMHWall.WallType.BLUE:
                BlueWall();
                break;
            case (int)NMHWall.WallType.RED:
                RedWall();
                break;
        }
    }

    void GreenWall()
    {
        TargetDirVec2.x *= -1;

        ForcePlayer();
    }

    void PurpleWall()
    {
        TargetDirVec2.x *= -1;
        TargetDirVec2.y = 0;

        ForcePlayer();
    }

    void BlueWall()
    {
        TargetDirVec2.x = 0;
        TargetDirVec2.y = 0;

        bIsSelectingAngle = true;

        PlayerRid2d.gravityScale = 0;
        PlayerRid2d.velocity = new Vector2(0, 0);
    }

    void RedWall()
    {
        GameOver();
    }

    void HItEffect()
    {
        Instantiate(HItEffectPrefab, transform.localPosition, Quaternion.identity);
    }
}
