using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectblink : MonoBehaviour {
	public float blinkinterval, interval;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();	
		StartCoroutine(Blink());
	}

	IEnumerator Blink()
	{
		yield return new WaitForSeconds (interval);
		StartCoroutine (Repeat ());
	}

	IEnumerator Repeat()
	{
		sr.enabled = true;
		yield return new WaitForSeconds (blinkinterval);
		sr.enabled = false;
		StartCoroutine (Blink ());
	}
}
