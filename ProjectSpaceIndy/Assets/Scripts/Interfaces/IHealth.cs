using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth {
    /// <summary>
    /// Returns the current amount of health points
    /// </summary>
    int CurrentHealth { get; }
    
    /// <summary>
    /// Returns the maximum amount of health points
    /// </summary>
    int MaxHealth { get; }
    
    /// <summary>
    /// Return the minimum amount of health points
    /// </summary>
    int MinHealth { get; }
    
    /// <summary>
    /// Tells whether the unit currently cannot die. The unit can still take damage
    /// </summary>
    bool IsImmortal { get; set; }
    
    /// <summary>
    /// Tells whether the unit currently cannot take any damage
    /// </summary>
    bool IsInvulnerable { get; set; }
    
    /// <summary>
    /// Increases current health points by the given amount. Will not increase beyond maximum health
    /// </summary>
    /// <param name="amount">Amount of health points to add</param>
    void IncreaseHealth(int amount);
    
    /// <summary>
    /// Decreases current health points by the given amount. Will not decrease under minimum health
    /// </summary>
    /// <param name="amount">Amount of health points to subtract</param>
    /// <returns>True if the unit dies (current health reaches minimum health). Else false</returns>
    bool DecreaseHealth(int amount);
    
    /// <summary>
    /// Resets the health
    /// </summary>
    void Reset();
}
