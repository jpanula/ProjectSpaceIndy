using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacle : MonoBehaviour
{
    public ActivatorBase[] Activators;
    [Tooltip("Gameobjects that will deactivate when the lasers turn off")]
    public GameObject[] ObjectsToDeactivate;

    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = this.GetComponent<BoxCollider>();
    }

    void Update()
    {
        int counter = 0;

        for (int i = 0; i < Activators.Length; i++)
        {
            if (Activators[i].Active)
            {
                counter++;
            }
        }

        if (counter == Activators.Length)
        {
            DeactivateLasers();
        }
    }

    private void DeactivateLasers()
    {
        foreach (GameObject gameObject in ObjectsToDeactivate)
        {
            gameObject.SetActive(false);
        }
        Destroy(_boxCollider);
    }
}
