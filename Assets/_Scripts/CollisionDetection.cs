using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour {

    
    float massValue;
    Vector3 scaleValue;
    public float percentageOfSizeToReduce;
    public float hitCount,defaultHitCount;
    TextMesh tm;
    float reducingRate,timeToResize;
    GameObject partical,smallPartical;

    public enum ParticalSystemList
    {
        Circle,
        Box,
        Triangle
    }
    public Ease easetype;
    public ParticalSystemList particals;

    void Start () {
        scaleValue = transform.localScale;
      
        //  float hitcountMultipler = hitCount*UpgradeManager.instance.hitCount;
        float hitcountMultipler =Mathf.Round(hitCount * ((UpgradeManager.instance.hitCount / UpgradeManager.instance.interval) / (1/0.2f)));
        // hitCount = Mathf.RoundToInt(Random.Range(hitcountMultipler * 0.9f, hitcountMultipler * 1.1f));
        hitCount = hitcountMultipler;
        defaultHitCount = hitCount;

        timeToResize = UpgradeManager.instance.interval/2;
       // reducingRate = (100 -((100 - percentageOfSizeToReduce) / hitCount))/100;
      
        tm = GetComponentInChildren<TextMesh>();
        if(hitCount.ToString().Length == 2)
        {
            tm.gameObject.transform.localScale = new Vector3(tm.gameObject.transform.localScale.x *0.9f, tm.gameObject.transform.localScale.y *0.9f, tm.gameObject.transform.localScale.z *0.9f);
        } else if (hitCount.ToString().Length == 3)
        {
            tm.gameObject.transform.localScale = new Vector3(tm.gameObject.transform.localScale.x *0.75f, tm.gameObject.transform.localScale.y *0.75f,tm.gameObject.transform.localScale.z * 0.75f);
        } else if(hitCount.ToString().Length == 4)
        {
            tm.gameObject.transform.localScale = new Vector3(tm.gameObject.transform.localScale.x *0.65f,tm.gameObject.transform.localScale.y * 0.65f,tm.gameObject.transform.localScale.z *0.65f);
        }
        tm.text = hitCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
           
                hitCount -= collision.GetComponent<BulletMovement>().hitCount * 1.2f;
                tm.text = (Mathf.Round( hitCount)).ToString();
                
                if (hitCount < 1)
                {
                   // GameObject partical = Instantiate(particals, transform, false);
                   // partical.transform.SetParent(null);
                    CreateParticals();
                    EventManager.instance.OnScoreAdd();
                    Destroy(gameObject);
                }
                else
                {
                CreateParticalsSmall(collision.gameObject.transform.position);
              //  AudioClip audios = Resources.Load("Hit_Bullet", typeof(AudioClip)) as AudioClip;
                //
                //AudioSource audioSource = gameObject.AddComponent<AudioSource>();
               // audioSource.volume = 0.1f;
               // audioSource.clip = audios;
              //  audioSource.Play();
                    //float bulletsReducingRate = collision.GetComponent<BulletMovement>().sizeReducingRate;
                reducingRate = ((percentageOfSizeToReduce) / 100) + (((100-percentageOfSizeToReduce) / 100) * (hitCount / defaultHitCount));

                    float newScaleX = scaleValue.x * (reducingRate);
                    float newScaleY = scaleValue.y * (reducingRate);
                    transform.DOScale(new Vector3(newScaleX, newScaleY, scaleValue.z), timeToResize).SetEase(easetype).OnComplete(() =>
                     {
                        // scaleValue = new Vector3(newScaleX, newScaleY, scaleValue.z);
                     });

                    EventManager.instance.OnScoreAdd();

            }
            Destroy(collision.gameObject);
        }
    }

    void CreateParticals()
    {
        
        if(particals == ParticalSystemList.Box)
        {
            partical = Instantiate(Resources.Load("Box_Explosion", typeof(GameObject))) as GameObject;
        } else if(particals == ParticalSystemList.Triangle)
        {
            partical = Instantiate(Resources.Load("Triangle_Explosion", typeof(GameObject))) as GameObject;
        } else 
        {
            partical = Instantiate(Resources.Load("Circle_Explosion", typeof(GameObject))) as GameObject;    
        }

        partical.transform.position = transform.position;
        partical.transform.SetParent(null);
        partical.GetComponent<ParticleSystemRenderer>().material.color  = GetComponent<SpriteRenderer>().color;
        partical.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        partical.GetComponent<ParticleSystem>().Play();
    }


    void CreateParticalsSmall( Vector3 point)
    {

        if (particals == ParticalSystemList.Box)
        {
            smallPartical = Instantiate(Resources.Load("Collision_Box_particals", typeof(GameObject))) as GameObject;
        }
        else if (particals == ParticalSystemList.Triangle)
        {
            smallPartical = Instantiate(Resources.Load("Collision_Triangle_particals", typeof(GameObject))) as GameObject;
        }
        else
        {
            smallPartical = Instantiate(Resources.Load("Collision_Circle_particals", typeof(GameObject))) as GameObject;
        }
        smallPartical.transform.position = new Vector3(point.x, point.y + 0.075f, point.z + 5f);
        smallPartical.transform.SetParent(null);
        smallPartical.GetComponent<ParticleSystemRenderer>().material.color = GetComponent<SpriteRenderer>().color;
        smallPartical.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f)*(transform.localScale.y* 0.25f);
        smallPartical.GetComponent<ParticleSystem>().Play();




        //GameObject smallPartical = Instantiate(Resources.Load("Collision_particals",typeof(GameObject))) as GameObject;

        //smallPartical.transform.position = new Vector3(point.x,point.y+0.075f,point.z+5f);
        //smallPartical.transform.SetParent(gameObject.transform);
        //smallPartical.GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
        //smallPartical.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
       
        //smallPartical.GetComponent<ParticleSystem>().Play();
    }
}
