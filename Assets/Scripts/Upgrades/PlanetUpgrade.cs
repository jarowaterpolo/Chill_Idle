using TMPro;
using UnityEngine;

public class PlanetUpgrade : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private AutobuyManager autobuyManager;

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

        if (autobuyManager == null)
        {
            autobuyManager = FindAnyObjectByType<AutobuyManager>();
        }

        _upgradeTextString = "cost: " + manager.textFormatter.ReturnText(manager.data.currentNotation, cost) + "\n" + upgradeDecription;
        UpdateText();

        if (upgradeText != null)
        {
            manager.OnFormattingChange += UpdateText;
        }
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
    public void UnlockAutoBuyStargainAddition()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        Debug.Log("autobuyer bought");
        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarGainAddition);
        if (autobuyer.isActive == false)
        {
            autobuyManager.StartStargainAdditionAutobuyer();
        }

        manager.SetStarGain();
        manager.UpdateText();

        gameObject.SetActive(false);
    }

    public void SpeedUpAutoBuyStargainAddition()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarGainAddition);

        if (autobuyer.buyDelay > .1f)
        {
            autobuyer.buyDelay -= .1f;
            if (autobuyer.buyDelay <= .1f)
            {
                gameObject.SetActive(false);
            }
        }

        manager.SetStarGain();
        manager.UpdateText();
    }

    public void AmountUpAutoBuyStargainAddition()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarGainAddition);
        autobuyer.buyAmount++;

        manager.SetStarGain();
        manager.UpdateText();
    }

    public void UnlockAutoBuyStargainMultProducer()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        Debug.Log("autobuyer bought");
        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarGainMultProducer);
        if (autobuyer.isActive == false)
        {
            autobuyManager.StartStargainMultProducerAutobuyer();
        }

        manager.SetStarGain();
        manager.UpdateText();

        gameObject.SetActive(false);
    }

    public void SpeedUpAutoBuyStargainMultProducer()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarGainMultProducer);

        if (autobuyer.buyDelay > .1f)
        {
            autobuyer.buyDelay -= .1f;
            if (autobuyer.buyDelay <= .1f)
            {
                gameObject.SetActive(false);
            }
        }

        manager.SetStarGain();
        manager.UpdateText();
    }

    public void AmountUpAutoBuyStargainMultProducer()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarGainMultProducer);
        autobuyer.buyAmount++;

        manager.SetStarGain();
        manager.UpdateText();
    }

    public void UnlockAutoBuyStarPlanetGain()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        Debug.Log("autobuyer bought");
        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarPlanetGain);
        if (autobuyer.isActive == false)
        {
            autobuyManager.StartStarPlanetGainAutobuyer();
        }

        manager.SetStarGain();
        manager.UpdateText();

        gameObject.SetActive(false);
    }

    public void SpeedUpAutoBuyStarPlanetGain()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarPlanetGain);

        if (autobuyer.buyDelay > .1f)
        {
            autobuyer.buyDelay -= .1f;
            if (autobuyer.buyDelay <= .1f)
            {
                gameObject.SetActive(false);
            }
        }

        manager.SetStarGain();
        manager.UpdateText();
    }

    public void AmountUpAutoBuyStarPlanetGain()
    {
        if (manager.GetPlanets() < cost) return;
        manager.DecereasePlanets(cost);

        var autobuyer = autobuyManager.GetAutobuyer(AutobuyerType.StarPlanetGain);
        autobuyer.buyAmount++;

        manager.SetStarGain();
        manager.UpdateText();
    }

    private void UpdateText()
    {
        _upgradeTextString = "cost: " + manager.textFormatter.ReturnText(manager.data.currentNotation, cost) + "\n" + upgradeDecription;
        if (string.IsNullOrEmpty(_upgradeTextString) || upgradeText == null) return;
        upgradeText.text = $"{_upgradeTextString}";
    }
}
