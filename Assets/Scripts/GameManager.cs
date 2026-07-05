using System.Collections;
using TMPro;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    public TextFormatter textFormatter = new();

    private DataSaver dataSaver = new();
    public Data data = new Data();

    //star
    [SerializeField] private TMP_Text starText;
    [SerializeField] private TMP_Text starGainText;

    //planet
    [SerializeField] private GameObject planetTabButton;
    [SerializeField] private TMP_Text planetText;

    public bool[] Milestone = new bool[2];
    
    void Start()
    {
        data = dataSaver.LoadGameData().Data;
        UpdateText();

        for (int i = 0; i < Milestone.Length; i++)
        {
            Milestone[i] = false;
        }

        StartCoroutine(StarGainPerSecond());

        StartCoroutine(CheckMillionStars());
        
        if (data.produceStarGainMult == true)
        {
            StartCoroutine(StarGainMultPerSecond());
        }
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

    private IEnumerator CheckMillionStars()
    {
        while (!Milestone[0])
        {
            yield return null;
            if (data.stars >= 1e6)
            {
                planetTabButton.SetActive(true);
                Milestone[0] = true;
            }
        }
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

    private void OnApplicationQuit()
    {
        dataSaver.SaveGameData(data);
    }

    public int GetStars()
    {
        return data.stars;
    }
    public void DecereaseStars(int amount)
    {
        data.stars -= amount;
        UpdateText();
    }

    public void SetStarGain()
    {
        data.totalStarGain = data.baseStarGain * data.starGainMult * data.planetStargainMult;
    }
    public int GetPlanets()
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


    [Button]
    public void ResetData()
    {
        dataSaver.ResetData();
    }
}
