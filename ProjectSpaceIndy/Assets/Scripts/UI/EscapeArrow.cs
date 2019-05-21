using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeArrow : MonoBehaviour
{
    public float Speed;
    public Path Path;
    public float NextNodeDistance;
    private RectTransform _rectTransform;
    private Camera _mainCam;
    private Node _currentNode;
    private Rect _viewportRect = new Rect(0, 0, 1, 1);

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (!_currentNode)
        {
            _currentNode = Path.GetClosestNode(_mainCam.ScreenToWorldPoint(_rectTransform.position));
        }

        Debug.Log("Arrow Position\nScreen: " + _rectTransform.position + " World: " + _mainCam.ScreenToWorldPoint(_rectTransform.position));
        Debug.Log("Node Position\nScreen: " + _mainCam.WorldToScreenPoint(_currentNode.GetPosition()) + " World: " + _currentNode.GetPosition());
        if (_viewportRect.Contains(_mainCam.WorldToViewportPoint(_currentNode.GetPosition())))
        {
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, _mainCam.WorldToScreenPoint(_currentNode.GetPosition()), TimerManager.Instance.UiDeltaTime * Speed / Vector3.Distance(_mainCam.ScreenToWorldPoint(_rectTransform.position), _currentNode.GetPosition()));
        }

        if (Vector3.Distance(_mainCam.ScreenToWorldPoint(_rectTransform.position), _currentNode.GetPosition()) <=
            NextNodeDistance)
        {
            _currentNode = Path.GetNextNode(_currentNode);
        }
    }
}
