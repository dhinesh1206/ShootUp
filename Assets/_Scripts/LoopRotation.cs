using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoopRotation : MonoBehaviour {

    public Vector3 startAngle, endAngle;
	public float time,timeToActivate,timeInterval;
    public Ease easeType;

    public RotateMode rotateMode;

	void Start () {
		StartCoroutine (StartRotation(timeToActivate));
	}

	IEnumerator StartRotation(float  time)
    {
		yield return new WaitForSeconds (time);
		int difference = Mathf.RoundToInt(360 - transform.eulerAngles.z);

		if (Mathf.Abs( startAngle.z) ==difference)
        {
            Rotate(endAngle);
        }
        else
        {
            Rotate(startAngle);
        }
    }

    void Rotate(Vector3 angle)
    {
		transform.DOLocalRotate(angle, time, rotateMode).SetEase(easeType).OnComplete(() =>
          {
				StartCoroutine(StartRotation(timeInterval));
          });
    }
}
