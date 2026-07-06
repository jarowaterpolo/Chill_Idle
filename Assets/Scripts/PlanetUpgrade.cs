using TMPro;
using UnityEngine;

public class PlanetUpgrade : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    [SerializeField] private int cost;
    [SerializeField] private TMP_Text upgradeText;
    [SerializeField] private string upgradeDecription;

    private string _upgradeTextString;

    void Start()
    {
        if (manager == null)
        {
            manager = FindAnyObjectByType<GameManager>();
        }

        _upgradeTextString = "cost: " + manager.textFormatter.ReturnText(cost) + "\n" + upgradeDecription;
        UpdateText(_upgradeTextString);
    }

    public void UpgradeStarGainMult()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        if (manager.data.planetStargainMult < 10)
        {
            manager.data.planetStargainMult = 10;
        }
        else
        {
            manager.data.planetStargainMult += 10;
        }
        manager.SetStarGain();
        manager.UpdateText();
    }

    public void IncreasePlanetGain()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        manager.data.planetGainBonus += 1;
        manager.SetStarGain();
        manager.UpdateText();
    }

    public void IncreasePlanetGainMult()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        manager.data.planetGainMult += 1;
        manager.SetStarGain();
        manager.UpdateText();
    }

    public void IncreasePlanetStarGainPower()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        manager.data.planetStarGainPower += 1;
        manager.SetStarGain();
        manager.UpdateText();
    }

    private void UpdateText(string text)
    {
        upgradeText.text = $"{text}";
    }
}
