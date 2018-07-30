using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgrageMenuDetails : MonoBehaviour {

    public static UpgrageMenuDetails instance;
    public Text CurrentCoinText, nextDamageLevel, nextIntervalLevel, nextDamageRate, nextIntervalRate,highScoreText;
    public Button upgradeLevelUpgradeButton, intervalLevelUpgradeButton;
    public GameObject intervalIndiCatorNormal, damageIndicatorNormal,intervalIndicatorAnimater,damageupgradeIndicatorAnimated;


    public UpgradeManager upgradeManager;


    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        UpdateMenu();

        float highScore = PlayerPrefs.GetFloat("HighScore",0);

        if (highScore.ToString().Length < 4)
        {
            highScoreText.text = highScore.ToString();
        }
        else
        {
            highScoreText.text = (highScore / 1000).ToString() + "k";
        }
    }
    public void UpdateMenu()
	{
        float coin = PlayerPrefs.GetFloat("Total_Coins", 2000);
        if(coin.ToString().Length <4)
        {
            CurrentCoinText.text = coin.ToString();  
        } else {
            CurrentCoinText.text = (coin / 1000).ToString("f2") + "K";
        }

        float damageLevel = PlayerPrefs.GetFloat("Damage_Level", 1);
        foreach (var cost in upgradeManager.damageUpgradeCosts)
        {
            if (cost.upgradeLevel == (damageLevel))
            {

                /// print(upgradeManager.damageUpgradeCosts.Count > damageLevel);
                if (upgradeManager.damageUpgradeCosts.Count > damageLevel)
                {
                    nextDamageLevel.text = "LEVEL  " + (damageLevel).ToString();
                    // print(coin > cost.upgradeCost);
                    nextDamageRate.text = cost.upgradeCost.ToString();
                    if (coin > cost.upgradeCost)
                    {
                        upgradeLevelUpgradeButton.enabled = true;
                        damageIndicatorNormal.SetActive(false);
                        damageupgradeIndicatorAnimated.SetActive(true);
                    }
                    else
                    {
                        upgradeLevelUpgradeButton.enabled = false;
                        damageIndicatorNormal.SetActive(true);
                        damageupgradeIndicatorAnimated.SetActive(false);
                      //  damageIndicator.color = normalColor;
                    }
                }
                else
                {
                    nextDamageLevel.text = "MAX OUT";
                    nextDamageRate.text = "-";
                }
            }
        }


        float intervalLvel = PlayerPrefs.GetFloat("Interval_level", 1);

        foreach (var cost in upgradeManager.intervalUpgradeCost)
        {
            if (cost.upgradeLevel == (intervalLvel))
            {
                //print(upgradeManager.intervalUpgradeCost.Count > intervalLvel);
                if (upgradeManager.intervalUpgradeCost.Count > intervalLvel)
                {
                    nextIntervalLevel.text = "LEVEL  " + (intervalLvel).ToString();
                    // print(coin > cost.upgradeCost);
                    nextIntervalRate.text = cost.upgradeCost.ToString();
                    if (coin > cost.upgradeCost)
                    {
                        intervalLevelUpgradeButton.enabled = true;
                        intervalIndicatorAnimater.SetActive(true);
                        intervalIndiCatorNormal.SetActive(false);
                       
                    }
                    else
                    {
                        intervalLevelUpgradeButton.enabled = false;
                       
                        intervalIndicatorAnimater.SetActive(false);
                        intervalIndiCatorNormal.SetActive(true);
                    }
                }
                else
                {
                    nextIntervalLevel.text = "MAX OUT";
                    nextIntervalRate.text = "-";
                }
            }
        }


    }
}
