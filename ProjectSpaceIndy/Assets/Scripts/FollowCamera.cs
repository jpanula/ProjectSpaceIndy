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
			_newPos = target.transform.position +
			          Vector3.Normalize(new Vector3(0, Mathf.Sin(Mathf.Deg2Rad * Angle),
				          -Mathf.Cos(Mathf.Deg2Rad * Angle))) * Distance;
			transform.position = Vector3.Lerp(transform.position, _newPos, 1 / MovementSmoothing * TimerManager.Instance.GameDeltaTime);
		}

	}
}
