using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public ActivatorBase Activator;
    public ActivatorBase ResetDetector;
    public GameObject PlayerPrefab;
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            if (i == 0)
            {
                _player = players[0];
            }
            else { Destroy(players[i]);}
        }

        if (_player == null)
        {
            _player = Instantiate(PlayerPrefab);
        }
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
         
        _reloaded = true;
        SceneManager.LoadScene("TestLevel");
        
        
    }
}
