using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int _combo;
    [SerializeField] private float _score;
    [SerializeField] private float _comboMultiplier;
    [SerializeField] private float _multiplierPerCombo;
    [SerializeField] private float _comboMaxMultiplier;
    [SerializeField] private float _comboTimeLeft;
    [SerializeField] private float _comboBaseTime;
    [SerializeField] private float _comboMinimumTime;
    [SerializeField] private float _comboTimeReduction;
    
    public static int Combo
    {
        get { return Instance._combo; }
        set 
        {
            Instance._combo = value;
            if (Combo > 0)
            {
                ResetComboTime();
            }
            else if (Combo == 0)
            {
                ComboTimeLeft = 0;
            }
            ComboMultiplier = Mathf.Clamp(1 + (Combo - 1) * MultiplierPerCombo, 1, ComboMaxMultiplier);
              
        }
    }

    public static float Score
    {
        get { return (int) Instance._score; }
        
        set 
        {
            if (value > Instance._score && Combo > 0)
            {
                ResetComboTime();
            } 
            Instance._score += (value - Instance._score) * ComboMultiplier; 
        }
    }

    public static float ComboMultiplier
    {
        get { return Instance._comboMultiplier; }
        set { Instance._comboMultiplier = value; }
    }

    public static float MultiplierPerCombo
    {
        get { return Instance._multiplierPerCombo; }
        set { Instance._multiplierPerCombo = value; }
    }

    public static float ComboMaxMultiplier
    {
        get { return Instance._comboMaxMultiplier; }
        set { Instance._comboMaxMultiplier = value; }
    }
    
    public static float ComboTimeLeft
    {
        get { return Instance._comboTimeLeft; }
        set { Instance._comboTimeLeft = value; }
    }

    public static float ComboBaseTime
    {
        get { return Instance._comboBaseTime; }
        set { Instance._comboBaseTime = value; }
    }

    public static float ComboMinimumTime
    {
        get { return Instance._comboMinimumTime; }
        set { Instance._comboMinimumTime = value; }
    }

    public static float ComboTimeReduction
    {
        get { return Instance._comboTimeReduction; }
        set { Instance._comboTimeReduction = value; }
    }
    
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
    }

    private void Update()
    {
        if (ComboTimeLeft <= 0)
        {
            Combo = 0;
        }
        
        ComboTimeLeft -= TimerManager.Instance.GameDeltaTime;

    }

    private static void ResetComboTime()
    {
        ComboTimeLeft = Mathf.Clamp(ComboBaseTime - Combo * ComboTimeReduction, ComboMinimumTime, ComboBaseTime);
    }
}
