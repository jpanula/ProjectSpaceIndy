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
	[Tooltip("Changes how much aim and movement cause the camera to adjust")]
	public float AdjustMultiplier;
	[Tooltip("How many seconds should the camera adjustment stay before resetting")]
	public float AdjustTimeout;
	//private Vector3 defaultCamPos;
	private Vector3 _newPos;

	// Use this for initialization
	void Start ()
	{
		//defaultCamPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*Vector3 newCamPos = target.transform.position + defaultCamPos;
		transform.position = newCamPos;*/
		transform.eulerAngles = new Vector3(Angle, 0, 0);
		if (target != null)
		{
			Vector3 adjustmentVector = Vector3.zero;
			
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
				adjustmentVector = rightStickVector;
			}
			else if (leftStickVector.magnitude >= InputManager.Instance.LeftStickDeadzone)
			{
				adjustmentVector = leftStickVector;
			}
			
			_newPos = target.transform.position + adjustmentVector * AdjustMultiplier + 
			          Vector3.Normalize(new Vector3(0, Mathf.Sin(Mathf.Deg2Rad * Angle),
				          -Mathf.Cos(Mathf.Deg2Rad * Angle))) * Distance;
			transform.position = Vector3.Lerp(transform.position, _newPos, 1 / MovementSmoothing * TimerManager.Instance.GameDeltaTime);
		}

	}
}
