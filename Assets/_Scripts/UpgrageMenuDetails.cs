using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgrageMenuDetails : MonoBehaviour {

    public static UpgrageMenuDetails instance;
    public Text CurrentCoinText, nextDamageLevel, nextIntervalLevel, nextDamageRate, nextIntervalRate,highScoreText,nextDamageRate2, nextIntervalRate2;
    public Button upgradeLevelUpgradeButton, intervalLevelUpgradeButton,upgradeLevelUpgradeButton2, intervalLevelUpgradeButton2;
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
            highScoreText.text = (highScore / 1000).ToString("f1") + "k";
        }
    }


    public void ReloadCurrentLevel()
    {
        
    }

    public void UpdateMenu()
	{
        float coin = PlayerPrefs.GetFloat("Total_Coins", 0);
        if(coin.ToString().Length <4)
        {
            CurrentCoinText.text = coin.ToString();  
        } else {
            CurrentCoinText.text = (coin / 1000).ToString("f1") + "K";
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
                    if(cost.upgradeCost.ToString().Length < 4)
                    {
                        nextDamageRate2.text = cost.upgradeCost.ToString();
                        nextDamageRate.text = cost.upgradeCost.ToString();  
                       
                    } 
                    else
                    {
                        nextDamageRate.text = (cost.upgradeCost / 1000).ToString() + "K";
                        nextDamageRate2.text = (cost.upgradeCost / 1000).ToString() + "K";
                    }

                  
                    if (coin > cost.upgradeCost)
                    {
                        upgradeLevelUpgradeButton.enabled = true;
                        upgradeLevelUpgradeButton2.enabled = true;
                        upgradeLevelUpgradeButton.gameObject.SetActive(false);
                        upgradeLevelUpgradeButton2.gameObject.SetActive(true);
                        damageIndicatorNormal.SetActive(false);
                        damageupgradeIndicatorAnimated.SetActive(true);
                    }
                    else
                    {
                        upgradeLevelUpgradeButton.enabled = false;
                        upgradeLevelUpgradeButton2.enabled = false;
                        upgradeLevelUpgradeButton.gameObject.SetActive(true);
                        upgradeLevelUpgradeButton2.gameObject.SetActive(false);
                        damageIndicatorNormal.SetActive(true);
                        damageupgradeIndicatorAnimated.SetActive(false);
                      //  damageIndicator.color = normalColor;
                    }
                }
                else
                {
                    nextDamageLevel.text = "MAX OUT";
                    nextDamageRate.text = "-";
                    nextDamageRate2.text = "-";
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

                    if (cost.upgradeCost.ToString().Length < 4)
                    {
                        nextIntervalRate.text = cost.upgradeCost.ToString();
                        nextIntervalRate2.text = cost.upgradeCost.ToString();

                    }
                    else
                    {
                        nextIntervalRate.text = (cost.upgradeCost / 1000).ToString() + "K";
                        nextIntervalRate2.text = (cost.upgradeCost / 1000).ToString() + "K";
                    }
                   
                    if (coin > cost.upgradeCost)
                    {
                        
                        intervalLevelUpgradeButton.enabled = true;
                        intervalLevelUpgradeButton2.enabled = true;

                        intervalLevelUpgradeButton.gameObject.SetActive(false);
                        intervalLevelUpgradeButton2.gameObject.SetActive(true);
                        intervalIndicatorAnimater.SetActive(true);
                        intervalIndiCatorNormal.SetActive(false);
                       
                    }
                    else
                    {

                        intervalLevelUpgradeButton.enabled = false;
                        intervalLevelUpgradeButton2.enabled = false;
                        intervalLevelUpgradeButton.gameObject.SetActive(true);
                        intervalLevelUpgradeButton2.gameObject.SetActive(false);

                        intervalIndicatorAnimater.SetActive(false);
                        intervalIndiCatorNormal.SetActive(true);
                    }
                }
                else
                {
                    nextIntervalLevel.text = "MAX OUT";
                    nextIntervalRate.text = "-";
                    nextIntervalRate2.text = "-";
                }
            }
        }
    }
}
