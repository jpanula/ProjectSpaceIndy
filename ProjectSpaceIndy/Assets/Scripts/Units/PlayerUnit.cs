﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{
	[FormerlySerializedAs("FuelAmount")] public float _fuelAmount;
	public float FuelCap;
	public TextMeshProUGUI FuelText;

	public Image HealthBar;
	public float HealthBarChangeSpeed;
	private Vector3 _spawnPosition;
	private PlayerMover _playerMover;
	private float _speed;
	private float _boostSpeed;
	private bool _speedValuesSaved;

	public float FuelAmount
	{
		get { return _fuelAmount; }
		set { _fuelAmount = Mathf.Min(FuelCap, value); }
	}
	
	private void Start()
	{
		_playerMover = GetComponent<PlayerMover>();
	}

	protected override void Update ()
	{
		float fillAmount = (float) Health.CurrentHealth / Health.MaxHealth;
		HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, fillAmount, Time.deltaTime * HealthBarChangeSpeed);
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
				FuelAmount -= Time.deltaTime;
				FuelAmount = Mathf.Max(0, FuelAmount);
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
