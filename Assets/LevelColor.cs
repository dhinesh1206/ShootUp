using UnityEngine;
using DG.Tweening;

public class LevelColor : MonoBehaviour {

    public Color levelBackgroundColor;


    private void Start()
    {
        Invoke("ChangeColor", 0.5f);
    }

    void ChangeColor()
    {
        ParticalManager.instance.currentColor = levelBackgroundColor;
        Camera.main.DOColor(levelBackgroundColor, 1.5f);
    }
}
