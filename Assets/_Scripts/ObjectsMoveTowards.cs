using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMoveTowards : MonoBehaviour {

    public float timeToActivate,timetoDeActivate, speed;
    public Vector3 endPosition;
    public bool idle = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(Activate());
        if(timetoDeActivate != 0)
        {
            StartCoroutine(DeActivate());
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(idle == false)
        {
            float steps = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, steps);
        }
	}

    IEnumerator DeActivate()
    {
        yield return new WaitForSeconds(timetoDeActivate);
        idle = true;
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(timeToActivate);
        idle = false;
    }
}
