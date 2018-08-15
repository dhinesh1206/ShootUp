using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Facebook;
using GameAnalyticsSDK;

public class ScoreManagement : MonoBehaviour {

    public static ScoreManagement instance;

    public float scoreMultiplier, score,highScore,CoinsCollectedSoFar,CoinsCollected,defaultRestartCost,retryCost;
    public Text scoreText,gameOverScoreText,gameoverHighScoreText,retrycostText;
    bool highscoreReached;
    public GameObject notEnoughGold;
    int levelCrossed = 1;
    public bool gameOver;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore");
        retryCost = defaultRestartCost * UpgradeManager.instance.damageUpgradeLevel * UpgradeManager.instance.intervalUpgradeLevel;
    }

    private void Awake()
    {
        instance = this;
       // FB.Init();

    }
    private void OnEnable()
    {
       // EventManager.instance.On_ScoreAdd += On_ScoreAdd;
       // EventManager.instance.On_MainGameOver += On_GameOver;
       // EventManager.instance.On_LevelCreate += On_LevelCreate;
       // EventManager.instance.On_LevelReload += On_LevelReload;
    }

    private void OnDisable()
    {
       // EventManager.instance.On_ScoreAdd -= On_ScoreAdd;
       // EventManager.instance.On_MainGameOver -= On_GameOver;
       // EventManager.instance.On_LevelCreate -= On_LevelCreate;
      //  EventManager.instance.On_LevelReload -= On_LevelReload;
    }

    private void On_ScoreAdd()
    {
        score += scoreMultiplier * UpgradeManager.instance.hitCount;
        ScoreAdd();
        TotalCoins();
    }

    void On_LevelReload()
    {
       
        scoreText.text = score.ToString();
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
                scoreText.text = (score / 1000).ToString("f1") + "k";
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
            }
            highScore = score;
        }
    }

    private void On_LevelCreate()
    {
        levelCrossed += 1;
    }

    private void On_GameOver()
    {
        gameOver = true;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", Mathf.RoundToInt(score));
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
            gameOverScoreText.text = (score / 1000).ToString("f1") + "k";
        }

        if (highScore.ToString().Length < 4)
        {
            gameoverHighScoreText.text = highScore.ToString();
        }
        else
        {
            gameoverHighScoreText.text = (highScore / 1000).ToString("f1") + "k";
        }
        EventManager.instance.OnClearObjects();

       // gameOverTotalCoinText.text = CoinsCollectedSoFar.ToString();
        retryCost = (defaultRestartCost * UpgradeManager.instance.damageUpgradeLevel * UpgradeManager.instance.intervalUpgradeLevel)+ (levelCrossed*50);
        PlayerPrefs.SetFloat("HighScore", highScore);


        if(retryCost.ToString().Length<4){
            retrycostText.text = "$" + Mathf.Round(retryCost).ToString();
        } else {
            retrycostText.text = "$" + Mathf.Round(retryCost / 1000).ToString()+"K";
        }

    }

    public  void CoinAnimation()
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
       // CoinsCollected = score;
       // gameOverCoinText.text = CoinsCollected.ToString();
        TotalCoins();
    }

    void TotalCoins()
    {
        CoinsCollectedSoFar = PlayerPrefs.GetFloat("Total_Coins", 0);
        CoinsCollectedSoFar += scoreMultiplier * UpgradeManager.instance.hitCount;
     //   gameOverTotalCoinText.text = CoinsCollectedSoFar.ToString();
        PlayerPrefs.SetFloat("Total_Coins", CoinsCollectedSoFar);
    }

    IEnumerator TotalCoinAnimation(int coins)
    {
        for (int i = 1; i < coins; i++)
        {
            yield return new WaitForSeconds(0f);
            CoinsCollectedSoFar +=1;
         //   gameOverTotalCoinText.text = CoinsCollectedSoFar.ToString();
            //gameOverTotalCoinText.transform.DOScale(1.2f, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
            //{
               
            //    gameOverTotalCoinText.transform.DOScale(1f, 0.01f).SetEase(Ease.Linear);
            //});
        }
        PlayerPrefs.SetFloat("Total_Coins", CoinsCollectedSoFar);
    }


    public void RetryLevel()
    {
        CoinsCollectedSoFar = PlayerPrefs.GetFloat("Total_Coins", 0);
        print(retryCost < CoinsCollectedSoFar);
        if(retryCost < CoinsCollectedSoFar) 
        {
            CoinsCollected -= retryCost;
            PlayerPrefs.SetFloat("Total_Coins", CoinsCollectedSoFar);
            EventManager.instance.OnLevelReload();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
            gameOver = false;
        } else 
        {
            notEnoughGold.SetActive(true);
        }
    }
}
