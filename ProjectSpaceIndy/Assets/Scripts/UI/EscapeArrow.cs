using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeArrow : MonoBehaviour
{
    public Path Path;
    public float Speed;
    private Node _currentNode;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        var cameraRect = new Rect(
            0, 0, Screen.width, Screen.height);
        
        var pos = transform.position;
        
        if (!_currentNode)
        {
            _currentNode = Path.GetClosestNode(pos);
        }

        if (pos == _currentNode.GetPosition())
        {
            _currentNode = Path.GetNextNode(_currentNode);
        }

        var newPos = Vector3.Lerp(pos, _currentNode.GetPosition(), TimerManager.Instance.GameDeltaTime * Speed / Vector3.Distance(pos, _currentNode.GetPosition()));
        var arrowScreenPos = _mainCamera.WorldToScreenPoint(newPos);
        if (!cameraRect.Contains(arrowScreenPos))
        {
            arrowScreenPos.x = Mathf.Clamp(arrowScreenPos.x, cameraRect.xMin, cameraRect.xMax);
            arrowScreenPos.y = Mathf.Clamp(arrowScreenPos.z, cameraRect.yMin, cameraRect.yMax);
            newPos.x = _mainCamera.ScreenToWorldPoint(arrowScreenPos).x;
            newPos.z = _mainCamera.ScreenToWorldPoint(arrowScreenPos).y;
        }
        transform.position = newPos;
        pos = newPos;
        var Ray = _mainCamera.ScreenPointToRay(_mainCamera.WorldToScreenPoint(pos));
        var newRot = Quaternion.LookRotation(Vector3.ProjectOnPlane(_currentNode.GetPosition() - pos, Ray.direction), -Ray.direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, TimerManager.Instance.GameDeltaTime * Speed);
    }
}
