using TMPro;
using UnityEngine;

public class StarUpgrade : MonoBehaviour
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

        _upgradeTextString = "cost: " + manager.textFormatter.ReturnText(cost) + $"\n stargain {upgradeDecription}";
        UpdateText(_upgradeTextString);
    }

    public void UpgradeStarGainAddition()
    {
        if (manager.GetStars() < cost) return;
        IncreaseStarGain(1);
        manager.DecereaseStars(cost);
    }

    public void UpgradeStarGainMult()
    {
        if (manager.GetStars() < cost) return;
        UpMultStarGain(1);
        manager.DecereaseStars(cost);
    }
    public void UpgradeProduceStarGainMult()
    {
        if (manager.GetStars() < cost) return;
        ProduceStarGainMult(1);
        manager.DecereaseStars(cost);
    }

    private void UpdateText(string text)
    {
        upgradeText.text = $"{text}";
    }

    public void IncreaseStarGain(int amount)
    {
        manager.data.baseStarGain += amount;
        manager.SetStarGain();
        manager.UpdateText();
    }
    public void UpMultStarGain(int amount)
    {
        manager.data.starGainMult += amount;
        manager.SetStarGain();
        manager.UpdateText();
    }
    public void ProduceStarGainMult(int amount)
    {
        if (manager.data.produceStarGainMult == false)
        {
            manager.data.produceStarGainMult = true;
            StartCoroutine(manager.StarGainMultPerSecond());
        }
        manager.data.starGainMultRate += amount;
        manager.UpdateText();
    }
}
