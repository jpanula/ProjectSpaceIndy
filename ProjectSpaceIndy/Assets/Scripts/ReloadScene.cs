using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public ActivatorBase Activator;
    public ActivatorBase ResetDetector;
    public PlayerUnit Player;
    private bool _reloaded;
    public static ReloadScene Instance;

    private int _health;
    private float _fuel;
    private Vector3 _position;
    private Quaternion _rotation;

    private Vector3 _cameraPosition;

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

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
        
            Player.GetComponent<Health>().CurrentHealth = _health;
            Player.transform.position = _position;
            Player.transform.rotation = _rotation;
            Player.FuelAmount = _fuel;
            Player.GetComponent<PlayerMover>().Camera.transform.position = _cameraPosition;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    private void Reload()
    {

        _health = Player.GetComponent<Health>().CurrentHealth;
        _position = Player.transform.position;
        _rotation = Player.transform.rotation;
        _fuel = Player.FuelAmount;
        _cameraPosition = Player.GetComponent<PlayerMover>().Camera.transform.position;
        
        SceneManager.LoadScene("TestLevel");
        _reloaded = true;

    }
}
