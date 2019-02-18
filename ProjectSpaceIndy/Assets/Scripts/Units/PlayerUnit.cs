using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{

	public Image HealthBar;
	public float HealthBarChangeSpeed;
	private Vector3 _spawnPosition;
	
	protected override void Update ()
	{
		float fillAmount = (float) Health.CurrentHealth / Health.MaxHealth;
		HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, fillAmount, Time.deltaTime * HealthBarChangeSpeed);
	}

	private void OnEnable()
	{
		_spawnPosition = transform.position;
	}

	protected override void Die()
	{
		transform.position = _spawnPosition;
		Reset();
	}
}
