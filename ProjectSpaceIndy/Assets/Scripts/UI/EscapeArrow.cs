using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeArrow : MonoBehaviour
{
    public float Speed = 2000;
    public Path Path;
    public float NextNodeDistance = 0.02f;
    public float Margin = 0.02f;
    public float RotationSpeed = 2;
    private RectTransform _rectTransform;
    private Camera _mainCam;
    private Node _currentNode;
    private Rect _viewportRect = new Rect(0, 0, 1, 1);
    private Rect _screenRect;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        _screenRect = new Rect(0 + Margin * Screen.width, 0 + Margin * Screen.height, Screen.width - Margin * 2 * Screen.width, Screen.height - Margin * 2 * Screen.height);
        
        if (!_currentNode)
        {
            _currentNode = Path.GetClosestNode(_mainCam.ScreenToWorldPoint(_rectTransform.position));
        }

        var nodeViewportPosition = _mainCam.WorldToViewportPoint(_currentNode.GetPosition());
        nodeViewportPosition.z = 0;

        var nodeScreenPosition = _mainCam.WorldToScreenPoint(_currentNode.GetPosition());
        nodeScreenPosition.z = 0;

        var arrowViewportPosition = _mainCam.ScreenToViewportPoint(_rectTransform.position);
        
        Debug.Log("Arrow Position\nScreen: " + _rectTransform.position + " World: " + _mainCam.ScreenToWorldPoint(_rectTransform.position));
        Debug.Log("Node Position\nScreen: " + nodeScreenPosition + " World: " + _currentNode.GetPosition());
        if (_viewportRect.Contains(_mainCam.WorldToViewportPoint(_currentNode.GetPosition())))
        {
            _currentNode = Path.GetNextNode(_currentNode);
        }

        var direction = _mainCam.WorldToViewportPoint(_currentNode.GetPosition()) - new Vector3(0.5f, 0.5f, 0);
        direction.z = 0;
        var newRot = Quaternion.Euler(new Vector3(_rectTransform.eulerAngles.x, _rectTransform.eulerAngles.y, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) + 90));
        _rectTransform.rotation = Quaternion.Slerp(_rectTransform.rotation, newRot, TimerManager.Instance.UiDeltaTime * RotationSpeed);
        if (direction.magnitude > 0.5 - Margin)
        {
            direction = direction.normalized / 2 * (1 - Margin);
        }
        direction = direction  + new Vector3(0.5f, 0.5f, 0);
        var newPos = _mainCam.ViewportToScreenPoint(direction);
        _rectTransform.position = Vector3.Lerp(_rectTransform.position, newPos, TimerManager.Instance.UiDeltaTime * Speed / Vector3.Distance(_rectTransform.position, newPos));

    }
}
