using System.Collections;
using TMPro;
using UnityEngine;
using NaughtyAttributes;
using System;

public class GameManager : MonoBehaviour
{
    public TextFormatter textFormatter = new();
    public Action OnFormattingChange;

    private DataSaver dataSaver = new();
    [HideInInspector] public Data data = new Data();

    //star
    [SerializeField] private TMP_Text starText;
    [SerializeField] private TMP_Text starGainText;
    [SerializeField] private GameObject AdvancedUpgrades;

    //planet
    [SerializeField] private GameObject planetTabButton;
    [SerializeField] private TMP_Text planetText;

    //nova
    [SerializeField] private GameObject novaTabButton;

    private AutobuyManager autoBuyManager;
    private void Awake()
    {
        SaveData saveData = dataSaver.LoadGameData().SaveData;
        data = DataConverter.FromSave(saveData);
        
    }
    void Start()
    {
        OnFormattingChange += UpdateText;

        if (autoBuyManager == null)
        {
            autoBuyManager = FindAnyObjectByType<AutobuyManager>();
        }

        SetStarGain();
        UpdateText();

        StartCoroutine(StarGainPerSecond());

        StartCoroutine(CheckMilestones());
        
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

    private IEnumerator CheckMilestones()
    {
        while (true)
        {
            yield return null;
            //M0
            if (data.stars >= 1e6)
            {
                data.milestones[0] = true;
                planetTabButton.SetActive(true);
                Save();
            }
            //M1
            if (data.stars >= 1e10)
            {
                data.milestones[1] = true;
                AdvancedUpgrades.SetActive(true);
                Save();
            }
            //M2
            if (data.planets >= 1e7)
            {
                data.milestones[2] = true;
                novaTabButton.SetActive(true);
                Save();
            }

            bool allMilestonesCompleted = true;
            foreach (bool milestone in data.milestones)
            {
                if (!milestone)
                {
                    allMilestonesCompleted = false;
                    break;
                }
            }

            if (allMilestonesCompleted)
            {
                yield break;
            }
        }
    }

    public void UpdateText()
    {
        if (data.currentNotation == notationType.shortend || data.currentNotation == notationType.normal)
        {
            if (data.stars >= 1e36)
            {
                data.currentNotation = notationType.scientific;
            }
        }
        //stars
        var starsString = textFormatter.ReturnText(data.currentNotation, data.stars);
        var starGainString = textFormatter.ReturnText(data.currentNotation, data.totalStarGain) + " stars/s";

        starText.text = starsString;
        starGainText.text = starGainString;

        //planets
        var planetsString = textFormatter.ReturnText(data.currentNotation, data.planets);

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
        data.totalStarGain = data.baseStarGain * data.starGainMult * data.planetStargainMult /* * Math.Pow(2, data.planetStarGainPower)*/;
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
        data.starPlanetGainIncrease = 0;
        StopCoroutine(StarGainMultPerSecond());
        SetStarGain();
        UpdateText();
    }

    public void ResetPlanets()
    {
        data.planets = 0;
        data.planetGainBonus = 0;
        data.planetGainMult = 1;
        data.planetStargainMult = 1;
        //data.planetStarGainPower = 0;
        data.autobuyers = new Autobuyer[]
        {
            new Autobuyer{ type = AutobuyerType.StarGainAddition, isActive = false, buyAmount = 1, buyDelay = 1f},
            new Autobuyer{ type = AutobuyerType.StarGainMultProducer, isActive = false, buyAmount = 1, buyDelay = 1f },
            new Autobuyer{ type = AutobuyerType.StarPlanetGain, isActive = false, buyAmount = 1, buyDelay = 1f }
        };
        ResetStars();
    }

    public void FormatChangeTrigger()
    {
        OnFormattingChange?.Invoke();
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
