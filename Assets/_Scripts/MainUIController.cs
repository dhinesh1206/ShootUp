using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GameAnalyticsSDK;

public class MainUIController : MonoBehaviour {

    public static MainUIController instance;

    public GameObject mainScreen, ingameScreen, gameOverScreen,playButton,muteOnButton,muteOffButton;
    public GameObject[] LockImaged;


    private void Awake()
    {
       // PlayerPrefs.DeleteAll();
        instance = this;
        CheckMute();
        GameAnalytics.Initialize();
        Facebook.Unity.FB.Init();
        CheckCompletedChallenges();
    }
    private void OnEnable()
    {
       // EventManager.instance.On_LevelCreate += On_LevelCreate;
       // EventManager.instance.On_LevelReload += On_LevelReload;
       // EventManager.instance.On_MainGameOver += On_GameOver;
       // EventManager.instance.On_ChallengeGameOver +=On_ChallengeGameOver;
        
    }

    private void OnDisable()
    {
       // EventManager.instance.On_LevelCreate -= On_LevelCreate;
       // EventManager.instance.On_LevelReload -= On_LevelReload;
       // EventManager.instance.On_MainGameOver -= On_GameOver;
       // EventManager.instance.On_ChallengeGameOver -= On_ChallengeGameOver;
    }

    private void On_LevelReload()
    {
        ingameScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    private void On_GameOver()
    {
        Invoke("GameOverMenu", 2f);
    }

    private void On_LevelCreate()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
        playButton.transform.DOScale(15, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            IngameMenu();
        });
    }

    public void MainMenu()
    {
        mainScreen.SetActive(true);
    }

    public void IngameMenu()
    {
        mainScreen.SetActive(false);
        ingameScreen.SetActive(true);
    }

    public void GameOverMenu()
    {
        
        ingameScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
       
        SceneManager.LoadScene(0);
    }

    public void MuteAudio(bool status)
    {
        AudioListener.pause = status;
        if(status)
        {
            PlayerPrefs.SetInt("Mute", 0);
        } else{
            PlayerPrefs.SetInt("Mute", 1); 
        }
    }

    public void CheckMute()
    {
        int status = PlayerPrefs.GetInt("Mute",1);
       
        if(status == 0)
        {
            muteOffButton.SetActive(true);
            MuteAudio(true);
        } else
        {
            muteOnButton.SetActive(true);    
            MuteAudio(false);
        }
    }

    public void ReloadGame()
    {
        ScoreManagement.instance.RetryLevel();
    }

    public void CheckCompletedChallenges()
    {
        for (int i = 0; i < LockImaged.Length;i++){
            if((PlayerPrefs.GetString("Level"+i.ToString(),"NotComplete")) == "Completed"){
                LockImaged[i].SetActive(false);
            }
        }
    }

    public void On_ChallengeGameOver()
    {
        
    }

}
