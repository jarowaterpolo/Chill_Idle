using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NovaPrestige : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    [SerializeField] private TMP_Text novaRewardText;
    private double _novaReward;

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
        if (manager.data.planets < 1e7)
        {
            _novaReward = 0;
            SetNovaPrestigeText();
        }
        else
        {
            _novaReward = Math.Floor(Math.Log10(manager.data.planets) - 6);
            SetNovaPrestigeText();
        }
    }

    private void SetNovaPrestigeText()
    {
        novaRewardText.text = "prestige now for " + manager.textFormatter.ReturnText(manager.data.currentNotation, _novaReward) + " nova(s)";
    }

    public void PrestigeNova()
    {
        manager.data.nova += _novaReward;
        manager.ResetPlanets();
        SetNovaPrestigeText();
    }
}
