using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour {



    float massValue;
    Vector3 scaleValue;
    public float percentageOfSizeToReduce;
    public float hitCount, defaultHitCount;
    TextMesh tm;
    float reducingRate, timeToResize;
    GameObject partical, smallPartical;
    Color changeColor;

    public enum ColorCode
    {
        Shrinkable,
        Destroyable
    }

    public ColorCode objectType;
    Color startingColor;


    public enum ParticalSystemList
    {
        Circle,
        Box,
        Triangle
    }
    public Ease easetype;
    public ParticalSystemList particals;

    void Start()
    {

        if (objectType == ColorCode.Shrinkable)
        {
            Changecolor(GameManager.instance.shrinkableDefaultColor);
            //gameObject.GetComponent<SpriteRenderer>().color = EventManager.instance.shrinkableDefaultColor;
            changeColor = GameManager.instance.shrinkableChangingColor;
        }
        else
        {
            Changecolor(GameManager.instance.DestroyableDefaultColor);
            //gameObject.GetComponent<SpriteRenderer>().color = EventManager.instance.DestroyableDefaultColor;
            changeColor = GameManager.instance.DestroyableChangingColor;
        }


        if(gameObject.GetComponent<SpriteRenderer>())
        {
            startingColor = gameObject.GetComponent<SpriteRenderer>().color;
        } else
        {
            startingColor = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        }

       
        print(startingColor);
        scaleValue = transform.localScale;
        float hitcountMultipler = Mathf.Round(hitCount * ((UpgradeManager.instance.hitCount / UpgradeManager.instance.interval) / (1 / 0.2f)));
        hitCount = hitcountMultipler;
        defaultHitCount = hitCount;

        timeToResize = UpgradeManager.instance.interval / 2;
        tm = GetComponentInChildren<TextMesh>();
        if (hitCount.ToString().Length == 2)
        {
            tm.gameObject.transform.localScale = new Vector3(tm.gameObject.transform.localScale.x * 0.9f, tm.gameObject.transform.localScale.y * 0.9f, tm.gameObject.transform.localScale.z * 0.9f);
        }
        else if (hitCount.ToString().Length == 3)
        {
            tm.gameObject.transform.localScale = new Vector3(tm.gameObject.transform.localScale.x * 0.75f, tm.gameObject.transform.localScale.y * 0.75f, tm.gameObject.transform.localScale.z * 0.75f);
        }
        else if (hitCount.ToString().Length == 4)
        {
            tm.gameObject.transform.localScale = new Vector3(tm.gameObject.transform.localScale.x * 0.65f, tm.gameObject.transform.localScale.y * 0.65f, tm.gameObject.transform.localScale.z * 0.65f);
        }
        tm.text = hitCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            hitCount -= collision.GetComponent<BulletMovement>().hitCount;
            tm.text = (Mathf.Round(hitCount)).ToString();

            if (hitCount <= 0)
            {
                CreateParticals();
                //EventManager.instance.OnScoreAdd();
                GameManager.instance.ScoreAdd();
                Destroy(gameObject);
            }
            else
            {
                CreateParticalsSmall(collision.gameObject.transform.position);
                reducingRate = ((percentageOfSizeToReduce) / 100) + (((100 - percentageOfSizeToReduce) / 100) * (hitCount / defaultHitCount));

                float tempDifference = 1 - (defaultHitCount - hitCount) / (defaultHitCount * 0.5f);

                print(tempDifference);
                if (tempDifference <= 0.9f)
                {
                    Changecolor(SpriteColor());
                }
                else if (tempDifference > 0.7f && tempDifference <= 0.8f)
                {
                    Changecolor(SpriteColor());
                }
                else if (tempDifference > 0.6f && tempDifference <= 0.7f)
                {
                    Changecolor(SpriteColor());
                }
                else if (tempDifference > 0.55f && tempDifference <= 0.6f)
                {
                    Changecolor(SpriteColor());
                }
                if (tempDifference >= 0.50f && tempDifference <= 0.55f)
                {
                    Changecolor(SpriteColor());
                }

                float newScaleX = scaleValue.x * (reducingRate);
                float newScaleY = scaleValue.y * (reducingRate);
                transform.DOScale(new Vector3(newScaleX, newScaleY, scaleValue.z), timeToResize).SetEase(easetype).OnComplete(() =>
                {
                    // scaleValue = new Vector3(newScaleX, newScaleY, scaleValue.z);
                });

                //GameManager.instance.ScoreAdd();
                GameManager.instance.ScoreAdd();

            }
            Destroy(collision.gameObject);
        }
    }

    void Changecolor(Color targetcolor)
    {
        if(gameObject.GetComponent<SpriteRenderer>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = targetcolor;
        } else
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = targetcolor;
        }
    }

    Color SpriteColor()
    {
        Color imagecolor = new Color();
        imagecolor.r = startingColor.r - ((startingColor.r - changeColor.r) * ((defaultHitCount - hitCount) / defaultHitCount) * 2);
        print(startingColor.r - changeColor.r);
        imagecolor.g = startingColor.g - ((startingColor.g - changeColor.g) * ((defaultHitCount - hitCount) / defaultHitCount) * 2);
        imagecolor.b = startingColor.b - ((startingColor.b - changeColor.b) * ((defaultHitCount - hitCount) / defaultHitCount) * 2);
        imagecolor.a = 1;
        print(imagecolor);
        return imagecolor;
    }

    void CreateParticals()
    {

        if (particals == ParticalSystemList.Box)
        {
            partical = Instantiate(Resources.Load("Box_Explosion", typeof(GameObject))) as GameObject;
        }
        else if (particals == ParticalSystemList.Triangle)
        {
            partical = Instantiate(Resources.Load("Triangle_Explosion", typeof(GameObject))) as GameObject;
        }
        else
        {
            partical = Instantiate(Resources.Load("Circle_Explosion", typeof(GameObject))) as GameObject;
        }

        partical.transform.position = transform.position;
        partical.transform.SetParent(null);
       // partical.GetComponent<ParticleSystemRenderer>().material.color = GetComponent<SpriteRenderer>().color;
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            partical.GetComponent<ParticleSystemRenderer>().material.color = gameObject.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            partical.GetComponent<ParticleSystemRenderer>().material.color = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        }
        partical.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        partical.GetComponent<ParticleSystem>().Play();
    }


    void CreateParticalsSmall(Vector3 point)
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
        if(gameObject.GetComponent<SpriteRenderer>())
        {
            smallPartical.GetComponent<ParticleSystemRenderer>().material.color = gameObject.GetComponent<SpriteRenderer>().color;
        } else
        {
            smallPartical.GetComponent<ParticleSystemRenderer>().material.color = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        }

        
        smallPartical.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f) * (transform.localScale.y * 0.25f);
        smallPartical.GetComponent<ParticleSystem>().Play();




        //GameObject smallPartical = Instantiate(Resources.Load("Collision_particals",typeof(GameObject))) as GameObject;

        //smallPartical.transform.position = new Vector3(point.x,point.y+0.075f,point.z+5f);
        //smallPartical.transform.SetParent(gameObject.transform);
        //smallPartical.GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
        //smallPartical.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        //smallPartical.GetComponent<ParticleSystem>().Play();
    }
}
