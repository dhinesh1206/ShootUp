using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speedMultiplier;
    public float hitCount;
    public Vector2 direction;
    public float destroyTime;

	void Start ()
    {
        hitCount = UpgradeManager.instance.hitCount;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
        //rb.AddForce(direction * speedMultiplier, ForceMode2D.Impulse);
    }
	
	void Update ()
    {
        rb.velocity = direction* speedMultiplier;
	}
}
