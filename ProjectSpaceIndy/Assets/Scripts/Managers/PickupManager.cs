using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance;

    public int TestLevelScrapPoolSize;
    public int TestLevelFuelPoolSize;
    public PickupPool ScrapPool;
    public PickupPool FuelPool;
    
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
}
