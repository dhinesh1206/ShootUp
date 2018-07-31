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
        print(levelBackgroundColor == Color.white);
        if (levelBackgroundColor == Color.white)
        {
            ParticalManager.instance.currentColor = levelBackgroundColor;
            Camera.main.DOColor(levelBackgroundColor, 1.5f);
        } else 
        {
            int index = Random.Range(0, 2);
            print(index);
            if(index == 0) 
            {
                ParticalManager.instance.currentColor = levelBackgroundColor;
                Camera.main.DOColor(levelBackgroundColor, 1.5f);
            } else {
                ParticalManager.instance.currentColor = Color.white;
                Camera.main.DOColor(Color.white, 1.5f);
            }
        }
    }
}
