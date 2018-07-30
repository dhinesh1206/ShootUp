using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManagement : MonoBehaviour {

    public float scoreMultiplier, score,highScore,CoinsCollectedSoFar,CoinsCollected;
    public Text scoreText,gameOverScoreText,gameoverHighScoreText,gameOverCoinText,gameOverTotalCoinText;
    bool highscoreReached;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore");
    }

    private void OnEnable()
    {
        EventManager.instance.On_ScoreAdd += On_ScoreAdd;
        EventManager.instance.On_GameOver += On_GameOver;
        EventManager.instance.On_LevelCreate += On_LevelCreate;
    }

    private void OnDisable()
    {
        EventManager.instance.On_ScoreAdd -= On_ScoreAdd;
        EventManager.instance.On_GameOver -= On_GameOver;
        EventManager.instance.On_LevelCreate -= On_LevelCreate;
    }

    private void On_ScoreAdd()
    {
        score += scoreMultiplier * UpgradeManager.instance.hitCount;
        ScoreAdd();
    }


    void ScoreAdd()
    {
        scoreText.transform.DOScale(1.2f, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (score.ToString().Length < 4)
            {
                scoreText.text = score.ToString();
            }
            else
            {
                scoreText.text = (score / 1000).ToString() + "k";
            }
            HighScoremanager();
            scoreText.transform.DOScale(1f, 0.1f).SetEase(Ease.Linear);
        });
    }


    private void HighScoremanager()
    {
        if(score>highScore)
        {
            if(!highscoreReached)
            {
                highscoreReached = true;
               // EventManager.instance.OnHighScore();
            }
            highScore = score;
        }
    }

    private void On_LevelCreate()
    {
        
    }

    private void On_GameOver()
    {
        Invoke("GameOverScoreDisplay", 2f);
    }

    void GameOverScoreDisplay()
    {
        if (score.ToString().Length < 4)
        {
            gameOverScoreText.text = score.ToString();
        }
        else
        {
            gameOverScoreText.text = (score / 1000).ToString() + "k";
        }

        if (highScore.ToString().Length < 4)
        {
            gameoverHighScoreText.text = highScore.ToString();
        }
        else
        {
            gameoverHighScoreText.text = (highScore / 1000).ToString() + "k";
        }
        CoinsCollectedSoFar = PlayerPrefs.GetFloat("Total_Coins", 2000);
        gameOverTotalCoinText.text = CoinsCollectedSoFar.ToString();
        PlayerPrefs.SetFloat("HighScore", highScore);
        CoinAnimation();
    }

    void CoinAnimation()
    {
        //for (int i = 1; i < coins; i++)
        //{
        //    yield return new WaitForSeconds(0f);
        //    CoinsCollected += 1;
        //    gameOverCoinText.text = CoinsCollected.ToString();
        //    //gameOverCoinText.transform.DOScale(1.2f, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
        //    //{

        //    //    gameOverCoinText.transform.DOScale(1f, 0.01f).SetEase(Ease.Linear);
        //    //});
        //}
        //StartCoroutine(TotalCoinAnimation(coins));
        CoinsCollected = score;
        gameOverCoinText.text = CoinsCollected.ToString();
        TotalCoins();
    }

    void TotalCoins()
    {
        CoinsCollectedSoFar += score;
        gameOverTotalCoinText.text = CoinsCollectedSoFar.ToString();
        PlayerPrefs.SetFloat("Total_Coins", CoinsCollectedSoFar);
    }

    IEnumerator TotalCoinAnimation(int coins)
    {
        for (int i = 1; i < coins; i++)
        {
            yield return new WaitForSeconds(0f);
            CoinsCollectedSoFar +=1;
            gameOverTotalCoinText.text = CoinsCollectedSoFar.ToString();
            //gameOverTotalCoinText.transform.DOScale(1.2f, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
            //{
               
            //    gameOverTotalCoinText.transform.DOScale(1f, 0.01f).SetEase(Ease.Linear);
            //});
        }
        PlayerPrefs.SetFloat("Total_Coins", CoinsCollectedSoFar);
    }

}
