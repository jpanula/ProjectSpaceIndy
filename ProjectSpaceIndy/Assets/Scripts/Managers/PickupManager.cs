using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance;

    public int TestLevelScrapPoolSize;
    public int TestLevelFuelPoolSize;
    public int TestLevelHealthPoolSize;
    public PickupPool ScrapPool;
    public PickupPool FuelPool;
    public PickupPool HealthPool;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        switch (GameManager.Instance.CurrentLevel)
        {
            case GameManager.Level.Test:
                ScrapPool.Size = TestLevelScrapPoolSize;
                FuelPool.Size = TestLevelFuelPoolSize;
                HealthPool.Size = TestLevelHealthPoolSize;
                break;
            
            default:
                ScrapPool.Size = 0;
                break;
        }
    }

    public PickupBase GetScrap()
    {
        return ScrapPool.GetPooledItem();
    }

    public bool ReturnScrap(ScrapPickup scrap)
    {
        return ScrapPool.ReturnPooledItem(scrap);
    }

    public PickupBase GetFuel()
    {
        return FuelPool.GetPooledItem();
    }

    public bool ReturnFuel(FuelPickup fuel)
    {
        return FuelPool.ReturnPooledItem(fuel);
    }

    public PickupBase GetHealth()
    {
        return HealthPool.GetPooledItem();
    }

    public bool ReturnHealth(HealthPickup health)
    {
        return HealthPool.ReturnPooledItem(health);
    }
}
