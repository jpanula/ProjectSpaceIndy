using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

	public GameObject target;
	private Vector3 defaultCamPos;

	// Use this for initialization
	void Start ()
	{
		defaultCamPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 newCamPos = target.transform.position + defaultCamPos;
		transform.position = newCamPos;
	}
}
