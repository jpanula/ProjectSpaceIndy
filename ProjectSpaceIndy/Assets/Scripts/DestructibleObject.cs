using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageReceiver
{
    private Health _health;
    public int AmountOfScrapToDrop;
    public int AmountOfFuelToDrop;
    public float DropDistance;
    [Tooltip("Max distance from player for object to be destroyed")]
    public float MaxDistance = 22f;

    public GameObject AudioSourceToSpawn;
    public GameObject ImpactSound;
    public float ImpactSoundDelay;
    private float _timeOfLastImpact;
    private bool _dead;

    private void Awake()
    {
        _health = GetComponent<Health>();
        if (MaxDistance < 1)
        {
            MaxDistance = 22;
        }
    }

    public virtual bool TakeDamage(int amount)
    {
        bool destroyed = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, transform.position) <= MaxDistance)
        {
            
            destroyed = _health.DecreaseHealth(amount);
            if (ImpactSound != null && !destroyed && (TimerManager.Instance.ScaledGameTime - _timeOfLastImpact >= ImpactSoundDelay))
            {
                Instantiate(ImpactSound, transform.position, transform.rotation);
                _timeOfLastImpact = TimerManager.Instance.ScaledGameTime;
            }
            if (destroyed)
            {
                if (AudioSourceToSpawn != null)
                {
                    Instantiate(AudioSourceToSpawn, transform.position, transform.rotation);
                }
                Die();
            }
        }

        return destroyed;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
}
