using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver {
    /// <summary>
    /// Takes damage according to the amount
    /// </summary>
    /// <param name="amount">The amount of damage to take</param>
    /// <returns>True if the unit dies, false otherwise</returns>
    bool TakeDamage(int amount);
    
}
