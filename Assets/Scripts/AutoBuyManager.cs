using System.Collections;
using UnityEngine;

public class AutoBuyManager : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    private StarUpgrade starUpgrade = new();

    void Start()
    {
        if (manager == null)
        {
            manager = FindAnyObjectByType<GameManager>();
        }

        if (manager.data.starGainAdditionAutobuyer.isActive == true)
        {
            StartCoroutine(StarGainAdditionAutobuyer());
        }
    }

    void Update()
    {
        
    }

    public IEnumerator StarGainAdditionAutobuyer()
    {
        while (manager.data.starGainAdditionAutobuyer.isActive == true)
        {
            yield return new WaitForSeconds(manager.data.starGainAdditionAutobuyer.buyDelay);
            for (int i = 0; i < manager.data.starGainAdditionAutobuyer.buyAmount; i++)
            {
                starUpgrade.UpgradeStarGainAddition();
            }
        }
    }
}

public class AutoBuyer
{
    public bool isActive;
    public int buyAmount;
    public float buyDelay;
}
