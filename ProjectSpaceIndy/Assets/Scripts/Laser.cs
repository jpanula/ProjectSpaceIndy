using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject LaserBeam;
    public int Damage;

    private void OnTriggerEnter(Collider other)
    {
        var damageReceiver = other.GetComponent<IDamageReceiver>();

        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(Damage);
        }
    }
}
