using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager instance;
  //  public delegate void Level_Delegate();
  //  public event Level_Delegate On_LevelCreate,On_LevelReload,On_MainGameOver,On_ChallengeGameOver,On_ScoreAdd,On_HighScore,On_ClearObjects;


    public delegate void Level_Delegate();
    public event Level_Delegate On_ClearObjects;


    private void Awake()
    {
        instance = this;
    }

    public void OnLevelCreate()
    {
        //if(On_LevelCreate != null)
        //{
        //    On_LevelCreate();
        //}
    }

    public void OnLevelReload()
    {
        //if(On_LevelReload!= null)
        //{
        //    On_LevelReload();
        //}
    }

    public void OnGameOver ()
    {
        //if (LevelCreation.instance.gameMode != "Challenge")
        //{
        //    if (On_MainGameOver != null)
        //    {
        //        On_MainGameOver();
        //    }
        //} else {
        //    if (On_ChallengeGameOver != null)
        //    {
        //        On_ChallengeGameOver();
        //    }
        //}
    }

    public void OnScoreAdd()
    {
        //if(On_ScoreAdd!= null)
        //{
        //    On_ScoreAdd();
        //}
    }

    public void OnChallengeCreate(int number)
    {
    //    if(On_ChallengeCreate!= null)
    //    {
    //        On_ChallengeCreate(number);
    //    }
    }

    public void OnHighScore()
    {
        //if(On_HighScore!= null)
        //{
        //    On_HighScore();
        //}
    }

    public void OnClearObjects()
    {
        if(On_ClearObjects!= null)
        {
            On_ClearObjects();
          
        }
    }
}
