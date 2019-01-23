using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Camera Camera;
    private Plane _plane;
    private float _distanceToPlane;
    private Vector3 _pointOnPlane;

    private void Awake()
    {
        _plane = new Plane(Vector3.up, 0);
    }

    private void Update()
    {
        Ray mouseRay = Camera.ScreenPointToRay(Input.mousePosition);
        if (_plane.Raycast(mouseRay, out _distanceToPlane))
        {
            _pointOnPlane = mouseRay.GetPoint(_distanceToPlane);
            Debug.DrawLine(_pointOnPlane + new Vector3(0, -2, 0), _pointOnPlane + new Vector3(0, 2, 0), Color.green, 0.5f);
            transform.LookAt(new Vector3(_pointOnPlane.x, transform.position.y, _pointOnPlane.z));
        }
    }
}
