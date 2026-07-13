using System.Collections;
using TMPro;
using UnityEngine;
using NaughtyAttributes;
using System;

public class GameManager : MonoBehaviour
{
    public TextFormatter textFormatter = new();

    private DataSaver dataSaver = new();
    public Data data = new Data();

    //star
    [SerializeField] private TMP_Text starText;
    [SerializeField] private TMP_Text starGainText;
    [SerializeField] private GameObject AdvancedUpgrades;

    //planet
    [SerializeField] private GameObject planetTabButton;
    [SerializeField] private TMP_Text planetText;
    
    private AutobuyManager autoBuyManager;

    void Start()
    {
        if (autoBuyManager == null)
        {
            autoBuyManager = FindAnyObjectByType<AutobuyManager>();
        }

        SaveData saveData = dataSaver.LoadGameData().SaveData;
        data = DataConverter.FromSave(saveData);
        SetStarGain();
        UpdateText();

        StartCoroutine(StarGainPerSecond());

        StartCoroutine(CheckStars());
        
        if (data.produceStarGainMult == true)
        {
            StartCoroutine(StarGainMultPerSecond());
        }

        autoBuyManager.ActiveAutobuyerRestart();
    }

    public IEnumerator StarGainPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            data.stars += data.totalStarGain;
            UpdateText();
        }
    }

    public IEnumerator StarGainMultPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            data.starGainMult += data.starGainMultRate;
            SetStarGain();
            UpdateText();
        }
    }

    private IEnumerator CheckStars()
    {
        while (!data.milestones[0])
        {
            yield return null;
            if (data.stars >= 1e6)
            {
                data.milestones[0] = true;
            }
        }
        planetTabButton.SetActive(true);
        Save();
        while (!data.milestones[1]) 
        {
            yield return null;
            if (data.stars >= 1e10)
            {
                data.milestones[1] = true;
            }
        }
        AdvancedUpgrades.SetActive(true);
        Save();
    }

    public void UpdateText()
    {
        //stars
        var starsString = textFormatter.ReturnText(data.stars);
        var starGainString = textFormatter.ReturnText(data.totalStarGain) + " stars/s";

        starText.text = starsString;
        starGainText.text = starGainString;

        //planets
        var planetsString = textFormatter.ReturnText(data.planets);

        planetText.text = planetsString;
    }


    public double GetStars()
    {
        return data.stars;
    }
    public void DecereaseStars(double amount)
    {
        data.stars -= amount;
        UpdateText();
    }

    public void SetStarGain()
    {
        data.totalStarGain = data.baseStarGain * data.starGainMult * data.planetStargainMult * Math.Pow(2, data.planetStarGainPower);
    }
    public double GetPlanets()
    {
        return data.planets;
    }
    public void DecereasePlanets(int amount)
    {
        data.planets -= amount;
        UpdateText();
    }

    public void ResetStars()
    {
        data.stars = 0;
        data.baseStarGain = 1;
        data.starGainMult = 1;
        data.produceStarGainMult = false;
        data.starGainMultRate = 0;
        StopCoroutine(StarGainMultPerSecond());
        SetStarGain();
        UpdateText();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save();
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        SaveData saveData = DataConverter.ToSave(data);
        dataSaver.SaveGameData(saveData);
    }

    [Button]
    public void ResetData()
    {
        dataSaver.ResetData();
    }
}
