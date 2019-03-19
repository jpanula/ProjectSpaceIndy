using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDestroyer : MonoBehaviour
{
    public DestructionMethods DestructionMethod;
    public int[] Tresholds;
    public GameObject[] GameObjects;
    public ParticleSystem DestroyEffect;
    private Dictionary<int, GameObject> _values;
    private Health _health;

    public enum DestructionMethods
    {
        Disable,
        Destroy
    }

    private void Awake()
    {
        if (Tresholds.Length < GameObjects.Length)
        {
            GameObject[] newObjects = new GameObject[Tresholds.Length];
            for (int i = 0; i < Tresholds.Length; i++)
            {
                newObjects[i] = GameObjects[i];
            }

            GameObjects = newObjects;
        }
        else if (GameObjects.Length < Tresholds.Length)
        {
            int[] newTresholds = new int[GameObjects.Length];
            for (int i = 0; i < GameObjects.Length; i++)
            {
                newTresholds[i] = Tresholds[i];
            }

            Tresholds = newTresholds;
        }
        
        _values = new Dictionary<int, GameObject>();
        for (int i = 0; i < Tresholds.Length; i++)
        {
            _values.Add(Tresholds[i], GameObjects[i]);
        }
    }

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        foreach (var pair in _values)
        {
            pair.Value.SetActive(true);
        }
    }

    private void Update()
    {
        foreach (var pair in _values)
        {
            if (_health.CurrentHealth < pair.Key && pair.Value != null && pair.Value.activeSelf)
            {
                var t = pair.Value.transform;
                Vector3 position = t.position;
                Quaternion rotation = t.rotation;
                
                GameObject destroyEffectObject = Instantiate(DestroyEffect.gameObject, position, rotation);
                var main = destroyEffectObject.GetComponent<ParticleSystem>().main;
                main.stopAction = ParticleSystemStopAction.Destroy;
                
                switch (DestructionMethod)
                {
                    case DestructionMethods.Destroy:
                        Destroy(pair.Value);
                        break;
                    case DestructionMethods.Disable:
                        pair.Value.SetActive(false);
                        break;
                }
            }
        }
    }
}
