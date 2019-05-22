using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Level
    {
        Error = -1,
        MainMenu = 0,
        Test = 1,
        Level1 = 2
    }

    public static float Score
    {
        get { return ScoreManager.Score; }
        set { ScoreManager.Score = value; }
    }
    public Level CurrentLevel;

    private static bool _escapePhase;
    
    #region Manager Prefabs

    public PickupManager PickupManager;
    public TimerManager TimerManager;
    public MenuManager MenuManager;
    public InputManager InputManager;
    public AudioManager AudioManager;
    public ScoreManager ScoreManager;
    
    #endregion Manager Prefabs

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (PickupManager.Instance == null)
        {
            PickupManager = Instantiate(PickupManager);
        }
        else
        {
            PickupManager = PickupManager.Instance;
        }

        if (TimerManager.Instance == null)
        {
            TimerManager = Instantiate(TimerManager);
        }
        else
        {
            TimerManager = TimerManager.Instance;
        }

        if (MenuManager.Instance == null)
        {
            MenuManager = Instantiate(MenuManager);
        }
        else
        {
            MenuManager = MenuManager.Instance;
        }

        if (InputManager.Instance == null)
        {
            InputManager = Instantiate(InputManager);
        }
        else
        {
            InputManager = InputManager.Instance;
        }

        if (AudioManager.Instance == null)
        {
            AudioManager = Instantiate(AudioManager);
        }
        else
        {
            AudioManager = AudioManager.Instance;
        }
        if (ScoreManager.Instance == null)
        {
            ScoreManager = Instantiate(ScoreManager);
        }
        else
        {
            ScoreManager = ScoreManager.Instance;
        }
    }

    private void Update()
    {
        if (!PickupManager)
        {
            if (!PickupManager.Instance)
            {
                PickupManager = Instantiate(PickupManager);
            }
            else
            {
                PickupManager = PickupManager.Instance;
            }
        }
    }

    private void Start()
    {
        if (CurrentLevel == Level.MainMenu)
        {
            MenuManager.Instance.ShowMainMenu(true);
        }
    }

    public static bool EscapePhase
    {
        get { return _escapePhase; }
        set { _escapePhase = value; }
    }
}
