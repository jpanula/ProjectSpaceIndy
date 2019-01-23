using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTester : MonoBehaviour
{

	public Weapon[] Weapons;
	
	void Update () {
		if (Input.GetAxisRaw("Fire1") > 0)
		{
			foreach (Weapon weapon in Weapons)
			{
				weapon.Fire();
			}
		}
	}
}
