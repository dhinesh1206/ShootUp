using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParachuteAnimation : MonoBehaviour {

    public static ParachuteAnimation instance;
    public GameObject danceParentTransform;
    public GameObject[] danceAnimations,loonParts;
    Color defaultcolor;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
	{
        defaultcolor = loonParts[0].GetComponent<SpriteRenderer>().color;
	}

	private void OnEnable()
	{
       // EventManager.instance.On_MainGameOver+= GameOver;
       // EventManager.instance.On_LevelReload += LevelReload;
	}

	private void OnDisable()
	{
       // EventManager.instance.On_MainGameOver -= GameOver;
       // EventManager.instance.On_LevelReload -= LevelReload;
	}



    public void LevelReload()
    {
        foreach (GameObject item in loonParts)
        {
            item.SetActive(true);
        }
        gameObject.GetComponent<ParachuteCollision>().hit = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.transform.localPosition = new Vector3(0, -8f,10);
    }


    public void GameOver()
    {
        foreach (GameObject item in loonParts)
        {
            item.SetActive(false);
        }
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

}
