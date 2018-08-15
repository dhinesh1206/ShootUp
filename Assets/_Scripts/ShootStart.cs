using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShootStart : MonoBehaviour {

    public static ShootStart instance;

    public float interval;
    public GameObject smallBullets,LargeBullets,tripleBullet,sizeUpBullet,fastBullets;
    //public PhysicsObjectController physicsObject;
    public GameObject[] parent;
    public List<BulletSounds> bulletSounds;
    GameObject bullet;
    public AudioSource audioSource;
    string bulletname;

    private void Awake()
    {
        instance = this;
        GetComponent<Camera>().enabled = false;

        float aspect = (float)Screen.height / (float)Screen.width;

       // print(aspect);
        if (aspect > 1.8f)
            GetComponent<Camera>().orthographicSize = 11.85f;
        else if(aspect < 1.5f)
            GetComponent<Camera>().orthographicSize = 7.9f;
       // gameObject.SetActive(false);
    }

	private void Start()
	{
        interval = UpgradeManager.instance.interval;
	}

	public IEnumerator StartShooting()
    {
        foreach (GameObject bulletCreater in parent)
        {
            if (bulletCreater.transform.parent.gameObject.activeSelf)
            {
                GameObject bulletCreated = Instantiate(bullet, bulletCreater.transform, false);
                bulletCreated.transform.SetParent(null);
                if(bullet == LargeBullets){
                    UpgradeManager.instance.DoubleBullet(); 
                }
                foreach (var sound in bulletSounds)
                {
                    if(bulletname == sound.bulletName)
                    {
                        audioSource.clip = sound.sound;
                        audioSource.Play();
                    }
                }
                yield return new WaitForSeconds(interval);
                StartCoroutine(StartShooting());
            }
        }
    }

    public void ResumeShooting()
    {
        bullet = smallBullets;
        bulletname = "Normal";
        StartCoroutine(StartShooting());
    }

    public void LargeBulletsShooting()
    {
        bullet = LargeBullets;
        bulletname = "Large";
        Invoke("SmallBulletsShooting", 5f);
    }

    public void SmallBulletsShooting()
    {
        bullet = smallBullets;
        bulletname = "Normal";
    }

    public void TripleBulletShooting()
    {
        CancelInvoke("SmallBulletsShooting");
        bullet = tripleBullet;
        bulletname = "Triple";
        Invoke("SmallBulletsShooting", 5f);
    }

}


[System.Serializable]
public class BulletSounds{
    public AudioClip sound;
    public string bulletName;
}
