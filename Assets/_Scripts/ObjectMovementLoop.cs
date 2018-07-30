using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMovementLoop : MonoBehaviour {

    public Vector3 startPosition, endPosition;
    public float frontMovementSpeed, backMovementSpeed,timeToActivate,intervalTime;
    public Ease easetype;

	// Use this for initialization
	void Start () {
        StartCoroutine(Activate());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Activate()
    {
        yield return new WaitForSeconds(timeToActivate);
        if (transform.position == startPosition)
        {
            StartCoroutine(AnimationStart(endPosition, backMovementSpeed));
        } else
        {
            StartCoroutine(AnimationStart(startPosition, frontMovementSpeed));
        }
    }

    public IEnumerator AnimationStart (Vector3 position, float time)
    {
        yield return new WaitForSeconds(intervalTime);
        transform.DOLocalMove(position,time,false).OnComplete(() =>
        {
            if (position == startPosition)
            {
                StartCoroutine(AnimationStart(endPosition, backMovementSpeed));
            }
            else
            {
                StartCoroutine(AnimationStart(startPosition, frontMovementSpeed));
            }
        });
    }
}
