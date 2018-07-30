using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextEffectFadeOut : MonoBehaviour {

    public float timeTravel;
    public Vector3 additionalEndValue;

	private void Start()
	{
        MoveToEndPoint();
        FadeOut();
	}


	void MoveToEndPoint()
    {
        Vector3 newEndvalue = new Vector3(transform.position.x,transform.position.y,transform.position.z) + additionalEndValue;
        transform.DOMove(newEndvalue, timeTravel, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    void FadeOut()
    {
        GetComponent<Text>().DOFade(0, timeTravel).SetEase(Ease.Linear);
    }

}
