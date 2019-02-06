using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private bool _active;


    public bool Active
    {
        get { return _active; }
        private set { _active = value;  }
    }
}
