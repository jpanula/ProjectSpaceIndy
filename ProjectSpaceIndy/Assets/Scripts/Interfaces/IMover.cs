﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover {
    /// <summary>
    /// Speed of movement
    /// </summary>
    float Speed { get; set; }

    Vector3 MovementVector { get; set; }

    void ResetMover();
}
