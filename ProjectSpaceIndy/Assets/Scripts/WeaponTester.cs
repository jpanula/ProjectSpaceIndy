using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTester : MonoBehaviour
{

	public Weapon[] Weapons;
	
	
	void Update ()
	{
		PlayerMover player = GetComponentInParent<PlayerMover>();
		float rightStickDeadzone = player.RightStickDeadzone;
		bool useMouse = player.UseMouse;
		
		Vector3 rightStickVector = new Vector3(Input.GetAxisRaw("Horizontal_Look"), 0, Input.GetAxisRaw("Vertical_Look"));
		float rightStickMagnitude = Vector3.Magnitude(rightStickVector);
		if (Input.GetAxisRaw("Fire1") > 0 || (rightStickMagnitude >= rightStickDeadzone && !useMouse))
		{
			foreach (Weapon weapon in Weapons)
			{
				weapon.Fire();
			}
		}
	}
}
