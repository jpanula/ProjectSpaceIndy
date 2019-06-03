using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float MinTimeBetweenShakes;
    public float MaxTimeBetweenShakes;
    private float _randomBetweenMinMax;
    private float _countDown;

    public float ShakeIntensity;
    public float ShakeRoughness;
    public float ShakeFadeInTime;
    public float ShakeFadeOutTime;

    public GameObject RandomExplosion;

    private void Start()
    {
        _countDown = 0;
        _randomBetweenMinMax = 0;
    }

    void Update()
    {
        if (GameManager.EscapePhase)
        {
            if (_randomBetweenMinMax == 0)
            {
                _randomBetweenMinMax = Random.Range(MinTimeBetweenShakes, MaxTimeBetweenShakes);
            }
            _countDown += Time.deltaTime * TimerManager.Instance.GameDeltaScale;

            if (_countDown >= _randomBetweenMinMax)
            {
                _countDown = 0;
                _randomBetweenMinMax = 0;
                Instantiate(RandomExplosion);
                CameraShaker.Instance.ShakeOnce(ShakeIntensity, ShakeRoughness, ShakeFadeInTime, ShakeFadeOutTime);
            }
        }
    }
}
