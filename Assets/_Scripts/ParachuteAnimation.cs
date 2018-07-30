using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParachuteAnimation : MonoBehaviour {

    public GameObject danceParentTransform;
    public GameObject[] danceAnimations,loonParts;
    Color defaultcolor;

	private void Start()
	{
        defaultcolor = loonParts[0].GetComponent<SpriteRenderer>().color;
	}

	private void OnEnable()
	{
        EventManager.instance.On_GameOver+= Instance_On_GameOver;
        EventManager.instance.On_LevelReload += Instance_On_LevelReload;
	}

	private void OnDisable()
	{
        EventManager.instance.On_GameOver -= Instance_On_GameOver;
        EventManager.instance.On_LevelReload -= Instance_On_LevelReload;
	}



    void Instance_On_LevelReload()
    {
        foreach (GameObject item in loonParts)
        {
            item.SetActive(true);
        }
        GetComponent<ParachuteCollision>().hit = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.transform.localPosition = new Vector3(0, -8f,10);
    }


    void Instance_On_GameOver()
    {
        foreach (GameObject item in loonParts)
        {
            item.SetActive(false);
        }
       
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

}
