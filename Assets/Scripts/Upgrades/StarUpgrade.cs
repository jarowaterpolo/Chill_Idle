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

        _upgradeTextString = "cost: " + manager.textFormatter.ReturnText(manager.data.currentNotation, cost) + $"\n stargain {upgradeDecription}";
        UpdateText();

        if (upgradeText != null)
        {
            manager.OnFormattingChange += UpdateText;
        }
    }

    public void UpgradeStarGainAddition()
    {
        Debug.Log("tried to buy stargain addition");

        if (manager.GetStars() < cost)
        {
            Debug.Log($"not enough stars");
            return;
        }
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
        Debug.Log("tried to buy stargain produce mult");

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
        Debug.Log("tried to buy star upgrade planet gain increase");

        if (manager.GetStars() < cost) return;
        manager.DecereaseStars(cost);

        manager.data.starPlanetGainIncrease += 1;
        manager.SetStarGain();
        manager.UpdateText();
    }

    private void UpdateText()
    {
        _upgradeTextString = "cost: " + manager.textFormatter.ReturnText(manager.data.currentNotation, cost) + $"\n stargain {upgradeDecription}";
        if (string.IsNullOrEmpty(_upgradeTextString) || upgradeText == null) return;
        upgradeText.text = $"{_upgradeTextString}";
    }
}
