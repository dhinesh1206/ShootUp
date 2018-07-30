using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysicsAdding : MonoBehaviour {

    bool playerCollided;
    public float gravityScale,destroyTime = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&!playerCollided)
        {
            
            ParticalManager.instance.CreatePartical(collision.contacts[0].point);
            EventManager.instance.OnScoreAdd();
        }
        playerCollided = true;
        transform.SetParent(null);
        transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
       // Destroy(gameObject, destroyTime);
    }
}
