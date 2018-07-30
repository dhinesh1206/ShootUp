using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletAnimation : MonoBehaviour {


    public float duration;
    public Ease easeType;
	// Use this for initialization
	void Start () {
        StartAnimation();
	}
	
    void StartAnimation()
    {
        transform.DOScale(0.35f, duration).SetEase(easeType).OnComplete(() =>
        {
            transform.DOScale(0.3f, duration).SetEase(easeType).OnComplete(() =>
            {
                Invoke("StartAnimation", 0.5f);
            });
        });
    }
}
