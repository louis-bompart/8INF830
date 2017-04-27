﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

public class BaseUpgradeManager : MonoBehaviour
{
    #region Gauge preparation
    private List<GameObject> healthGaugeGrads = new List<GameObject>();
    private List<GameObject> armorGaugeGrads = new List<GameObject>();
    private List<GameObject> damageGaugeGrads = new List<GameObject>();
    private List<GameObject> fireRateGaugeGrads = new List<GameObject>();
    private List<GameObject> topSpeedGaugeGrads = new List<GameObject>();
    private List<GameObject> handlingGaugeGrads = new List<GameObject>();
    private BaseStats stats;
    private /*static*/ int scanRangeStat = 0;
    private /*static*/ int printerStat = 0;
    private /*static*/ int numberDronesStat = 0;
    private /*static*/ int fireDroneStat = 0;
    private /*static*/ int shieldStat = 0;
    private /*static*/ int chargeStat = 0;
    private /*static*/ int basePoints = 0;
    private /*static*/ int dronesPoints = 0;
    private /*static*/ int shieldPoints = 0;

    public InventoryController inventoryController;

    public GameObject scanGauge;
    public GameObject printerGauge;
    public GameObject numberDroneGauge;
    public GameObject fireRateGauge;
    public GameObject maxShieldGauge;
    public GameObject rechargeRateGauge;

    private void Awake()
    {
        inventoryController = new List<InventoryController>(Data.instance.GetComponentsInChildren<InventoryController>()).Find(x => x.inventoryType == InventoryController.Inventory.Both);
    }

    #endregion

    public void Start()
    {
        #region Gauge init
        //invControl = GameObject.Find("ShipInventoryC").GetComponent<InventoryController>();
        if (inventoryController == null)
        {
            Debug.LogError("InventoryController not attached !", this);
        }
        UpdateUpgradeCount();

        stats = Data.instance.GetComponentInChildren<BaseStats>();
        foreach (Transform child in scanGauge.transform)
        {
            healthGaugeGrads.Add(child.gameObject);
        }
        foreach (Transform child in printerGauge.transform)
        {
            armorGaugeGrads.Add(child.gameObject);
        }
        foreach (Transform child in numberDroneGauge.transform)
        {
            damageGaugeGrads.Add(child.gameObject);
        }
        foreach (Transform child in fireRateGauge.transform)
        {
            fireRateGaugeGrads.Add(child.gameObject);
        }
        foreach (Transform child in maxShieldGauge.transform)
        {
            topSpeedGaugeGrads.Add(child.gameObject);
        }
        foreach (Transform child in rechargeRateGauge.transform)
        {
            handlingGaugeGrads.Add(child.gameObject);
        }

        int i = 0;
        foreach (GameObject grad in healthGaugeGrads)
        {
            if (i < stats.scanRangeStat)
            {
                grad.GetComponent<RawImage>().color = Color.green;
                i++;
            }
            else
            {
                i = 0;
                break;
            }
        }
        foreach (GameObject grad in armorGaugeGrads)
        {
            if (i < stats.printerStat)
            {
                grad.GetComponent<RawImage>().color = Color.green;
                i++;
            }
            else
            {
                i = 0;
                break;
            }
        }
        foreach (GameObject grad in damageGaugeGrads)
        {
            if (i < stats.numberDronesStat)
            {
                grad.GetComponent<RawImage>().color = Color.green;
                i++;
            }
            else
            {
                i = 0;
                break;
            }
        }
        foreach (GameObject grad in fireRateGaugeGrads)
        {
            if (i < stats.fireDroneStat)
            {
                grad.GetComponent<RawImage>().color = Color.green;
                i++;
            }
            else
            {
                i = 0;
                break;
            }
        }
        foreach (GameObject grad in topSpeedGaugeGrads)
        {
            if (i < stats.shieldStat)
            {
                grad.GetComponent<RawImage>().color = Color.green;
                i++;
            }
            else
            {
                i = 0;
                break;
            }
        }
        foreach (GameObject grad in handlingGaugeGrads)
        {
            if (i < stats.chargeStat)
            {
                grad.GetComponent<RawImage>().color = Color.green;
                i++;
            }
            else
            {
                i = 0;
                break;
            }
        }
        #endregion
    }

    public GameObject baseCounter;
    public GameObject dronesCounter;
    public GameObject shieldCounter;

    private void UpdateUpgradeCount()
    {

        basePoints = inventoryController.GetQuantity(210);
        baseCounter.GetComponent<Text>().text = basePoints.ToString();
        dronesPoints = inventoryController.GetQuantity(211);
        dronesCounter.GetComponent<Text>().text = dronesPoints.ToString();
        shieldPoints = inventoryController.GetQuantity(212);
        shieldCounter.GetComponent<Text>().text = shieldPoints.ToString();
    }

