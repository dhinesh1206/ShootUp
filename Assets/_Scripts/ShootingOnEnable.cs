using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootingOnEnable : MonoBehaviour {

    private void OnEnable()
    {
        ShootStart.instance.ResumeShooting();
    }

    private void Start()
    {
        Movement();
    }

    void Movement()
    {
        float value = Random.Range(0.2f, -0.2f);
        transform.DOLocalMoveX(value, 0.025f, false).SetEase(Ease.Linear).OnComplete(() => {
            Movement();
        });
    }
}
