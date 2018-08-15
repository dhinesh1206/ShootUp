using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObjects : MonoBehaviour {

    public string levelname;
    bool ReadyToDestroy;

    private void Start()
    {
        if (transform.parent)
        {

            string[] name = GetComponentInParent<LevelColor>().gameObject.name.Split('(');

            levelname = name[0];
        } else {
            string[] name = gameObject.name.Split('(');
            levelname = name[0];
        }
    }

    private void OnEnable()
    {
        EventManager.instance.On_ClearObjects += Instance_On_ClearObjects;   
    }

    private void OnDisable()
    {
        EventManager.instance.On_ClearObjects -= Instance_On_ClearObjects;   
    }

    void Instance_On_ClearObjects()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Level_End")
        {
            ReadyToDestroy = true;
        } else if(collision.transform.tag == "Object_end" && ReadyToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
