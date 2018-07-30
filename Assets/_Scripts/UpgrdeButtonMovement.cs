using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpgrdeButtonMovement : MonoBehaviour {

    public float duration;
    public Ease easetype;

    public void Move(GameObject target){
        if(target.transform.localPosition.y == -170)
        {
            target.transform.DOLocalMoveY(-400, duration, false).SetEase(easetype);

        } else if(target.transform.localPosition.y == -400)
        {
            target.transform.DOLocalMoveY(-170, duration, false).SetEase(easetype);
        }
    }
}
