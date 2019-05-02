using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

	public GameObject target;
	[Tooltip("Distance of camera from the object")]
	public float Distance;
	[Range(0.0f, 90.0f), Tooltip("Angle at which the camera looks at the object")]
	public float Angle;
	public float MovementSmoothing;
	public float AdjustSmoothing;
	[Tooltip("Changes how much aim and movement cause the camera to adjust")]
	public float AdjustMultiplier;
	[Tooltip("Lower values make the camera more springy when adjusting")]
	public float Stiffness;
	[Tooltip("How long should the player move before the camera starts adjusting to movement")]
	public float MovementAdjustRelax;
	[Tooltip("Holds a slowly falling deadzone for right stick values to prevent camera jumping back when moving stick back to neutral")]
	public float HoldFor;

	public AudioListener AudioListener;
	//private Vector3 defaultCamPos;
	private Vector3 _newPos;
	private float _adjustTimer;
	private bool _adjusting;
	private Vector3 _adjustmentVector;
	private float _smoothing;
	private float _adjustMultiplier;
	private float _holdValue;
	private float _movementRelaxTimer;
	private float _returnTimer;
	private bool _returning;
	private PlayerUnit _player;
	private Vector3 _startPosition;
	private bool _returned;

	// Use this for initialization
	void Start ()
	{
		//defaultCamPos = transform.position;
	}

	private void Awake()
	{
		//_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
		if (target == null)
		{
			target = GameObject.FindWithTag("Player");
		}
		_adjustMultiplier = AdjustMultiplier;
	}

	// Update is called once per frame
	void Update ()
	{
		AudioListener.volume = AudioManager.MasterVolume;
		
		/*Vector3 newCamPos = target.transform.position + defaultCamPos;
		transform.position = newCamPos;*/
		_holdValue -= TimerManager.Instance.GameDeltaTime / HoldFor;
		_holdValue = Mathf.Max(0, _holdValue);
		transform.eulerAngles = new Vector3(Angle, 0, 0);
			
		Vector3 rightStickVector = new Vector3(Input.GetAxisRaw("Horizontal_Look"), 0, Input.GetAxisRaw("Vertical_Look"));
		if (rightStickVector.magnitude > 1)
		{
			rightStickVector = rightStickVector.normalized;
		}
		
		Vector3 leftStickVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		if (leftStickVector.magnitude > 1)
		{
			leftStickVector = leftStickVector.normalized;
		}

		if (rightStickVector.magnitude >= InputManager.Instance.RightStickDeadzone)
		{
			if (rightStickVector.magnitude > _holdValue)
			{
				_holdValue = rightStickVector.magnitude;
			}
			_adjustmentVector = rightStickVector.normalized * _holdValue;
			_movementRelaxTimer = 0;
			_smoothing = AdjustSmoothing;
			_adjusting = true;
		}
		else if (leftStickVector.magnitude >= InputManager.Instance.LeftStickDeadzone)
		{
			_movementRelaxTimer += TimerManager.Instance.GameDeltaTime;
			if (_movementRelaxTimer >= MovementAdjustRelax)
			{
				_adjustmentVector = Vector3.zero;
				_adjusting = false;

				/*
				if (leftStickVector.magnitude > _stiffValue)
				{
					_stiffValue = leftStickVector.magnitude;
				}

				_adjustmentVector = leftStickVector.normalized * _stiffValue;
				_adjusting = true;
				if (Input.GetAxisRaw("Triggers") != 0 && _player.FuelAmount > 0)
				{
					_adjustMultiplier = 2 * AdjustMultiplier;
				}
				else
				{
					_adjustMultiplier = AdjustMultiplier;
				}*/
			}
		}

		if (_adjusting)
		{
			if (_smoothing < AdjustSmoothing)
			{
				_smoothing += TimerManager.Instance.GameDeltaTime / Stiffness;
				_smoothing = Mathf.Min(_smoothing, AdjustSmoothing);
			}
			else if (_smoothing > AdjustSmoothing)
			{
				_smoothing -= TimerManager.Instance.GameDeltaTime / Stiffness;
				_smoothing = Mathf.Max(_smoothing, AdjustSmoothing);
			}
		}
		else
		{
			if (_smoothing > MovementSmoothing)
			{
				_smoothing -= TimerManager.Instance.GameDeltaTime / Stiffness;
				_smoothing = Mathf.Max(_smoothing, MovementSmoothing);
			}
			else if (_smoothing < MovementSmoothing)
			{
				_smoothing += TimerManager.Instance.GameDeltaTime / Stiffness;
				_smoothing = Mathf.Min(_smoothing, MovementSmoothing);
			}
		}

			_newPos = target.transform.position + _adjustmentVector * _adjustMultiplier +
			          Vector3.Normalize(new Vector3(0, Mathf.Sin(Mathf.Deg2Rad * Angle),
				          -Mathf.Cos(Mathf.Deg2Rad * Angle))) * Distance;

			transform.position = Vector3.Lerp(transform.position, _newPos,
				1 / _smoothing * TimerManager.Instance.GameDeltaTime);
	}
}
