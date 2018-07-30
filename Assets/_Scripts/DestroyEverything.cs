using UnityEngine;

public class DestroyEverything : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(collision.gameObject);
    }
}
