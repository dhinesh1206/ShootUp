using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatinginvoke : MonoBehaviour {

	public float speed,time;
	bool activate;

	// Use this for initialization
	void Start () {
		Invoke ("Activate", time);
	}
	
	// Update is called once per frame
	void Update () {
		if (activate == true) {
			transform.Rotate (Vector3.forward * Time.deltaTime * speed);
		}
	}

	void Activate()
	{
		activate = true;
	}

}
