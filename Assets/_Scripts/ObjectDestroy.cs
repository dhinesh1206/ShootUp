using UnityEngine;
using System.Collections;

public class ObjectDestroy : MonoBehaviour {

	public float timeToActivate;

	// Use this for initialization
	void Start () {
		StartCoroutine (Activate());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Activate()
	{
		yield return new WaitForSeconds(timeToActivate);
		Destroy (transform.gameObject);
	}

}

