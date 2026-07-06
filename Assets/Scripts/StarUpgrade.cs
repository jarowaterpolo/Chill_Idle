using TMPro;
using UnityEngine;

public class StarUpgrade : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    [SerializeField] private double cost;
    [SerializeField] private TMP_Text upgradeText;
    [SerializeField] private string upgradeDecription;

    private string _upgradeTextString;

    void Start()
    {
        if (manager == null)
        {
            manager = FindAnyObjectByType<GameManager>();
        }

        _upgradeTextString = "cost: " + manager.textFormatter.ReturnText(cost) + $"\n stargain {upgradeDecription}";
        UpdateText(_upgradeTextString);
    }

    public void UpgradeStarGainAddition()
    {
        if (manager.GetStars() < cost) return;
        manager.DecereaseStars(cost);

        manager.data.baseStarGain += 1;
        manager.SetStarGain();
        manager.UpdateText();
    }

    public void UpgradeStarGainMult()
    {
        if (manager.GetStars() < cost) return;
        manager.DecereaseStars(cost);

        manager.data.starGainMult += 1;
        manager.SetStarGain();
        manager.UpdateText();
    }

    public void UpgradeProduceStarGainMult()
    {
        if (manager.GetStars() < cost) return;
        manager.DecereaseStars(cost);

        if (manager.data.produceStarGainMult == false)
        {
            manager.data.produceStarGainMult = true;
            StartCoroutine(manager.StarGainMultPerSecond());
        }
        manager.data.starGainMultRate += 1;
        manager.UpdateText();
    }

    public void IncreasePlanetGain()
    {
        if (manager.GetStars() < cost) return;
        manager.DecereaseStars(cost);

        manager.data.starPlanetGainIncrease += 1;
        manager.SetStarGain();
        manager.UpdateText();
    }

    private void UpdateText(string text)
    {
        upgradeText.text = $"{text}";
    }
}
