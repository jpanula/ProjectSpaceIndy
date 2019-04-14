using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{

    public SkinnedMeshRenderer Renderer;
    public GameObject AfterImagePrefab;
    public float Frequency;
    private float _timer;

    private void Update()
    {
        _timer += TimerManager.Instance.GameDeltaTime;
        if (_timer > Frequency)
        {
            var afterImage = Instantiate(AfterImagePrefab, transform.position, transform.rotation);
            Renderer.BakeMesh(afterImage.GetComponent<MeshFilter>().mesh);
            _timer = 0;
        }
    }
}
