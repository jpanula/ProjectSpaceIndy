using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastTest : MonoBehaviour
{

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDitstance;
    

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        
        direction = transform.forward;
        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask,
            QueryTriggerInteraction.UseGlobal))
        {
            Debug.Log("Hit!");
            currentHitDitstance = hit.distance;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Debug.DrawLine(origin, origin + direction * currentHitDitstance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDitstance, sphereRadius);
    }
}
