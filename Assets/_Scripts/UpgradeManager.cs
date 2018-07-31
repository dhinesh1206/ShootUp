using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
 
    public static UpgradeManager instance;

    public float interval = 0.4f,intervalUpgradeValue,totalCoins;
    public float hitCount;
   
    public float damageUpgradeLevel, damageUpgradeValue, intervalUpgradeLevel;
    public List<UpgradeCost> damageUpgradeCosts,intervalUpgradeCost;
    bool doublebulletActive;


    private void Awake()
	{
        instance = this;
        damageUpgradeLevel = PlayerPrefs.GetFloat("Damage_Level",1);
        damageUpgradeValue = PlayerPrefs.GetFloat("Damage_Value",1);
        intervalUpgradeLevel = PlayerPrefs.GetFloat("Interval_level",1);
        intervalUpgradeValue = PlayerPrefs.GetFloat("Interval_Value",0.2f);
        totalCoins = PlayerPrefs.GetFloat("Total_Coins", 10000000);

        if (damageUpgradeLevel > 0)
        {
            hitCount = damageUpgradeValue;
        }

        if (intervalUpgradeLevel > 0)
        {
            interval = intervalUpgradeValue;
        }

	}

    public void damageUpgrade()
    {
        damageUpgradeLevel = PlayerPrefs.GetFloat("Damage_Level", 1);
        damageUpgradeLevel += 1;
        foreach(var cost in damageUpgradeCosts)
        {
            if (cost.upgradeLevel == damageUpgradeLevel)
            {
                hitCount = cost.upgradeValue;

                PlayerPrefs.SetFloat("Damage_Level", damageUpgradeLevel);
                PlayerPrefs.SetFloat("Damage_Value", hitCount);
                UpgrageMenuDetails.instance.UpdateMenu();
            } 
            if (cost.upgradeLevel == damageUpgradeLevel -1)
            {
                float totalCoin = PlayerPrefs.GetFloat("Total_Coins", 10000000);
                PlayerPrefs.SetFloat("Total_Coins", (totalCoin - cost.upgradeCost));
            }

        }
    }

    public void DoubleBullet()
    {
        if (!doublebulletActive)
        {
            doublebulletActive = true;
            hitCount = hitCount * 2;
            Invoke("NormalBullet", 5f);
        }
    }

    public void NormalBullet()
    {
        doublebulletActive = false;
        hitCount = hitCount / 2;
    }

    public void intervalUpgrade()
    {
        intervalUpgradeLevel = PlayerPrefs.GetFloat("Interval_level", 1);
        intervalUpgradeLevel += 1;
        foreach (var cost in intervalUpgradeCost)
        {
            if(cost.upgradeLevel == intervalUpgradeLevel)
            {
                interval = cost.upgradeValue/10;
                ShootStart.instance.interval = interval;
                PlayerPrefs.SetFloat("Interval_level", intervalUpgradeLevel);
                PlayerPrefs.SetFloat("Interval_Value", interval);
                UpgrageMenuDetails.instance.UpdateMenu();
            }
            if (cost.upgradeLevel == intervalUpgradeLevel - 1)
            {
                float totalCoin = PlayerPrefs.GetFloat("Total_Coins",10000000);
                PlayerPrefs.SetFloat("Total_Coins", (totalCoin - cost.upgradeCost));
            }
        }
    }
}

[System.Serializable]
public class UpgradeCost{
    public float upgradeLevel,upgradeCost, upgradeValue;
}