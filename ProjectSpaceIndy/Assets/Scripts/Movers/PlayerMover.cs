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
	public float BoostSpeed = 2;
	[Tooltip("How fast should the player turn when using the right stick.")]
	public float LookTurnSpeed;
	[Tooltip("How much smoothing should be applied when turning with the right stick.")]
	public float LookTurnSmoothing;
	[Range(0.0f, 1.0f), Tooltip("Bigger number = Ship turns slower")]
	public float turnSmoothing;
	[Tooltip("Time in seconds to wait before changing rotation to match movement direction")]
	public float RotationTimeout;
	public bool UseMouse;
	private float rotationSpeed;
	private Plane _plane;
	private float _distanceToPlane;
	private Vector3 _pointOnPlane;
	public Vector3 _movementVector;
	private float _timeOutTimer;

	public bool Knockback;
	private float _knockbackTimer;
	private float _knockbackTimeout;
	private float _knockbackSpeed;
	private Vector3 _knockbackDirection;
	
	// Spherecast variables
	private float _sphereRadius;
	private float _maxDistance;
	public float AdditionalSphereRadius;
	
	[Tooltip("AudioSource for when the player is moving (without boost)")]
	public AudioSource NormalMovement;
	[Tooltip("How fast the movement sound fades away")]
	public float FadeTime;

	private float _volume;

	public Vector3 MovementVector
	{
		get { return _movementVector; }
		set { _movementVector = value; }
	}

	private void Awake()
	{
		_plane = new Plane(Vector3.up, 0);
		_timeOutTimer = 0;
		_sphereRadius = gameObject.GetComponent<SphereCollider>().radius + AdditionalSphereRadius;
		if (Camera == null)
		{
			Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}
	}

	private void Start()
	{
		_volume = NormalMovement.volume;
	}

	public float Speed
	{
		get { return _speed; }
		set { _speed = value; }
	}
	
	
	
	public void Move(Vector3 movementVector)
	{
		Vector3 newPosition = transform.position;
		
		rotationSpeed = 1 / turnSmoothing;
		newPosition = newPosition + movementVector * Speed;
		Vector3 lookAt = newPosition - transform.position;
		
		// Make a vector from the right stick input
		Vector3 rightStick = new Vector3(Input.GetAxisRaw("Horizontal_Look"), 0, Input.GetAxisRaw("Vertical_Look"));
		// Check if the input passes the deadzone threshold for the right stick
		if (Vector3.Magnitude(rightStick) >= InputManager.Instance.RightStickDeadzone)
		{
			UseMouse = false;
			rightStick = rightStick.normalized *
			             ((rightStick.magnitude - InputManager.Instance.RightStickDeadzone) / (1 - InputManager.Instance.RightStickDeadzone));
			if (rightStick.magnitude > 1)
			{
				rightStick = rightStick.normalized;
			}
			Vector3 lookVector = rightStick;
			Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
			//lookVector += transform.position;
			//transform.LookAt(lookVector);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, LookTurnSpeed * TimerManager.Instance.GameDeltaTime / LookTurnSmoothing);
			_timeOutTimer = 0;
			Debug.DrawLine(transform.position, transform.position + rightStick * 5, Color.magenta, 0.2f);
		}
		// If controller right stick fails, get from mouse if possible
		else if (UseMouse && TimerManager.Instance.GameDeltaScale > 0)
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
		else if (Vector3.Magnitude(_movementVector) >= InputManager.Instance.LeftStickDeadzone && _timeOutTimer >= RotationTimeout)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
				rotationSpeed * TimerManager.Instance.GameDeltaTime);
		}

		int layerMask = (int) (Const.Layers.Enemy | Const.Layers.Environment | Const.Layers.InvisibleWall);
		//layerMask = ~layerMask;

		Vector3 origin = transform.position;
		_maxDistance = Vector3.Distance(origin, newPosition);
		Vector3 direction = newPosition - transform.position;
		RaycastHit hit;

		Debug.DrawLine(origin, newPosition);
		while (Physics.SphereCast(origin, _sphereRadius, direction, out hit, _maxDistance, layerMask))
		{
			direction = Vector3.ProjectOnPlane(direction, hit.normal);
			newPosition = transform.position + direction;

			if (direction.magnitude < 0.00001)
			{
				newPosition = transform.position;
				break;
			}

		}

		if (transform.position != newPosition)
		{
			if (NormalMovement != null && !NormalMovement.isPlaying)
			{
				NormalMovement.Play();
			}
		}

		newPosition.y = 0;
		transform.position = newPosition;		
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			UseMouse = true;
		}
		 
		_timeOutTimer += TimerManager.Instance.GameDeltaTime;
		float horizontal = Input.GetAxisRaw( "Horizontal" );
		float vertical = Input.GetAxisRaw( "Vertical" );

		// Muodostetaan syötteestä vektori
		Vector3 inputVector = new Vector3( horizontal, 0, vertical );
		
		// Check if the input passes the deadzone threshold, and if it doesn't, make it zero
		if (Vector3.Magnitude(inputVector) < InputManager.Instance.LeftStickDeadzone)
		{
			inputVector = Vector3.zero;
			if (NormalMovement != null && NormalMovement.isPlaying)
			{
				NormalMovement.volume = AudioManager.EffectsVolume * _volume;
				AudioFadeOut(NormalMovement);
			}
		}
		else
		{
			inputVector = inputVector.normalized *
			              ((inputVector.magnitude - InputManager.Instance.LeftStickDeadzone) / (1 - InputManager.Instance.LeftStickDeadzone));
		}

		// Kertomalla inputVector TimerManager.Instance.GameDeltaTime:lla saamme fps:stä
		// riippumattoman liikevektorin
		if (Vector3.Magnitude(inputVector) > 1.0f)
		{
			inputVector = Vector3.Normalize(inputVector);
		}
		Debug.DrawLine(transform.position, transform.position + inputVector * 5, Color.green, 0.2f);
		_movementVector = inputVector;

		if (Knockback)
		{
			var pos = transform.position;
			var newPos = Vector3.Lerp(pos, pos + _knockbackDirection, TimerManager.Instance.GameDeltaTime * _knockbackSpeed);
			var direction = newPos - pos;
			var maxDistance = Vector3.Distance(pos, newPos);
			var layerMask = (int) (Const.Layers.Enemy | Const.Layers.Environment | Const.Layers.EnemyProjectile | Const.Layers.InvisibleWall);
			
			RaycastHit hit;
			while (Physics.SphereCast(pos, _sphereRadius, direction, out hit, maxDistance, layerMask))
			{
				direction = Vector3.ProjectOnPlane(direction, hit.normal);
				newPos = transform.position + direction;

				if (direction.magnitude < 0.00001)
				{
					newPos = transform.position;
					break;
				}
			}

			newPos.y = 0;
			transform.position = newPos;
			
			_knockbackTimer += TimerManager.Instance.GameDeltaTime;
			if (_knockbackTimer > _knockbackTimeout)
			{
				Knockback = false;
				_knockbackTimer = 0;
			}
		}
		else
		{
			// Kutsutaan Moverin Move metodia ja välitetään syötevektori 
			// parametrina.
			Move(_movementVector * TimerManager.Instance.GameDeltaTime);
		}
		
		//Debug.Log("X: " + Input.GetAxisRaw("Horizontal_Look") + " Y: " + Input.GetAxisRaw("Vertical_Look"));
		
		// Temporary toggle for mouse for testing
		if (Input.GetKeyDown(KeyCode.M))
		{
			UseMouse = !UseMouse;
		}
	}

	private void AudioFadeOut(AudioSource audioSource)
	{
		float startVolume = audioSource.volume;

			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
		
		
		audioSource.Stop();
		audioSource.volume = startVolume;
	}

	public void ResetMover()
	{
		_timeOutTimer = 0;
		_knockbackTimer = _knockbackTimeout;
		Knockback = false;
	}

	public void KnockBack(Vector3 direction, float speed, float time)
	{
		Knockback = true;
		_knockbackTimer = 0;
		
		_knockbackDirection = direction;
		_knockbackSpeed = speed;
		_knockbackTimeout = time;
	}
}
