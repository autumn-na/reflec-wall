using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NMHGameSceneUICtrl : MonoBehaviour
{
    public GameObject TItleObj;
    public GameObject ScoreObj;
    public GameObject GameOverObj;

    public Text CurScoreText;
    public Text BestScoreText;
    public Text GameOverScoreText;

	void Start ()
    {
        BestScoreText.text = NMHGameMng.Instance.nBestScore.ToString();

    }
	
	void Update ()
    {
        CheckStartGame();
        CheckGameOver();
        CheckCurScore();
    }

    void CheckStartGame()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TItleObj.SetActive(false);
            ScoreObj.SetActive(true);

            GameObject Player = GameObject.Find("NMHPlayer");
        }
    }

    void CheckCurScore()
    {
        if (NMHGameMng.Instance.nGameMode == (int)NMHGameMng.GameMode.PLAYING || NMHGameMng.Instance.nGameMode == (int)NMHGameMng.GameMode.TITLE)
        {
            CurScoreText.text = NMHGameMng.Instance.nCurScore.ToString();

            if (NMHGameMng.Instance.nCurScore >= NMHGameMng.Instance.nBestScore)
            {
                BestScoreText.text = NMHGameMng.Instance.nCurScore.ToString();
            }
        }
    }

    void CheckGameOver()
    {
        if (NMHGameMng.Instance.nGameMode == (int)NMHGameMng.GameMode.GAMEOVER)
        {
            GameOverObj.SetActive(true);

            GameOverScoreText.text = CurScoreText.text;
        }
    }

    public void BackToMain()
    {
        NMHGameMng.Instance.initializeGame();

        GameOverObj.SetActive(false);
    }
}
