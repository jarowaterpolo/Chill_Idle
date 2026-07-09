using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AutobuyManager : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private StarUpgrade starUpgrade;
    [SerializeField] private PlanetUpgrade planetUpgrade;

    [SerializeField] private AutobuyerUI[] autobuyerUIs;

    private void Awake()
    {
        if (manager == null)
        {
            manager = FindAnyObjectByType<GameManager>();
        }        
    }

    private void Start()
    {
        foreach (var autobuyer in manager.data.autobuyers)
        {
            foreach (var ui in autobuyerUIs)
            {
                if (autobuyer.type == ui.type)
                {
                    if (autobuyer.isActive == true)
                    {
                        ui.unlockButton.SetActive(false);
                    }

                    if (autobuyer.buyDelay <= .1f)
                    {
                        ui.speedupButton.SetActive(false);
                    }
                }
            }
        }

        SetAutobuyerTexts();
    }

    public void ActiveAutobuyerRestart()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainAddition);
        if (autobuyer.isActive == true)
        {
            StartCoroutine(StarGainAdditionAutobuyer());
        }
        autobuyer = GetAutobuyer(AutobuyerType.StarGainMultProducer);
        if (autobuyer.isActive == true)
        {
            StartCoroutine(StarGainMultProducerAutobuyer());
        }
    }

    public void StartStargainAdditionAutobuyer()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainAddition);
        autobuyer.isActive = true;
        StartCoroutine(StarGainAdditionAutobuyer());
    }

    public IEnumerator StarGainAdditionAutobuyer()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainAddition);
        while (autobuyer.isActive == true)
        {
            SetAutobuyerTexts();
            //Debug.Log($"stargain addittion autobuyer isactive == {manager.data.starGainAdditionAutobuyer.isActive} and needs to wait {manager.data.starGainAdditionAutobuyer.buyDelay} sec to buy {manager.data.starGainAdditionAutobuyer.buyAmount} upgrades");
            yield return new WaitForSeconds(autobuyer.buyDelay);
            for (int i = 0; i < autobuyer.buyAmount; i++)
            {
                starUpgrade.UpgradeStarGainAddition();
            }
        }
    }

    public void StartStargainMultProducerAutobuyer()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainMultProducer);
        autobuyer.isActive = true;
        StartCoroutine(StarGainMultProducerAutobuyer());
    }

    public IEnumerator StarGainMultProducerAutobuyer()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainMultProducer);
        while (autobuyer.isActive == true)
        {
            SetAutobuyerTexts();
            //Debug.Log($"stargain addittion autobuyer isactive == {manager.data.starGainAdditionAutobuyer.isActive} and needs to wait {manager.data.starGainAdditionAutobuyer.buyDelay} sec to buy {manager.data.starGainAdditionAutobuyer.buyAmount} upgrades");
            yield return new WaitForSeconds(autobuyer.buyDelay);
            for (int i = 0; i < autobuyer.buyAmount; i++)
            {
                starUpgrade.UpgradeProduceStarGainMult();
            }
        }
    }

    public void SetAutobuyerTexts()
    {
        foreach (var autobuyer in manager.data.autobuyers)
        {
            foreach (var ui in autobuyerUIs)
            {
                if (autobuyer.type == ui.type)
                {
                    ui.funcionText.text = $"buys {autobuyer.buyAmount} upgrades per {Math.Round(autobuyer.buyDelay,1)} seconds";
                }
            }
        }
    }

    public Autobuyer GetAutobuyer(AutobuyerType type)
    {
        foreach (Autobuyer buyer in manager.data.autobuyers)
        {
            if (buyer.type == type)
                return buyer;
        }

        return null;
    }
}

[System.Serializable]
public class Autobuyer
{
    public AutobuyerType type;
    public bool isActive;
    public int buyAmount;
    public float buyDelay;
}

[System.Serializable]
public class AutobuyerUI
{
    public AutobuyerType type;
    public GameObject unlockButton;
    public GameObject speedupButton;
    public TMP_Text funcionText;
}
