using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePhaseEnable : MonoBehaviour
{
    private List<GameObject> _gameObjects = new List<GameObject>();
    private int _activeChildren;
    private int _totalChildren;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            _gameObjects.Add(child.gameObject);
            _totalChildren++;
        }
    }

    void Update()
    {
        if (GameManager.EscapePhase && _activeChildren != _totalChildren)
        {
            foreach (GameObject obj in _gameObjects)
            {
                obj.SetActive(true);
                _activeChildren++;
            }
        }
    }
}
