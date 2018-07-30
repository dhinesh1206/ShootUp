using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticalManager : MonoBehaviour {

    public static ParticalManager instance;
    public GameObject plus1Partical, imageCanves;
    public Color currentColor;

    private void Awake()
    {
        instance = this;
    }

    public void CreatePartical( Vector2 position)
    {
       
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(position);
        GameObject particalCreated = Instantiate(plus1Partical, imageCanves.transform, false);
        if (currentColor == Color.black)
        {
            particalCreated.GetComponent<Text>().color = Color.white;
        }
        particalCreated.transform.position = screenPoint;
    }
}
