using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{

	private Vector3 _spawnPosition;
	
	protected override void Update ()
	{
	}

	private void OnEnable()
	{
		_spawnPosition = transform.position;
	}

	protected override void Die()
	{
		transform.position = _spawnPosition;
	}
}
