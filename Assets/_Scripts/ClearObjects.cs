using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObjects : MonoBehaviour {

    public string levelname;

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
       // print(levelname);
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

}
