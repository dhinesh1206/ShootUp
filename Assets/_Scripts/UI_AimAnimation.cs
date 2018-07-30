using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_AimAnimation : MonoBehaviour {

    public float maxScaleValue, minScaleValue,scaleTiming,rotationTiming,waitTime;

	// Use this for initialization
	void Start () {
        Animate();
	}
	
    void Animate()
    {
        int Index = Random.Range(0, 3);
        if (Index == 0)
            Scale();
        else if (Index == 1)
            Rotate();
        else if (Index == 2)
            ScaleAndRotate();
    }

    void Scale()
    {
        float newscalueValue = Random.Range(minScaleValue, maxScaleValue);
        transform.DOScale(newscalueValue, scaleTiming).SetEase(Ease.Linear).OnComplete(() =>
        {
            CancelInvoke("Animate");
            Invoke("Animate", waitTime);
        });
    }

    void Rotate()
    {
        float newAngle = Random.Range(0, 360);
        transform.DORotate(new Vector3(0, 0, newAngle), rotationTiming, RotateMode.Fast).OnComplete(() =>
        {
            CancelInvoke("Animate");
            Invoke("Animate", waitTime);
        });
    }

    void ScaleAndRotate()
    {
        Scale();
        Rotate();
    }

}
