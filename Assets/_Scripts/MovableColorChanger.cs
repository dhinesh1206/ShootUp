using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableColorChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().color = GameManager.instance.movableDefaultColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
