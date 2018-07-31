using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager instance;
    public delegate void Level_Delegate();
    public event Level_Delegate On_LevelCreate,On_LevelReload,On_GameOver,On_ScoreAdd,On_ChallengeCreate,On_HighScore,On_ClearObjects;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    public void OnLevelCreate()
    {
        if(On_LevelCreate != null)
        {
            On_LevelCreate();
        }
    }

    public void OnLevelReload()
    {
        if(On_LevelReload!= null)
        {
            On_LevelReload();
        }
    }

    public void OnGameOver ()
    {
        if(On_GameOver!= null)
        {
            On_GameOver();
        }
    }

    public void OnScoreAdd()
    {
        if(On_ScoreAdd!= null)
        {
            On_ScoreAdd();
        }
    }

    public void OnChallengeCreate()
    {
        if(On_ChallengeCreate!= null)
        {
            OnChallengeCreate();
        }
    }

    public void OnHighScore()
    {
        if(On_HighScore!= null)
        {
            On_HighScore();
        }
    }

    public void OnClearObjects()
    {
        if(On_ClearObjects!= null)
        {
            On_ClearObjects();
          
        }
    }
}
