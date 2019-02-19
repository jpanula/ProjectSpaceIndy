using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance;
    public PickupPool ScrapPool;
    
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
                ScrapPool.Size = 20;
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
}
