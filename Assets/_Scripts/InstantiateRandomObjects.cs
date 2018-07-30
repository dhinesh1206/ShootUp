using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateRandomObjects : MonoBehaviour {

	public GameObject[] objectToCreate;
	public Transform parentToCreate;
	public float creationCount, creationInterval, creationStartTime;

	// Use this for initialization
	void Start () {
		StartCoroutine(Activate());	
	}

	// Update is called once per frame
	void Update () {

	}

	public IEnumerator Activate()
	{
		yield return new WaitForSeconds(creationStartTime);
		StartCoroutine(CreateObjects());
	}

	public IEnumerator CreateObjects()
	{
		for (int i=0; i<creationCount; i ++)
		{
			GameObject createdObject =  Instantiate(objectToCreate[Random.Range(0,objectToCreate.Length)], parentToCreate, false);
			createdObject.transform.SetParent (null);
			Destroy (createdObject, 10f);
			yield return new WaitForSeconds(creationInterval);
		}
	}
}
