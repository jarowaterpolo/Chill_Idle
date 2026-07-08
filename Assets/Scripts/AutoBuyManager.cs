using System.Collections;
using UnityEngine;

public class AutobuyManager : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private StarUpgrade starUpgrade;
    [SerializeField] private PlanetUpgrade planetUpgrade;

    void Start()
    {
        if (manager == null)
        {
            manager = FindAnyObjectByType<GameManager>();
        }
    }

    void Update()
    {
        
    }

    public void ActiveAutobuyerRestart()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainAddition);
        if (autobuyer.isActive == true)
        {
            StartCoroutine(StarGainAdditionAutobuyer());
        }
    }

    public void StartStargainAdditionAutobuyer()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainAddition);
        autobuyer.isActive = true;
        StartCoroutine(StarGainAdditionAutobuyer());
    }

    public IEnumerator StarGainAdditionAutobuyer()
    {
        var autobuyer = GetAutobuyer(AutobuyerType.StarGainAddition);
        while (autobuyer.isActive == true)
        {
            //Debug.Log($"stargain addittion autobuyer isactive == {manager.data.starGainAdditionAutobuyer.isActive} and needs to wait {manager.data.starGainAdditionAutobuyer.buyDelay} sec to buy {manager.data.starGainAdditionAutobuyer.buyAmount} upgrades");
            yield return new WaitForSeconds(autobuyer.buyDelay);
            for (int i = 0; i < autobuyer.buyAmount; i++)
            {
                starUpgrade.UpgradeStarGainAddition();
            }
        }
    }

    public Autobuyer GetAutobuyer(AutobuyerType type)
    {
        foreach (Autobuyer buyer in manager.data.autobuyers)
        {
            if (buyer.type == type)
                return buyer;
        }

        return null;
    }
}

[System.Serializable]
public class Autobuyer
{
    public AutobuyerType type;
    public bool isActive;
    public int buyAmount;
    public float buyDelay;
}
