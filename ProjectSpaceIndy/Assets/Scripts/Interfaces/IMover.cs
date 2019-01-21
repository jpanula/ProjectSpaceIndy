using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover{
    /// <summary>
    /// Speed of movement
    /// </summary>
    float Speed { get; }
    
    /// <summary>
    /// Method for moving
    /// </summary>
    /// <param name="movementVector">The movement vector</param>
    void Move(Vector3 movementVector);
}
