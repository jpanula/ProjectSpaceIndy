using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{
	[FormerlySerializedAs("FuelAmount")] public float _fuelAmount;
	public float FuelCap;
	public float ShootDelay;
	
	[Header("UI Stuff")]
	public TextMeshProUGUI FuelText;
	public Image HealthBar;
	public float HealthBarChangeSpeed = 10;
	public float FuelBarChangeSpeed = 10;
	public float ShipHUDTimeout = 3;
	public float ShipHUDFadeOutSpeed = 1;
	public float ShipHUDFadeInSpeed = 1;
	public Image[] ShipHUDElements = new Image[6];
	
	private Vector3 _spawnPosition;
	private PlayerMover _playerMover;
	private float _speed;
	private float _boostSpeed;
	private bool _speedValuesSaved;
	private float _shootDelayTimer;

	private Image _hpBarFill;
	private Image _fuelBarFill;
	private Image[] _healthHUD = new Image[3];
	private Image[] _fuelHUD = new Image[3];
	private float[] _shipHUDAlpha;
	private float[] _healthHUDAlpha;
	private float[] _fuelHUDAlpha;
	private float _healthHUDTimeoutTimer;
	private float _fuelHUDTimeoutTimer;
	private float _shipHUDTimeoutTimer;
	private int _lastHP;
	private float _lastFuel;

	public Vector3 SpawnPos
	{
		get { return _spawnPosition; }
		set { _spawnPosition = value; }
	}
	
	public float FuelAmount
	{
		get { return _fuelAmount; }
		set { _fuelAmount = Mathf.Min(FuelCap, value); }
	}
	
	private void Start()
	{
		_playerMover = GetComponent<PlayerMover>();

		//TODO make this part less shit maybe
		
		_shipHUDAlpha = new float[ShipHUDElements.Length];
		_healthHUDAlpha = new float[3];
		_fuelHUDAlpha = new float[3];
		for (int i = 0; i < ShipHUDElements.Length; i++)
		{
			_shipHUDAlpha[i] = ShipHUDElements[i].color.a;
			if (i < 3)
			{
				_healthHUDAlpha[i] = _shipHUDAlpha[i];
				_healthHUD[i] = ShipHUDElements[i];
			}
			else
			{
				_fuelHUDAlpha[i - 3] = _shipHUDAlpha[i];
				_fuelHUD[i - 3] = ShipHUDElements[i];
			}
			
			Image image = ShipHUDElements[i];
			
			if (image.name.Contains("HPBarFill"))
			{
				_hpBarFill = image;
			}
			else if (image.name.Contains("FuelBarFill"))
			{
				_fuelBarFill = image;
			}		
		}
		
		_lastHP = Health.CurrentHealth;
		_lastFuel = FuelAmount;
	}

	protected override void Update ()
	{
		float fillAmount = (float) Health.CurrentHealth / Health.MaxHealth;
		HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, fillAmount, TimerManager.Instance.UiDeltaTime * HealthBarChangeSpeed);
		_hpBarFill.fillAmount = HealthBar.fillAmount;
		fillAmount = FuelAmount / FuelCap;
		_fuelBarFill.fillAmount = Mathf.Lerp(_fuelBarFill.fillAmount, fillAmount, TimerManager.Instance.UiDeltaTime * FuelBarChangeSpeed);
		FuelText.text = "Fuel: " + FuelAmount.ToString("0.00");
		
		if (Input.GetButton("Fire3") || Input.GetAxis("Triggers") != 0)
		{
			if (!_speedValuesSaved)
			{
				_speed = _playerMover.Speed;
				_boostSpeed = _playerMover.BoostSpeed;
				_speedValuesSaved = true;
			}

			if (FuelAmount > 0)
			{
				_playerMover.Speed = _boostSpeed;
				if (_playerMover.MovementVector.magnitude > InputManager.Instance.LeftStickDeadzone)
				{
					FuelAmount -= TimerManager.Instance.GameDeltaTime;
					FuelAmount = Mathf.Max(0, FuelAmount);
				}
			}
			else
			{
				_playerMover.Speed = _speed;
				FuelAmount = 0;
			}

		}
		else
		{
			if (_speedValuesSaved)
			{
				_playerMover.Speed = _speed;
				_speedValuesSaved = false;
			}
		}

		if (_lastHP != Health.CurrentHealth)
		{
			_lastHP = Health.CurrentHealth;
			_healthHUDTimeoutTimer = 0;
		}

		if (Health.CurrentHealth < Health.MaxHealth)
		{
			_healthHUDTimeoutTimer = 0;
		}

		if (_lastFuel != FuelAmount)
		{
			_lastFuel = FuelAmount;
			_fuelHUDTimeoutTimer = 0;
		}
		
		_healthHUDTimeoutTimer += TimerManager.Instance.UiDeltaTime;
		_fuelHUDTimeoutTimer += TimerManager.Instance.UiDeltaTime;
		
		if (_healthHUDTimeoutTimer >= ShipHUDTimeout)
		{
			foreach (var image in _healthHUD)
			{
				if (image.color.a > 0)
				{
					var imageColor = image.color;
					imageColor.a = Mathf.Lerp(imageColor.a, 0, TimerManager.Instance.UiDeltaTime * ShipHUDFadeOutSpeed);
					image.color = imageColor;
				}
			}
		}
		else
		{
			for (int i = 0; i < _healthHUD.Length; i++)
			{
				var image = _healthHUD[i];
				if (image.color.a < _healthHUDAlpha[i])
				{
					var imageColor = image.color;
					imageColor.a = Mathf.Lerp(imageColor.a, _healthHUDAlpha[i], TimerManager.Instance.UiDeltaTime * ShipHUDFadeInSpeed);
					image.color = imageColor;
				}
			}
		}
		if (_fuelHUDTimeoutTimer >= ShipHUDTimeout)
		{
			foreach (var image in _fuelHUD)
			{
				if (image.color.a > 0)
				{
					var imageColor = image.color;
					imageColor.a = Mathf.Lerp(imageColor.a, 0, TimerManager.Instance.UiDeltaTime * ShipHUDFadeOutSpeed);
					image.color = imageColor;
				}
			}
		}
		else
		{
			for (int i = 0; i < _fuelHUD.Length; i++)
			{
				var image = _fuelHUD[i];
				if (image.color.a < _fuelHUDAlpha[i])
				{
					var imageColor = image.color;
					imageColor.a = Mathf.Lerp(imageColor.a, _fuelHUDAlpha[i], TimerManager.Instance.UiDeltaTime * ShipHUDFadeInSpeed);
					image.color = imageColor;
				}
			}
		}
	}

	private void LateUpdate()
	{
		float rightStickDeadzone = InputManager.Instance.RightStickDeadzone;
		bool useMouse = _playerMover.UseMouse;
		
		Vector3 rightStickVector = new Vector3(Input.GetAxisRaw("Horizontal_Look"), 0, Input.GetAxisRaw("Vertical_Look"));
		float rightStickMagnitude = Vector3.Magnitude(rightStickVector);
		if (Input.GetAxisRaw("Fire1") > 0 || rightStickMagnitude >= rightStickDeadzone && !useMouse)
		{
			_shootDelayTimer += TimerManager.Instance.GameDeltaTime;
			if (_shootDelayTimer >= ShootDelay)
			{
				foreach (Weapon weapon in Weapons)
				{
					weapon.Fire();
				}
			}
		}
		else
		{
			_shootDelayTimer = 0;
		}
	}

	private void OnEnable()
	{
		_spawnPosition = transform.position;
	}

	protected override void Die()
	{
		transform.position = _spawnPosition;

		ResetUnit();
	}
}
