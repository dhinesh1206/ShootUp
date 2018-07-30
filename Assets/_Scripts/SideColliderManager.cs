using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideColliderManager : MonoBehaviour {

    public Image imageCanves;
    public GameObject plus1Prefab;

    Vector3 entryPoint;


	private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && Mathf.Abs(collision.gameObject.transform.position.x) > 7f) ;
        {
            if (entryPoint != Vector3.zero)
            {
                EventManager.instance.OnScoreAdd();
               // Vector3 screenPoint = Camera.main.WorldToScreenPoint(entryPoint);
               // GameObject createdScoreimage = Instantiate(plus1Prefab, imageCanves.transform, false);
               // createdScoreimage.transform.position = screenPoint;
                //Destroy(collision.gameObject);
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            entryPoint = collision.gameObject.transform.position;
        }
    }
}
