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
    private bool _splittersSpawned;

    private void OnEnable()
    {
        _splittersSpawned = false;
        ResultScreen.SceneChanging += OnSceneChanging;
        PauseMenu.OnBackToMainMenu += OnSceneChanging;
    }

    private void OnDisable()
    {
        ResultScreen.SceneChanging -= OnSceneChanging;
        PauseMenu.OnBackToMainMenu -= OnSceneChanging;
        if (!_isQuitting && GetComponent<BasicEnemy>().Spawner != null && !_splittersSpawned)
        {
            var t = transform;
            var rot = t.rotation;
            var pos = t.position;
            Instantiate(Splitter, pos + leftSplitterPosition, rot);
            Instantiate(Splitter, pos + rightSplitterPosition, rot);
            _splittersSpawned = true;
        }
    }

    private void OnSceneChanging()
    {
            _isQuitting = true;
    }
    
    private void OnDestroy()
    {
        if (!_isQuitting && GetComponent<BasicEnemy>().Spawner == null && !_splittersSpawned)
        {
            var t = transform;
            var rot = t.rotation;
            var pos = t.position;
            Instantiate(Splitter, pos + leftSplitterPosition, rot);
            Instantiate(Splitter, pos + rightSplitterPosition, rot);
            _splittersSpawned = true;
        }
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
