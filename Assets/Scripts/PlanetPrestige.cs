using System.Collections;
using TMPro;
using UnityEngine;

public class PlanetPrestige : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    [SerializeField] private TMP_Text planetRewardText;
    private int _planetReward;

    private void Start()
    {
        if (manager == null)
        {
            manager = FindAnyObjectByType<GameManager>();
        }
    }

    private void Update()
    {
        PrestigeSetter();
    }
    private void PrestigeSetter()
    {
        if (manager.data.stars < 1e6)
        {
            _planetReward = 0;
            SetPlanetPrestigeText();
        }
        else
        {
            _planetReward = Mathf.RoundToInt(Mathf.Log(manager.data.stars, 10f));
            SetPlanetPrestigeText();
        }
    }

    private void SetPlanetPrestigeText()
    {
        planetRewardText.text = $"prestige now for {_planetReward} planets";
    }

    public void PrestigePlanet()
    {
        manager.data.planets += _planetReward;
        manager.ResetStars();
        SetPlanetPrestigeText();
    }
}
