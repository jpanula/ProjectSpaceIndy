using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const
{
    [Flags]
    public enum Layers
    {
        Everything = -1,
        Default = 1 << 0,
        TransparentFX = 1 << 1,
        IgnoreRaycast = 1 << 2,
        Water = 1 << 4,
        UI = 1 << 5,
        PostProcessing = 1 << 8,
        
        Player = 1 << 9,
        Enemy = 1 << 10,
        Environment = 1 << 11,
        PlayerProjectile = 1 << 12,
        EnemyProjectile = 1 << 13,
        Activator = 1 << 14,
        Pickup = 1 << 15
    }

    public enum LayerNumbers
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        PostProcessing = 8,
        
        Player = 9,
        Enemy = 10,
        Environment = 11,
        PlayerProjectile = 12,
        EnemyProjectile = 13,
        Activator = 14,
        Pickup = 15
        
    }
}
