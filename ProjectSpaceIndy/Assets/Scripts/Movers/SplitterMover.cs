using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterMover : BasicEnemyMover
{
    public float ProjectileDetectionRadius;
    public float DodgeSpeed;
    public float DistanceFactor;
    private Collider[] _projectileColliders;
    
    protected override void Update()
    {
        _projectileColliders = Physics.OverlapSphere(transform.position, ProjectileDetectionRadius, (int) Const.Layers.PlayerProjectile);
        if (_projectileColliders.Length > 0)
        {
            GameObject closestProjectile = _projectileColliders[0].gameObject;
            float distance = float.PositiveInfinity;
            foreach (var collider in _projectileColliders)
            {
                var colliderDistance = Vector3.Distance(collider.transform.position, transform.position);
                if (colliderDistance < distance)
                {
                    distance = colliderDistance;
                    closestProjectile = collider.gameObject;
                }
            }

            var projectileForward = closestProjectile.transform.forward;
            var forward = -transform.forward;
            Vector3.OrthoNormalize(ref projectileForward, ref forward);

            var position = transform.position;
            var newPos = Vector3.Lerp(position, position + forward, TimerManager.Instance.GameDeltaTime * (DodgeSpeed + DistanceFactor * ( 1 - (distance / ProjectileDetectionRadius))));
            var direction = newPos - position;
            
            RaycastHit hit;
            while (Physics.SphereCast(position, _collider.radius, direction, out hit,
                Vector3.Distance(position, newPos), _layerMask))
            {
                direction = Vector3.ProjectOnPlane(direction, hit.normal);
                newPos = position + direction;

                if (direction.magnitude < 0.00001)
                {
                    newPos = transform.position;
                    break;
                }
            }
        
            newPos = new Vector3(newPos.x, 0, newPos.z);
            transform.position = newPos;
        }
        else
        {

            base.Update();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ProjectileDetectionRadius);
    }
}
