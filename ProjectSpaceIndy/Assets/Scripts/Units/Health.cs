using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public int _startingHealth;
    public int _currentHealth;
    public int _maxHealth;
    public int _minHealth;
    public bool _isImmortal;
    public bool _isInvulnerable;
    
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, MinHealth, MaxHealth); }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public int MinHealth
    {
        get { return _minHealth; }
    }

    public bool IsImmortal { get; set; }
    public bool IsInvulnerable { get; set; }

    private void Awake()
    {
        _currentHealth = _startingHealth;
    }

    public void IncreaseHealth(int amount)
    {
        CurrentHealth += amount;
    }

    public bool DecreaseHealth(int amount)
    {
        if (!IsInvulnerable)
        {
            CurrentHealth -= amount;
        }

        if (IsImmortal && CurrentHealth == MinHealth)
        {
            CurrentHealth += 1;
        }

        return CurrentHealth <= MinHealth;
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
