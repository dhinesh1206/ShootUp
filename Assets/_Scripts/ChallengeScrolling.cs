using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ChallengeScrolling : MonoBehaviour {

    bool touched;
    Vector2 startPosition, endPosition;

    public float limit;
    public GameObject[] scrollindicator;
    public Color activeIndicator, deactiveIndicator;

    public GameObject scrollImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            touched = true;
            startPosition = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            touched = false;
            endPosition = Input.mousePosition;
            CheckDistance();
        }
	}


    void UpdateScollIndicator()
    {
        int positionx = int.Parse((scrollImage.transform.localPosition.x).ToString());
        print(positionx == 0);
        if(positionx == 0)
        {
            scrollindicator[0].GetComponent<Image>().color = activeIndicator;
            scrollindicator[1].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[2].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[3].GetComponent<Image>().color = deactiveIndicator;
        } else if(positionx == -1100)
        {
            scrollindicator[0].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[1].GetComponent<Image>().color = activeIndicator;
            scrollindicator[2].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[3].GetComponent<Image>().color = deactiveIndicator;
        } else if(positionx == -2200)
        {
            scrollindicator[0].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[1].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[2].GetComponent<Image>().color = activeIndicator;
            scrollindicator[3].GetComponent<Image>().color = deactiveIndicator;
        } else {
            scrollindicator[0].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[1].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[2].GetComponent<Image>().color = deactiveIndicator;
            scrollindicator[3].GetComponent<Image>().color = activeIndicator;
        }
    }

    void CheckDistance()
    {
        float distance = startPosition.x - endPosition.x;
        print(distance);
        if(distance > limit)
        {
            if (scrollImage.transform.localPosition.x <= 0 && scrollImage.transform.localPosition.x >= -2200)
            {
                scrollImage.transform.DOLocalMoveX(scrollImage.transform.localPosition.x- 1100, 0.2f, false).SetEase(Ease.Linear).OnComplete(() => {
                    UpdateScollIndicator(); 
                });
            }
        } 
        else if(distance < -limit)
        {
            if (scrollImage.transform.localPosition.x <= -1100 && scrollImage.transform.localPosition.x >= -3300)
            {
                scrollImage.transform.DOLocalMoveX(scrollImage.transform.localPosition.x + 1100, 0.2f, false).SetEase(Ease.Linear).OnComplete(() => {
                    UpdateScollIndicator();
                });
            }
        }
    }


}
