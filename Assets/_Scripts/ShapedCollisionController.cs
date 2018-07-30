using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapedCollisionController : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GameObject partical = Instantiate(Resources.Load("Collision_Explosion", typeof(GameObject))) as GameObject;
            partical.transform.position = new Vector3(collision.gameObject.transform.position.x,collision.gameObject.transform.position.y+0.1f,collision.gameObject.transform.position.z);
            partical.transform.SetParent(null);
           // partical.GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
            partical.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            partical.GetComponent<ParticleSystem>().Play();
           // AudioClip audio = Resources.Load("Stopped_Bullet", typeof(AudioClip)) as AudioClip;

           // AudioSource audioSource = gameObject.AddComponent<AudioSource>();
           // audioSource.volume = 0.01f;
           // audioSource.clip = audio;
          //  audioSource.Play();
            Destroy(collision.gameObject);
        }
    }
}
