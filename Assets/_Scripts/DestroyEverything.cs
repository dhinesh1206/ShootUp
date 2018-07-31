using UnityEngine;

public class DestroyEverything : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Loon")
        {
            Destroy(collision.gameObject);
        }
    }
}
