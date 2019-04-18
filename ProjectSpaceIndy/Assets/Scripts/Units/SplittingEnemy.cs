using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplittingEnemy : MonoBehaviour
{
    public GameObject Splitter;
    public Vector3 leftSplitterPosition;
    public Vector3 rightSplitterPosition;
    
    private bool _isQuitting;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene current, Scene next)
    {
        _isQuitting = true;
    }
    
    private void OnDestroy()
    {
        if (!_isQuitting)
        {
            var t = transform;
            var rot = t.rotation;
            var pos = t.position;
            Instantiate(Splitter, pos + leftSplitterPosition, rot);
            Instantiate(Splitter, pos + rightSplitterPosition, rot);
        }
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
