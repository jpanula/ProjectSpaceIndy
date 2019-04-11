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
                Instantiate(ImpactSound);
                ImpactSound.transform.position = transform.position;
                _timeOfLastImpact = TimerManager.Instance.ScaledGameTime;
            }
            if (destroyed)
            {
                if (AudioSourceToSpawn != null)
                {
                    Instantiate(AudioSourceToSpawn);
                    AudioSourceToSpawn.transform.position = transform.position;
                }
                Die();
            }
        }

        return destroyed;
    }

    protected virtual void Die()
    {
        if(AmountOfScrapToDrop > 0)   
        {
            List<PickupBase> scraps = new List<PickupBase>();
            for (int i = 0; i < AmountOfScrapToDrop; i++)
            {
                scraps.Add(PickupManager.Instance.GetScrap());
            }

            Vector3 dropAngle = Vector3.forward * DropDistance;
            for (int i = 0; i < scraps.Count; i++)
            {
                if (scraps[i] != null)
                {
                    scraps[i].transform.position = dropAngle + transform.position;
                    dropAngle = Quaternion.AngleAxis(360.0f / scraps.Count, Vector3.up) * dropAngle;
                }
            }
        }

        if (AmountOfFuelToDrop > 0)
        {
            List<PickupBase> fuel = new List<PickupBase>();
            for (int i = 0; i < AmountOfFuelToDrop; i++)
            {
                fuel.Add(PickupManager.Instance.GetFuel());
            }

            Vector3 dropAngle = Vector3.forward * DropDistance;
            for (int i = 0; i < fuel.Count; i++)
            {
                if (fuel[i] != null)
                {
                    fuel[i].transform.position = dropAngle + transform.position;
                    dropAngle = Quaternion.AngleAxis(360.0f / fuel.Count, Vector3.up) * dropAngle;
                }
            }
        }

        
        
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
}
