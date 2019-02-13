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
        Instance = this;
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
