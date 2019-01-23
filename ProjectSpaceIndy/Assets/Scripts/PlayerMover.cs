using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IMover
{

	public float _speed = 1;
	[Range(0.1f, 1.0f), Tooltip("Bigger number = Ship turns slower")]
	public float turnSmoothing;
	private float rotationSpeed;
	

	public float Speed
	{
		get { return _speed; }
		set { _speed = value; }
	}
	
	
	
	public void Move(Vector3 movementVector)
	{
		Vector3 position = transform.position;
		position = position + movementVector * _speed;
		Vector3 lookAt = position - transform.position;
		rotationSpeed = 1 / turnSmoothing;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), rotationSpeed * Time.deltaTime);
		transform.position = position;

		
	}

	
}
