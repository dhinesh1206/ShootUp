using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreation : MonoBehaviour
{

    public static LevelCreation instance;

    //public List<GameObject> levelPrefabs, createdLevel, challengeLevels;
    public float intervalBetweenLevels;
   // public int totalLevel;
  //  public string deadLevel;
   // public Text LevelCountText;
  //  int CreatedLevel;
  //  public string gameMode;


    //public List<GameObject> easyLevels, mediumLevels, HardLevels, veryHardLevels, easyLevelCreated, mediumLevelCreated, HardLevelCreated, veryHardlevelcreated;


    public List<DifficultyLevel> levels;
    //GameObject levelCreated,Reload;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       // totalLevel = levelPrefabs.Count;
    }

    private void OnEnable()
    {
       // EventManager.instance.On_LevelCreate += On_LevelCreate;
       // EventManager.instance.On_LevelReload += On_LevelReload;
       // EventManager.instance.On_ChallengeCreate += On_ChallengeCreate;
       // EventManager.instance.On_MainGameOver += On_GameOver;
    }

    private void OnDisable()
    {
      //  EventManager.instance.On_LevelReload -= On_LevelReload;
       // EventManager.instance.On_LevelCreate -= On_LevelCreate;
       // EventManager.instance.On_ChallengeCreate -= On_ChallengeCreate;
       // EventManager.instance.On_MainGameOver -= On_GameOver;
    }

    //private void On_LevelCreate()
    //{
    //    gameMode = "Normal";
    //    //if (levelPrefabs.Count > 0)
    //    //{
    //    //    Index = Random.Range(0, levelPrefabs.Count);
    //    //    GameObject levelCreated = Instantiate(levelPrefabs[Index], transform, false);
    //    //    levelCreated.transform.SetParent(null);
    //    //    createdLevel.Add(levelPrefabs[Index]);
    //    //    levelPrefabs.Remove(levelPrefabs[Index]);
    //    //}
    //    //else
    //    //{
    //    //    foreach (GameObject level in createdLevel)
    //    //    {
    //    //        levelPrefabs.Add(level);
    //    //    }
    //    //    createdLevel.Clear();
    //    //    On_LevelCreate();
    //    //}

    //    if (CreatedLevel == 0)
    //    {
    //        CreateLevel("Easy");
    //    }
    //    else
    //    {
    //        int scorelastDigit = int.Parse((CreatedLevel.ToString()[CreatedLevel.ToString().Length-1]).ToString());
    //        print(scorelastDigit);
    //        if(scorelastDigit < 5) 
    //        {
    //            CreateLevel("Easy");    
    //        } else if(scorelastDigit <9 && scorelastDigit >= 5) 
    //        {
    //            CreateLevel("Medium");  
    //        } else {
    //            CreateLevel("Hard");
    //        }
    //    }

    //    if(CreatedLevel <= totalLevel)
    //        LevelCountText.text = CreatedLevel.ToString() + " / " + totalLevel.ToString();
    //}



    //private void On_ChallengeCreate(int levelNumber)
    //{
    //    gameMode = "Challenge";
    //    string levelName = "Level" + levelNumber.ToString();
    //    GameObject currentlevel = Instantiate(challengeLevels[levelNumber], transform, false);
    //    currentlevel.transform.SetParent(null);
    //}

    //public void On_GameOver()
    //{
    //    CancelInvoke("CreateNextLevel");
    //}


    //private void On_LevelReload()
    //{
    //    LevelReload();
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Level_End" && !GameManager.instance.gameOver)
        {
            if(!GameManager.instance.challengeModeActive)
                Invoke("CreateNextLevel", intervalBetweenLevels);
        }
    }

    void CreateNextLevel()
    {
        // EventManager.instance.OnLevelCreate();
        GameManager.instance.CreateLevels();
    }
    //public void LevelReload()
    //{

    //    for (int i = 0; i < levels.Count; i++)
    //    {
    //        foreach (var item in levels[i].createdList)
    //        {
    //            if(item.name == deadLevel)
    //            {
    //                GameObject levelCreated = Instantiate(item, transform, false);
    //                levelCreated.transform.SetParent(null);
    //            }
    //        }
    //    }
    //}

    //public void DeathLevel(string level)
    //{
    //    deadLevel = level;
    //}

    //void CreateLevel(string difficultyLevel)
    //{
        
    //    foreach (var levelList in levels)
    //    {
    //        if(levelList.difficultyLevel == difficultyLevel)
    //        {
    //            if (levelList.levelList.Count > 0)
    //            {

    //                List<int> values = new List<int>();

    //                for (int i = 0; i < levelList.levelList.Count/5; i++)
    //                {
    //                    Random.seed = System.DateTime.Now.Millisecond;
    //                    values.Add(Random.Range(0, levelList.levelList.Count));
    //                    print(values[i]);
    //                }
    //                int indexValue = values[Random.Range(0, values.Count)];

    //                GameObject levelCreated = Instantiate(levelList.levelList[indexValue], transform, false);
    //                CreatedLevel += 1;
    //                levelCreated.transform.SetParent(null);
    //                levelList.createdList.Add(levelList.levelList[indexValue]);
    //                levelList.levelList.Remove(levelList.levelList[indexValue]);
    //            } else 
    //            {
    //                foreach (GameObject createdlvls in levelList.createdList)
    //                {
    //                    levelList.levelList.Add(createdlvls);
    //                }
    //                levelList.createdList.Clear();
    //                CreateLevel(difficultyLevel);
    //            }


    //        }
    //    }
    //}

}

