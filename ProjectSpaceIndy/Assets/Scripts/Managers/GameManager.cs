using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Level
    {
        Error = -1,
        None = 0,
        Test = 1
    }
    
    public static int Score;
    public Level CurrentLevel;
    
    #region Manager Prefabs

    public PickupManager PickupManager;
    
    #endregion Manager Prefabs

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (PickupManager.Instance == null)
        {
            PickupManager = Instantiate(PickupManager);
        }
    }
}
