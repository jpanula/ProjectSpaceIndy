using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public int Damage;
    public float KnockBackSpeed;
    public float KnockBackTime;
    public AudioSource HitSound;
    private float _volume;
    private bool _isAudioNull;

    private void Awake()
    {
        _isAudioNull = HitSound == null;
        
        if (!_isAudioNull)
        {
            _volume = HitSound.volume;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool dead = false;
        var damageReceiver = other.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            if (!_isAudioNull)
            {
                HitSound.volume = _volume * AudioManager.EffectsVolume;
                HitSound.Play();
            }
            dead = damageReceiver.TakeDamage(Damage);
        }

        var player = other.GetComponent<PlayerMover>();
        if (player != null && !dead)
        {
            var direction = other.transform.position - transform.position;
            player.KnockBack(direction, KnockBackSpeed, KnockBackTime);
        }
    }
}
