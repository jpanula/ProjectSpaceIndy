using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public int Damage;
    public float KnockBackSpeed;
    public float KnockBackTime;
    public AudioSource HitSound;

    private void OnTriggerEnter(Collider other)
    {
        
        
        bool dead = false;
        var damageReceiver = other.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            if (HitSound != null)
            {
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
