using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour
{
    public int MinimumScrap;
    public int MaximumScrap;
    
    [Space(10)]
    public int MinimumFuel;
    public int MaximumFuel;
    
    [Space(10)]
    public int MinimumHealth;
    public int MaximumHealth;

    [Space(20)]
    [Range(0,100)]
    public float ScrapDropChance = 100;
    [Range(0,100)]
    public float FuelDropChance = 100;
    [Range(0,100)]
    public float HealthDropChance = 100;
    
    
    public float DropRadius;
    
    private PickupManager PickupManager;
    private bool _isQuitting;

    private void OnValidate()
    {
        MaximumScrap = Mathf.Max(MaximumScrap, MinimumScrap);
        MaximumFuel = Mathf.Max(MaximumFuel, MinimumFuel);
        MaximumHealth = Mathf.Max(MaximumHealth, MinimumHealth);
    }

    private void Start()
    {
        PickupManager = PickupManager.Instance;
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!_isQuitting)
        {
            int scrapAmount = 0;
            int fuelAmount = 0;
            int healthAmount = 0;
            
            if (ScrapDropChance / 100 >= Random.value && !(ScrapDropChance <= 0)) scrapAmount = Random.Range(MinimumScrap, MaximumScrap);
            if (FuelDropChance / 100 >= Random.value && !(FuelDropChance <= 0)) fuelAmount = Random.Range(MinimumFuel, MaximumFuel);
            if (HealthDropChance / 100 >= Random.value && !(HealthDropChance <= 0)) healthAmount = Random.Range(MinimumHealth, MaximumHealth);
            
            int dropTotal = scrapAmount + fuelAmount + healthAmount;
            var positions = GetRandomPositions(dropTotal);
            for (int i = 0; i < dropTotal; i++)
            {
                PickupBase drop;
                if (i < scrapAmount)
                {
                    drop = PickupManager.GetScrap();
                }
                else if (i < scrapAmount + fuelAmount)
                {
                    drop = PickupManager.GetFuel();
                }
                else
                {
                    drop = PickupManager.GetHealth();
                }

                Vector3 newPos = transform.position + positions[i];
                newPos.y = 0;
                drop.transform.position = newPos;
                drop.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            }
        }
    }

    public Vector3[] GetRandomPositions(int amount)
    {
        Vector3[] randomValues = new Vector3[amount];
        for (int i = 0; i < amount; i++)
        {
            do
            {
                randomValues[i] = Random.insideUnitSphere * DropRadius;
                randomValues[i].y = 0;
            } while (i > 0 && randomValues[i] == randomValues[i - 1]);
        }

        return randomValues;
    }
}
