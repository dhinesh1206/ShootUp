using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameAnalyticsSDK;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region variables

    [Header("Testing Toggles")]
    public bool challengeModeActive;
    public bool ActivateAds;

    [Space]
    [Header("Managers Transform")]
    public Transform levelCreator;
    public Transform levelDestroyer;

    [Space]
    [Header("UIPart")]
    public GameObject mainScreen;
    public GameObject ingameScreen;
    public GameObject gameOverScreen;
    public GameObject challengeScreen;
    public GameObject challengeIngameScreen;
    public GameObject challengeCompletedScreen;
    public GameObject challengeGameOverScreen;
    public GameObject playButton;
    public GameObject muteOnButton;
    public GameObject muteOffButton;
    public Text levelCountText;
    public Text scoreText;
    public Text gameOverScoreText;
    public Text gameoverHighScoreText;
    public bool clearCache;
    public GameObject[] LockedImages;


    [Space]
    [Header("Camera & Player")]
    public Camera mainCamera;
    public float speedMultiplier;
    public float levelSpeedMultiplier;
    public GameObject[] players;
    private GameObject currentPlayer;


    [Space]
    [Header("Level Creation")]
    private int totalLevel, createdLevel;
    private string leveltoReloadName, deathLevel;
    public List<DifficultyLevel> normalLevels;
    public List<ChallengeLevel> challengeLevels;
    private int challengeLvelcreated;
    [HideInInspector]


    [Space]
    [Header("Score Manager")]
    public float scoreMultiplier;
    [HideInInspector]
    public float score;
    public float highScore;
    public float CoinsCollectedSoFar;
    public float CoinsCollected;
    [HideInInspector]
    public bool highscoreReached, gameOver;


    [Space]
    [Header("Color Manager")]
    [Header("Shrinkable Colors")]
    public Color shrinkableDefaultColor;
    public Color shrinkableChangingColor;
    [Header("Movable Colors")]
    public Color movableDefaultColor;
    public Color movableChangingColor;
    [Header("Destroyable Color")]
    public Color DestroyableDefaultColor;
    public Color DestroyableChangingColor;

    #endregion /variables


    void Start()
    {
        if(ActivateAds)
        {
            
        }

        if (clearCache)
        {
            PlayerPrefs.DeleteAll();
        }
        highScore = PlayerPrefs.GetFloat("HighScore");
        foreach (var level in normalLevels)
        {
            totalLevel += level.levelList.Count;
        }
    }

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

        #region SDK Initialization
        GameAnalytics.Initialize();
        Facebook.Unity.FB.Init();
        #endregion

        //Check MuteOption PlayerPrefs
        CheckMuteFromPlayerPrefs();

        //Enable ChallengesIcon which are already Completed/Unlocked
        CheckCompletedChallengesIconEnable();

    }

    // Update is called once per frame
    void Update()
    {
        //Camera Up Movement
        mainCamera.gameObject.transform.Translate(Vector2.up * Time.deltaTime * speedMultiplier * levelSpeedMultiplier);
    }


    #region UIPart

    public void CheckMuteFromPlayerPrefs()
    {
        int status = PlayerPrefs.GetInt("Mute", 1);

        if (status == 0)
        {
            muteOffButton.SetActive(true);
            MuteAudio(true);
        }
        else
        {
            muteOnButton.SetActive(true);
            MuteAudio(false);
        }
    }

    private void MuteAudio(bool status)
    {
        AudioListener.pause = status;
        if (status)
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
    }

    private void CheckCompletedChallengesIconEnable()
    {
        string checkingStartingChallengeLevel = PlayerPrefs.GetString("Level0", "Locked");
        if (checkingStartingChallengeLevel == "Locked")
        {
            PlayerPrefs.SetString("Level0", "Unlocked");
        }


        for (int i = 0; i < LockedImages.Length; i++)
        {
            if ((PlayerPrefs.GetString("Level" + i.ToString(), "NotComplete")) == "Completed")
            {
                LockedImages[i].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                LockedImages[i].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                LockedImages[i].transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                LockedImages[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else if ((PlayerPrefs.GetString("Level" + i.ToString(), "NotComplete")) == "Unlocked")
            {
                LockedImages[i].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                LockedImages[i].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                LockedImages[i].transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                LockedImages[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                LockedImages[i].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                LockedImages[i].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                LockedImages[i].transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                LockedImages[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    public void StartNormalGame()
    {
        EnablePlayerAndCamera();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
        playButton.transform.DOScale(15, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            IngameMenu();
            CreateLevels();
        });

    }

    public void ActivateChallengeScreen()
    {
        challengeScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    public void ChallengeGameStart()
    {
        challengeScreen.SetActive(false);
    }

    public void ChallengeLevelGameOverScreensActivate()
    {
        DisablePlayeronHit();
        challengeIngameScreen.SetActive(false);
        challengeGameOverScreen.SetActive(true);
    }

    public void ChallengeLevelCompletedScreenActivate()
    {
        DisablePlayeronHit();
        challengeIngameScreen.SetActive(false);
        challengeCompletedScreen.SetActive(true);
    }

    public void DeactivateChallengeStatusScreen()
    {
        challengeCompletedScreen.SetActive(false);
        challengeGameOverScreen.SetActive(false);
        challengeScreen.SetActive(true);
    }

    public void RetryChallenge()
    {
        EventManager.instance.OnClearObjects();
        challengeCompletedScreen.SetActive(false);
        challengeGameOverScreen.SetActive(false);
        StartChallengeGame(challengeLvelcreated);
    }

    public void StartChallengeGame(int index)
    {
        challengeModeActive = true;
        ChallengeGameStart();
        EnablePlayerAndCamera();
        challengeLvelcreated = index;
        foreach(var challengeLvl in challengeLevels)
        {
            if(challengeLvl.levelNumber == index)
            {
                GameObject createdchlnglvl = Instantiate(challengeLvl.levelPrefab, levelCreator.transform, false);
                createdchlnglvl.transform.SetParent(null);
            }
        }
        ParachuteAnimation.instance.LevelReload();
    }



    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void MainMenu()
    {
        mainScreen.SetActive(true);
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(0);
    }

    private void IngameMenu()
    {
        mainScreen.SetActive(false);
        ingameScreen.SetActive(true);
    }

    private void ReloadIngameMenu()
    {
        scoreText.text = score.ToString();
        ingameScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    private void GameOverMenu()
    {
        ingameScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    private void GameOverScoreDisplay()
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
        PlayerPrefs.SetFloat("HighScore", highScore);

    }


    #endregion /UIPart

    #region Camera & Player
    public void EnablePlayerAndCamera()
    {
        levelSpeedMultiplier = 1;
        currentPlayer = players[Random.Range(0, players.Length)];
        RestePlayerPosition();
        mainCamera.enabled = true;
    }

    public void RestePlayerPosition()
    {
        currentPlayer.transform.localPosition = new Vector3(0, 0, 10);
        currentPlayer.SetActive(true);
    }

    public void DisablePlayeronHit()
    {
        levelSpeedMultiplier = 0;
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }
    }
    #endregion /Camera & Player


    #region LevelManagement

    public void CreateLevels()
    {
        CreateNormalLevel();
        challengeModeActive = false;
    }

    public void CreateNormalLevel()
    {
        
        if (createdLevel == 0)
        {
            CreateLevel("Easy");
        }
        else
        {
            int scorelastDigit = int.Parse((createdLevel.ToString()[createdLevel.ToString().Length - 1]).ToString());
            print(scorelastDigit);
            if (scorelastDigit < 5)
            {
                CreateLevel("Easy");
            }
            else if (scorelastDigit < 9 && scorelastDigit >= 5)
            {
                CreateLevel("Medium");
            }
            else
            {
                CreateLevel("Hard");
            }
        }

        if (createdLevel <= totalLevel)
            levelCountText.text = createdLevel.ToString() + " / " + totalLevel.ToString();

    }

    public void NormalLevelReload()
    {
        RestePlayerPosition();
        ReloadIngameMenu();
        ParachuteAnimation.instance.LevelReload();
        for (int i = 0; i < normalLevels.Count; i++)
        {
            foreach (var item in normalLevels[i].createdList)
            {
                if (item.name == deathLevel)
                {
                    GameObject levelCreated = Instantiate(item, levelCreator, false);
                    levelCreated.transform.SetParent(null);
                }
            }
        }
    }

    void CreateLevel(string difficultyLevelString)
    {
        
        foreach (var levelList in normalLevels)
        {
            if (levelList.difficultyLevel == difficultyLevelString)
            {
                if (levelList.levelList.Count > 0)
                {

                    List<int> values = new List<int>();

                    for (int i = 0; i < levelList.levelList.Count / 5; i++)
                    {
                        Random.seed = System.DateTime.Now.Millisecond;
                        values.Add(Random.Range(0, levelList.levelList.Count));
                        print(values[i]);
                    }
                    int indexValue = values[Random.Range(0, values.Count)];

                    GameObject levelCreated = Instantiate(levelList.levelList[indexValue], levelCreator, false);
                    createdLevel += 1;
                    levelCreated.transform.SetParent(null);
                    levelList.createdList.Add(levelList.levelList[indexValue]);
                    levelList.levelList.Remove(levelList.levelList[indexValue]);
                }
                else
                {
                    foreach (GameObject createdlvls in levelList.createdList)
                    {
                        levelList.levelList.Add(createdlvls);
                    }
                    levelList.createdList.Clear();
                    CreateLevel(difficultyLevelString);
                }


            }
        }
    }

    public void DeathLevel(string level)
    {
        deathLevel = level;
    }
    #endregion /LevelManagement


    #region ScoreManagement
    public void ScoreAdd()
    {
        if (!challengeModeActive)
        {
            score += scoreMultiplier * UpgradeManager.instance.hitCount;
            ScoreValueAdd();
            TotalCoins();
        }
    }

    void ScoreValueAdd()
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
        if (score > highScore)
        {
            if (!highscoreReached)
            {
                highscoreReached = true;
            }
            highScore = score;
        }
    }
   
    void TotalCoins()
    {
        CoinsCollectedSoFar = PlayerPrefs.GetFloat("Total_Coins", 0);
        CoinsCollectedSoFar += scoreMultiplier * UpgradeManager.instance.hitCount;
        PlayerPrefs.SetFloat("Total_Coins", CoinsCollectedSoFar);
    }

    #endregion /ScoreManagement

    #region HelperFunctions
    public void GameOver()
    {
        gameOver = true;
        ParachuteAnimation.instance.GameOver();
        DisablePlayeronHit();
        CheckGameOver();

    }

    public void ChallengeGameOver()
    {
        gameOver = true;
        ParachuteAnimation.instance.GameOver();
        DisablePlayeronHit();
        Invoke("ChallengeLevelGameOverScreensActivate", 2f);
    }


    public void CheckGameOver()
    {
            Invoke("GameOverMenu", 2f);
            Invoke("GameOverScoreDisplay", 2f);
    }

    public void ChallengeCompleted()
    {
        gameOver = true;
        UnlockNextNevel();
        Invoke("ChallengeLevelCompletedScreenActivate", 2f);
    }

    public void UnlockNextNevel()
    {
        PlayerPrefs.SetString("Level" + (challengeLvelcreated-1).ToString(), "Completed");
        PlayerPrefs.SetString("Level" + (challengeLvelcreated).ToString(), "Unlocked");
        CheckCompletedChallengesIconEnable();
    }


    #endregion /HelperRegion
}


[System.Serializable]
public class DifficultyLevel
{
    public string difficultyLevel;
    public List<GameObject> levelList;
    [HideInInspector]
    public List<GameObject> createdList;
}

[System.Serializable]
public class ChallengeLevel
{
    public int levelNumber;
    public GameObject levelPrefab;
}
