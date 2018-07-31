using System.Collections.Generic;
using UnityEngine;

public class LevelCreation : MonoBehaviour {

    public static LevelCreation instance;

    public List<GameObject> levelPrefabs,createdLevel,challengeLevels,challengeLevelCreated;
    public float intervalBetweenLevels;
    public int Index;
    public string deadLevel;
    //GameObject levelCreated,Reload;

    private void Awake()
    {
        instance = this;
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
        if (levelPrefabs.Count > 0)
        {
            Index = Random.Range(0, levelPrefabs.Count);
            GameObject levelCreated = Instantiate(levelPrefabs[Index], transform, false);
            levelCreated.transform.SetParent(null);
            createdLevel.Add(levelPrefabs[Index]);
            levelPrefabs.Remove(levelPrefabs[Index]);
        }
        else
        {
            foreach (GameObject level in createdLevel)
            {
                levelPrefabs.Add(level);
            }
            createdLevel.Clear();
            On_LevelCreate();
        }
    }



    private void On_ChallengeCreate()
    {
        if(challengeLevels.Count>0)
        {
            int Index = Random.Range(0, challengeLevels.Count);
            GameObject createdChallengeLevel = Instantiate(challengeLevels[Index], transform, false);
            createdChallengeLevel.transform.SetParent(null);
            challengeLevelCreated.Add(createdChallengeLevel);
            challengeLevels.Remove(challengeLevels[Index]);
        } else {
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
        if(collision.gameObject.tag == "Level_End"&& !ScoreManagement.instance.gameOver)
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
       
        foreach(GameObject levelcreatedbefore in createdLevel)
        {
            if (levelcreatedbefore.name == deadLevel)
            {
               
                GameObject levelCreated = Instantiate(levelcreatedbefore, transform, false);
                levelCreated.transform.SetParent(null);
            }
        }
    }

    public void DeathLevel(string level)
    {
        deadLevel = level;
    }
}