    #region Button click functions (update the values of the ship's stats)
    public void HealthUpgrade()
    {
        if (basePoints > 0)
        {
            basePoints--;
            baseCounter.GetComponent<Text>().text = basePoints.ToString();
            scanRangeStat += 1;
            int i = 0;
            if (stats.scanRangeStat + scanRangeStat > 5)
            {
                scanRangeStat = 5 - stats.scanRangeStat;
                basePoints++;
            }
            foreach (GameObject grad in healthGaugeGrads)
            {
                if (i < scanRangeStat + stats.scanRangeStat)
                {
                    if (grad.GetComponent<RawImage>().color == Color.white)
                    {
                        grad.GetComponent<RawImage>().color = Color.yellow;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Not enough defense upgrade kits !");
        }
    }

    public void ArmorUpgrade()
    {
        if (basePoints > 0)
        {
            basePoints--;
            printerStat += 1;
            baseCounter.GetComponent<Text>().text = basePoints.ToString();
            int i = 0;
            if (stats.printerStat + printerStat >= 5)
            {
                printerStat = 5 - stats.printerStat;
                basePoints++;
            }
            foreach (GameObject grad in armorGaugeGrads)
            {
                if (i < printerStat + stats.printerStat)
                {
                    if (grad.GetComponent<RawImage>().color == Color.white)
                    {
                        grad.GetComponent<RawImage>().color = Color.yellow;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Not enough defense upgrade kits !");
        }
    }

    public void DamageUpgrade()
    {
        if (dronesPoints > 0)
        {
            dronesPoints--;
            dronesCounter.GetComponent<Text>().text = dronesPoints.ToString();
            numberDronesStat += 1;
            int i = 0;
            if (stats.numberDronesStat + numberDronesStat >= 5)
            {
                numberDronesStat = 5 - stats.numberDronesStat;
                dronesPoints++;
            }
            foreach (GameObject grad in damageGaugeGrads)
            {
                if (i < numberDronesStat + stats.numberDronesStat)
                {
                    if (grad.GetComponent<RawImage>().color == Color.white)
                    {
                        grad.GetComponent<RawImage>().color = Color.yellow;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Not enough attack upgrade kits !");
        }
    }

    public void FireRateUpgrade()
    {
        if (dronesPoints > 0)
        {
            dronesPoints--;
            dronesCounter.GetComponent<Text>().text = dronesPoints.ToString();
            fireDroneStat += 1;
            int i = 0;
            if (stats.fireDroneStat + fireDroneStat >= 5)
            {
                fireDroneStat = 5 - stats.fireDroneStat;
                dronesPoints++;
            }
            foreach (GameObject grad in fireRateGaugeGrads)
            {
                if (i < fireDroneStat + stats.fireDroneStat)
                {
                    if (grad.GetComponent<RawImage>().color == Color.white)
                    {
                        grad.GetComponent<RawImage>().color = Color.yellow;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Not enough attack upgrade kits !");
        }
    }

    public void TopSpeedUpgrade()
    {
        if (shieldPoints > 0)
        {
            shieldPoints--;
            shieldCounter.GetComponent<Text>().text = shieldPoints.ToString();
            shieldStat += 1;
            int i = 0;
            if (stats.shieldStat + shieldStat >= 5)
            {
                shieldStat = 5 - stats.shieldStat;
                shieldPoints++;
            }
            foreach (GameObject grad in topSpeedGaugeGrads)
            {
                if (i < shieldStat + stats.shieldStat)
                {
                    if (grad.GetComponent<RawImage>().color == Color.white)
                    {
                        grad.GetComponent<RawImage>().color = Color.yellow;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Not enough mobility upgrade kits !");
        }
    }

    public void HandlingUpgrade()
    {
        if (shieldPoints > 0)
        {
            shieldPoints--;
            shieldCounter.GetComponent<Text>().text = shieldPoints.ToString();
            chargeStat += 1;
            int i = 0;
            if (stats.chargeStat + chargeStat >= 5)
            {
                chargeStat = 5 - stats.chargeStat;
                shieldPoints++;
            }
            foreach (GameObject grad in handlingGaugeGrads)
            {
                if (i < chargeStat + stats.chargeStat)
                {
                    if (grad.GetComponent<RawImage>().color == Color.white)
                    {
                        grad.GetComponent<RawImage>().color = Color.yellow;
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Not enough mobility upgrade kits !");
        }
    }
    #endregion

    #region Upgrade validate/cancel buttons and functions.
    private void ResetTmpCounters()
    {
        scanRangeStat = 0;
        printerStat = 0;
        numberDronesStat = 0;
        fireDroneStat = 0;
        chargeStat = 0;
        shieldStat = 0;
    }
    public void Validate()
    {

        stats.scanRangeStat += scanRangeStat;
        stats.printerStat += printerStat;
        stats.numberDronesStat += numberDronesStat;
        stats.fireDroneStat += fireDroneStat;
        stats.shieldStat += shieldStat;
        stats.chargeStat += chargeStat;

        inventoryController.RemoveItem(210, (scanRangeStat + printerStat));
        inventoryController.RemoveItem(211, (numberDronesStat + fireDroneStat));
        inventoryController.RemoveItem(212, (shieldStat + chargeStat));

        ResetTmpCounters();
        UpdateUpgradeCount();


        foreach (GameObject grad in healthGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.green;
            }
        }
        foreach (GameObject grad in armorGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.green;
            }
        }
        foreach (GameObject grad in damageGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.green;
            }
        }
        foreach (GameObject grad in fireRateGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.green;
            }
        }
        foreach (GameObject grad in topSpeedGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.green;
            }
        }
        foreach (GameObject grad in handlingGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.green;
            }
        }
    }

    public void Cancel()
    {
        ResetTmpCounters();
        UpdateUpgradeCount();
        baseCounter.GetComponent<Text>().text = basePoints.ToString();
        dronesCounter.GetComponent<Text>().text = dronesPoints.ToString();
        shieldCounter.GetComponent<Text>().text = shieldPoints.ToString();
        foreach (GameObject grad in healthGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.white;
            }
        }
        foreach (GameObject grad in armorGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.white;
            }
        }
        foreach (GameObject grad in damageGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.white;
            }
        }
        foreach (GameObject grad in fireRateGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.white;
            }
        }
        foreach (GameObject grad in topSpeedGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.white;
            }
        }
        foreach (GameObject grad in handlingGaugeGrads)
        {
            if (grad.GetComponent<RawImage>().color == Color.yellow)
            {
                grad.GetComponent<RawImage>().color = Color.white;
            }
        }
    }
    #endregion
}
