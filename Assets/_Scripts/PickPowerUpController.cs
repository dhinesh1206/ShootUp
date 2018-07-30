using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPowerUpController : MonoBehaviour {

    public int powerUpIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (powerUpIndex)
            {
                case 1:
                    ShootStart.instance.LargeBulletsShooting();
                    break;
                case 2:
                    ShootStart.instance.TripleBulletShooting();
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
