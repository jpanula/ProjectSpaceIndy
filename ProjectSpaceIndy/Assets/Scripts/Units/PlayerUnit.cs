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
	private float[] _shipHUDAlpha;
	private float _shipHUDTimeoutTimer;
	private int _lastHP;

	public float FuelAmount
	{
		get { return _fuelAmount; }
		set { _fuelAmount = Mathf.Min(FuelCap, value); }
	}
	
	private void Start()
	{
		_playerMover = GetComponent<PlayerMover>();
		foreach (var image in ShipHUDElements)
		{
			if (image.name.Contains("HPBarFill"))
			{
				_hpBarFill = image;
			}
			else if (image.name.Contains("FuelBarFill"))
			{
				_fuelBarFill = image;
			}
		}
		_shipHUDAlpha = new float[ShipHUDElements.Length];
		for (int i = 0; i < ShipHUDElements.Length; i++)
		{
			_shipHUDAlpha[i] = ShipHUDElements[i].color.a;
			
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
	}

	protected override void Update ()
	{
		float fillAmount = (float) Health.CurrentHealth / Health.MaxHealth;
		HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, fillAmount, Time.deltaTime * HealthBarChangeSpeed);
		_hpBarFill.fillAmount = HealthBar.fillAmount;
		fillAmount = FuelAmount / FuelCap;
		_fuelBarFill.fillAmount = Mathf.Lerp(_fuelBarFill.fillAmount, fillAmount, Time.deltaTime * FuelBarChangeSpeed);
		FuelText.text = "Fuel: " + FuelAmount.ToString("0.00");
		
		if (Input.GetButton("Fire3") || Input.GetAxis("Triggers") != 0)
		{
			_shipHUDTimeoutTimer = 0;
			if (!_speedValuesSaved)
			{
				_speed = _playerMover.Speed;
				_boostSpeed = _playerMover.BoostSpeed;
				_speedValuesSaved = true;
			}

			if (FuelAmount > 0)
			{
				_playerMover.Speed = _boostSpeed;
				if (_playerMover.MovementVector.magnitude > _playerMover.LeftStickDeadzone)
				{
					FuelAmount -= Time.deltaTime;
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
			_shipHUDTimeoutTimer = 0;
		}
		
		_shipHUDTimeoutTimer += Time.deltaTime;
		if (_shipHUDTimeoutTimer >= ShipHUDTimeout)
		{
			foreach (var image in ShipHUDElements)
			{
				if (image.color.a > 0)
				{
					var imageColor = image.color;
					imageColor.a = Mathf.Lerp(imageColor.a, 0, Time.deltaTime * ShipHUDFadeOutSpeed);
					image.color = imageColor;
				}
			}
		}
		else
		{
			for (int i = 0; i < ShipHUDElements.Length; i++)
			{
				var image = ShipHUDElements[i];
				if (image.color.a < _shipHUDAlpha[i])
				{
					var imageColor = image.color;
					imageColor.a = Mathf.Lerp(imageColor.a, _shipHUDAlpha[i], Time.deltaTime * ShipHUDFadeInSpeed);
					image.color = imageColor;
				}
			}
		}
	}

	private void LateUpdate()
	{
		float rightStickDeadzone = _playerMover.RightStickDeadzone;
		bool useMouse = _playerMover.UseMouse;
		
		Vector3 rightStickVector = new Vector3(Input.GetAxisRaw("Horizontal_Look"), 0, Input.GetAxisRaw("Vertical_Look"));
		float rightStickMagnitude = Vector3.Magnitude(rightStickVector);
		if (Input.GetAxisRaw("Fire1") > 0 || rightStickMagnitude >= rightStickDeadzone && !useMouse)
		{
			_shootDelayTimer += Time.deltaTime;
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
