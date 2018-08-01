using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreation : MonoBehaviour
{

    public static LevelCreation instance;

    public List<GameObject> levelPrefabs, createdLevel, challengeLevels, challengeLevelCreated;
    public float intervalBetweenLevels;
    public int Index, totalLevel;
    public string deadLevel;
    public Text LevelCountText;
    int CreatedLevel;

    //public List<GameObject> easyLevels, mediumLevels, HardLevels, veryHardLevels, easyLevelCreated, mediumLevelCreated, HardLevelCreated, veryHardlevelcreated;


    public List<DifficultyLevel> levels;
    //GameObject levelCreated,Reload;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        totalLevel = levelPrefabs.Count;
    }

    private void OnEnable()
    {
        EventManager.instance.On_LevelCreate += On_LevelCreate;
        EventManager.instance.On_LevelReload += On_LevelReload;
        EventManager.instance.On_ChallengeCreate += On_ChallengeCreate;
        EventManager.instance.On_GameOver += On_GameOver;
    }

    private void OnDisable()
    {
        EventManager.instance.On_LevelReload -= On_LevelReload;
        EventManager.instance.On_LevelCreate -= On_LevelCreate;
        EventManager.instance.On_ChallengeCreate -= On_ChallengeCreate;
        EventManager.instance.On_GameOver -= On_GameOver;
    }

    private void On_LevelCreate()
    {
        //if (levelPrefabs.Count > 0)
        //{
        //    Index = Random.Range(0, levelPrefabs.Count);
        //    GameObject levelCreated = Instantiate(levelPrefabs[Index], transform, false);
        //    levelCreated.transform.SetParent(null);
        //    createdLevel.Add(levelPrefabs[Index]);
        //    levelPrefabs.Remove(levelPrefabs[Index]);
        //}
        //else
        //{
        //    foreach (GameObject level in createdLevel)
        //    {
        //        levelPrefabs.Add(level);
        //    }
        //    createdLevel.Clear();
        //    On_LevelCreate();
        //}

        if (CreatedLevel == 0)
        {
            CreateLevel("Easy");
        }
        else
        {
            int scorelastDigit = int.Parse((CreatedLevel.ToString()[CreatedLevel.ToString().Length-1]).ToString());
            print(scorelastDigit);
            if(scorelastDigit < 5) 
            {
                CreateLevel("Easy");    
            } else if(scorelastDigit <9 && scorelastDigit >= 5) 
            {
                CreateLevel("Medium");  
            } else {
                CreateLevel("Hard");
            }
        }

        if(CreatedLevel <= totalLevel)
            LevelCountText.text = CreatedLevel.ToString() + " / " + totalLevel.ToString();
    }



    private void On_ChallengeCreate()
    {
        if (challengeLevels.Count > 0)
        {
            int Index = Random.Range(0, challengeLevels.Count);
            GameObject createdChallengeLevel = Instantiate(challengeLevels[Index], transform, false);
            createdChallengeLevel.transform.SetParent(null);
            challengeLevelCreated.Add(createdChallengeLevel);
            challengeLevels.Remove(challengeLevels[Index]);
        }
        else
        {
            foreach (GameObject level in challengeLevelCreated)
            {
                challengeLevels.Add(level);
            }
            challengeLevelCreated.Clear();
            On_ChallengeCreate();
        }
    }

    void On_GameOver()
    {
        CancelInvoke("CreateNextLevel");
    }


    private void On_LevelReload()
    {
        LevelReload();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Level_End" && !ScoreManagement.instance.gameOver)
        {
            Invoke("CreateNextLevel", intervalBetweenLevels);
        }
    }

    void CreateNextLevel()
    {
        EventManager.instance.OnLevelCreate();
    }

    public void LevelReload()
    {

        for (int i = 0; i < levels.Count; i++)
        {
            foreach (var item in levels[i].createdList)
            {
                if(item.name == deadLevel)
                {
                    GameObject levelCreated = Instantiate(item, transform, false);
                    levelCreated.transform.SetParent(null);
                }
            }
        }
    }

    public void DeathLevel(string level)
    {
        deadLevel = level;
    }

    void CreateLevel(string difficultyLevel)
    {
        foreach (var levelList in levels)
        {
            if(levelList.difficultyLevel == difficultyLevel)
            {
                if (levelList.levelList.Count > 0)
                {
                    int indexValue = Random.Range(0, levelList.levelList.Count);
                 //   int playerPrefsCount = PlayerPrefs.GetInt(difficultyLevel + levelList.levelList[indexValue].name,0);
                 //   while (playerPrefsCount != 0)
                //    {
                 //       indexValue = Random.Range(0, levelList.levelList.Count);
                 //       playerPrefsCount = PlayerPrefs.GetInt(difficultyLevel + levelList.levelList[indexValue].name, 0);
                 //   }

                    GameObject levelCreated = Instantiate(levelList.levelList[indexValue], transform, false);
                    CreatedLevel += 1;
                    levelCreated.transform.SetParent(null);
                    levelList.createdList.Add(levelList.levelList[indexValue]);
                    levelList.levelList.Remove(levelList.levelList[indexValue]);
                 //   for (int i = 0; i < levelList.levelList.Count;i++) {
                //        int indexofthelevel = PlayerPrefs.GetInt(difficultyLevel + levelList.levelList[i].name, 0);
                //        if (indexofthelevel > 0)
                //        {
                   //         PlayerPrefs.SetInt(difficultyLevel + levelList.levelList[i].name, indexofthelevel - 1);
                //        }
                 //                  
                //    }
               //     PlayerPrefs.SetInt(difficultyLevel + levelList.levelList[indexValue].name, 1);
                } else 
                {
                    foreach (GameObject createdlvls in levelList.createdList)
                    {
                        levelList.levelList.Add(createdlvls);
                    }
                    levelList.createdList.Clear();
                    CreateLevel(difficultyLevel);
                }
            }
        }
    }

}

[System.Serializable]
public class DifficultyLevel
{
    public string difficultyLevel;
    public List<GameObject> levelList;
    public List<GameObject> createdList;
}