using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public ActivatorBase Activator;
    public ActivatorBase ResetDetector;
    private GameObject _player;
    private bool _reloaded;
    public static ReloadScene Instance;

    private void Awake()
    {
        _reloaded = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (ResetDetector.Active && _reloaded)
        {
            _reloaded = false;
        }
        
        if (Activator.Active && !_reloaded)
        {
            Reload();
        }
    }

    private void Reload()
    {
        GameObject oldPlayer = _player;
        _reloaded = true;
        SceneManager.LoadScene("TestLevel");
        GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");
        newPlayer = oldPlayer;
    }
}
