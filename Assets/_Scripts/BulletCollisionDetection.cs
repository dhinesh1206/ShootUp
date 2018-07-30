using UnityEngine;

public class BulletCollisionDetection : MonoBehaviour {

    public int count;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<TextMesh>().text = count.ToString();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            Destroy(gameObject);
        }
    }
}
