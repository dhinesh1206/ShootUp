using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speedMultiplier,levelSpeedMultiplier;
    public GameObject[] players;
    GameObject currentPlayer;

    private void OnEnable()
    {
        EventManager.instance.On_LevelCreate += On_LevelCreate;
        EventManager.instance.On_GameOver += On_GameOver;
        EventManager.instance.On_LevelReload += On_LevelReload;
    }

    private void OnDisable()
    {
        EventManager.instance.On_LevelCreate -= On_LevelCreate;
        EventManager.instance.On_GameOver -= On_GameOver;
        EventManager.instance.On_LevelReload -= On_LevelReload;
    }

    private void On_LevelCreate()
    {
        levelSpeedMultiplier = 1;

    }

    public void GameStart()
    {
       // AudioListener.pause = true;
        currentPlayer = players[Random.Range(0, players.Length)];
        currentPlayer.SetActive(true);
        GetComponent<Camera>().enabled = true;
    }

    private void On_LevelReload()
    {
        levelSpeedMultiplier = 1;
        currentPlayer.transform.localPosition = new Vector3(0,0,10);
        currentPlayer.SetActive(true);
    }

    private void On_GameOver()
    {
        levelSpeedMultiplier = 0;
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }
    }

    void Update () 
    {
        transform.Translate(Vector2.up * Time.deltaTime * speedMultiplier* levelSpeedMultiplier);
	}
}
