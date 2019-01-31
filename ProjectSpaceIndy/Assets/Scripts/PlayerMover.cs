using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerMover : MonoBehaviour, IMover
{

	[Tooltip("Camera from which to take rotation when using the mouse")]
	public Camera Camera;
	public float _speed = 1;
	[Range(0.1f, 1.0f), Tooltip("Bigger number = Ship turns slower")]
	public float turnSmoothing;
	[Tooltip("Time in seconds to wait before changing rotation to match movement direction")]
	public float RotationTimeout;
	public bool UseMouse;
	[Range(0.0f, 1.0f),Tooltip("The deadzone for the left stick in the gamepad")]
	public float LeftStickDeadzone;
	[Range(0.0f, 1.0f),Tooltip("The deadzone for the right stick in the gamepad")]
	public float RightStickDeadzone;
	private float rotationSpeed;
	private Plane _plane;
	private float _distanceToPlane;
	private Vector3 _pointOnPlane;
	public Vector3 _movementVector;
	private float _timeOutTimer;
	
	// Spherecast variables
	private float _sphereRadius;
	private float _maxDistance;
	

	public Vector3 MovementVector
	{
		get { return _movementVector; }
		set { _movementVector = value; }
	}

	private void Awake()
	{
		_plane = new Plane(Vector3.up, 0);
		_timeOutTimer = 0;
		_sphereRadius = gameObject.GetComponent<SphereCollider>().radius;
	}

	public float Speed
	{
		get { return _speed; }
		set { _speed = value; }
	}
	
	
	
	public void Move(Vector3 movementVector)
	{
		Vector3 newPosition = transform.position;
		newPosition = newPosition + movementVector * _speed;
		Vector3 lookAt = newPosition - transform.position;
		rotationSpeed = 1 / turnSmoothing;
		// Make a vector from the right stick input
		Vector3 rightStick = new Vector3(Input.GetAxisRaw("Horizontal_Look"), 0, Input.GetAxisRaw("Vertical_Look"));
		// Check if the input passes the deadzone threshold for the right stick
		if (Vector3.Magnitude(rightStick) >= RightStickDeadzone)
		{
			Vector3 lookVector = rightStick;
			lookVector += transform.position;
			transform.LookAt(lookVector);
			_timeOutTimer = 0;
			Debug.DrawLine(transform.position, transform.position + rightStick * 5, Color.magenta, 0.2f);
		}
		// If controller right stick fails, get from mouse if possible
		else if (UseMouse)
		{
			
			Ray mouseRay = Camera.ScreenPointToRay(Input.mousePosition);
			if (_plane.Raycast(mouseRay, out _distanceToPlane))
			{
				//Debug.DrawLine(_pointOnPlane + new Vector3(0, -2, 0), _pointOnPlane + new Vector3(0, 2, 0), Color.green, 0.5f);
				_pointOnPlane = mouseRay.GetPoint(_distanceToPlane);
				transform.LookAt(new Vector3(_pointOnPlane.x, transform.position.y, _pointOnPlane.z));
			}
		}
		// If mouse and right stick rotation fails, use movement
		else if (Vector3.Magnitude(_movementVector) >= LeftStickDeadzone && _timeOutTimer >= RotationTimeout)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
				rotationSpeed * Time.deltaTime);
		}

		int layerMask = (1 << 10) | (1 << 11) | (1 << 13);
		//layerMask = ~layerMask;

		Vector3 origin = transform.position;
		_maxDistance = Vector3.Distance(origin, newPosition);
		Vector3 direction = newPosition - transform.position;
		RaycastHit hit;

		Debug.DrawLine(origin, newPosition);
		if (Physics.SphereCast(origin, _sphereRadius, direction, out hit, _maxDistance, layerMask))
		{
			Debug.Log("Hit!");
			//transform.position = hit.point;
		}
		
		else { transform.position = newPosition; }
		
	}

	private void Update()
	{
		_timeOutTimer += Time.deltaTime;
		float horizontal = Input.GetAxisRaw( "Horizontal" );
		float vertical = Input.GetAxisRaw( "Vertical" );

		// Muodostetaan syötteestä vektori
		Vector3 inputVector = new Vector3( horizontal, 0, vertical );
		
		// Check if the input passes the deadzone threshold, and if it doesn't, make it zero
		if (Vector3.Magnitude(inputVector) < LeftStickDeadzone)
		{
			inputVector = Vector3.zero;
		}
		Debug.DrawLine(transform.position, transform.position + inputVector * 5, Color.green, 0.2f);

		// Kertomalla inputVector Time.deltaTime:lla saamme fps:stä
		// riippumattoman liikevektorin
		_movementVector = inputVector;

		// Kutsutaan Moverin Move metodia ja välitetään syötevektori 
		// parametrina.
		Move( _movementVector * Time.deltaTime);
		
		//Debug.Log("X: " + Input.GetAxisRaw("Horizontal_Look") + " Y: " + Input.GetAxisRaw("Vertical_Look"));
		
		// Temporary toggle for mouse for testing
		if (Input.GetKeyDown(KeyCode.M))
		{
			UseMouse = !UseMouse;
		}
	}
}
