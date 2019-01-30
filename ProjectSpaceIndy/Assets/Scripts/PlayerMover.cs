using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IMover
{

	[Tooltip("Camera from which to take rotation when using the mouse")]
	public Camera Camera;
	public float _speed = 1;
	[Range(0.1f, 1.0f), Tooltip("Bigger number = Ship turns slower")]
	public float turnSmoothing;
	private float rotationSpeed;
	private Plane _plane;
	private float _distanceToPlane;
	private Vector3 _pointOnPlane;
	public Vector3 _movementVector;

	public Vector3 MovementVector
	{
		get { return _movementVector; }
		set { _movementVector = value; }
	}

	private void Awake()
	{
		_plane = new Plane(Vector3.up, 0);
	}

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
		
		// Get rotation from mouse if possible
		Ray mouseRay = Camera.ScreenPointToRay(Input.mousePosition);
		if (_plane.Raycast(mouseRay, out _distanceToPlane))
		{
			//Debug.DrawLine(_pointOnPlane + new Vector3(0, -2, 0), _pointOnPlane + new Vector3(0, 2, 0), Color.green, 0.5f);
			_pointOnPlane = mouseRay.GetPoint(_distanceToPlane);
			transform.LookAt(new Vector3(_pointOnPlane.x, transform.position.y, _pointOnPlane.z));
		}
		// If mouse rotation fails, use movement
		else if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
				rotationSpeed * Time.deltaTime);
		}

		transform.position = position;

	}

	private void Update()
	{
		float horizontal = Input.GetAxisRaw( "Horizontal" );
		float vertical = Input.GetAxisRaw( "Vertical" );

		// Muodostetaan syötteestä vektori
		Vector3 inputVector = new Vector3( horizontal, 0, vertical );

		// Kertomalla inputVector Time.deltaTime:lla saamme fps:stä
		// riippumattoman liikevektorin
		_movementVector = inputVector;

		// Kutsutaan Moverin Move metodia ja välitetään syötevektori 
		// parametrina.
		Move( _movementVector * Time.deltaTime);

	}
}
