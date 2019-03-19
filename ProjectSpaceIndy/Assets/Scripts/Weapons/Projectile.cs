using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region BasicStuff
    
    [Header("Stats")]
    
    [Tooltip("Damage the projectile deals.")]
    public int Damage = 1;
    
    [Tooltip("Speed in units/s")]
    public float Speed = 15;
    
    [Tooltip("Choose who the projectile should hurt.\n\nCustom uses the layer mask from advanced stuff.")]
    public HitLayer CollideWith;
    
    [Tooltip("Radius of the projectile hitbox in units.")]
    public float HitBoxRadius = 0.3f;
    
    [Tooltip("Lifetime of the projectile in seconds")]
    public float Lifetime = 1.5f;

    
    
    [Space(15), Header("Effects")]
    
    [Tooltip("The particle effect to play when the projectile is fired.")]
    public ParticleSystem FiringEffect;
    
    [Tooltip("The particle effect to play when the projectile hits something.")]
    public ParticleSystem CollisionEffect;
    
    [Tooltip("The particle effect to play continuously on the projectile.")]
    public ParticleSystem ConstantEffect;

    [Tooltip("Trail effect")]
    public TrailRenderer Trail;

    #endregion BasicStuff
    
    
    
    [Space(15), Header("Advanced Stuff")]
    
    [Tooltip("Layers that the projectile can collide with.")]
    public LayerMask LayerMask;
    
    
    
    public IMover Mover;
    
    [HideInInspector]
    public bool _isFired;
    private Weapon _weapon;
    private Vector3 _direction;
    private float _lifeTimeTimer;

    private bool _firingEffectIsNotNull;
    private bool _collisionEffectIsNotNull;
    private bool _constantEffectIsNotNull;
    private bool _trailIsNotNull;
    
    private GameObject _firingEffectObject;
    private GameObject _collisionEffectObject;
    private GameObject _constantEffectObject;
    private GameObject _trailObject;

    private readonly LayerMask _playerProjectileMask =
        (int) (Const.Layers.Enemy | Const.Layers.Environment | Const.Layers.Activator | Const.Layers.InvisibleWall);

    private readonly LayerMask _enemyProjectileMask = (int) (Const.Layers.Player | Const.Layers.Environment | Const.Layers.InvisibleWall);

    public enum HitLayer
    {
        Player,
        Enemy,
        Custom
    }
    
    private void Awake()
    {   
        _firingEffectIsNotNull = FiringEffect != null;
        _collisionEffectIsNotNull = CollisionEffect != null;
        _constantEffectIsNotNull = ConstantEffect != null;
        _trailIsNotNull = Trail != null;

        if (_firingEffectIsNotNull)
        {
            
        }
        
        Mover = GetComponent<IMover>();
        Mover.Speed = Speed;
        _lifeTimeTimer = 0;

        switch (CollideWith)
        {
            case HitLayer.Player:
                LayerMask = _enemyProjectileMask;
                gameObject.layer = (int) Const.LayerNumbers.EnemyProjectile;
                break;
            
            case HitLayer.Enemy:
                LayerMask = _playerProjectileMask;
                gameObject.layer = (int) Const.LayerNumbers.PlayerProjectile;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_isFired)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, HitBoxRadius, Mover.MovementVector, out hit,
                Vector3.Distance(transform.position, transform.position + Mover.MovementVector * Speed * Time.deltaTime), LayerMask))
            {
                Hit(hit.collider);
            }
        }
        else
        {
            Mover.MovementVector = Vector3.zero;
        }
        
        _lifeTimeTimer += Time.deltaTime;
        if (_lifeTimeTimer >= Lifetime)
        {
            ReturnProjectile();
        }
    }

    private void LateUpdate()
    {
        if (_constantEffectIsNotNull) _constantEffectObject.transform.position = transform.position;
        if (_trailIsNotNull) _trailObject.transform.position = transform.position;
    }

    public void Launch(Weapon weapon, Vector3 direction)
    {
        _weapon = weapon;
        _direction = direction;
        _isFired = true;
        Mover.MovementVector = _direction;
        _lifeTimeTimer = 0;
        if (_firingEffectIsNotNull)
        {
            _firingEffectObject = Instantiate(FiringEffect.gameObject, transform.position, transform.rotation);
            var mainModule = _firingEffectObject.GetComponent<ParticleSystem>().main;
            mainModule.stopAction = ParticleSystemStopAction.Destroy;
        }
        if (_constantEffectIsNotNull) _constantEffectObject = Instantiate(ConstantEffect.gameObject, transform.position, transform.rotation);
        if (_trailIsNotNull)
        {
            _trailObject = Instantiate(Trail.gameObject, transform.position, transform.rotation);
            _trailObject.GetComponent<TrailRenderer>().autodestruct = true;
        }

        Transform pt = weapon.transform.parent;
        Vector3 launchPos = transform.position - pt.position;
        RaycastHit hit;
        if (Physics.SphereCast(pt.position, HitBoxRadius, launchPos, out hit,
            Vector3.Distance(pt.position, transform.position), LayerMask))
        {
            Hit(hit.collider);
        }
    }

    public void ReturnProjectile()
    {
        Weapon owner = _weapon;
        ResetProjectile();
        owner.ReturnProjectile(this);
    }

    private void Hit(Collider hitCollider)
    {
        IDamageReceiver damageReceiver = hitCollider.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(Damage);
            ReturnProjectile();
        }
        else //if (1 << hitCollider.gameObject.layer == (int) Const.Layers.Environment || 1 << hitCollider.gameObject.layer == (int) Const.Layers.Activator || 1 << hitCollider.gameObject.layer == (int) Const.Layers.InvisibleWall)
        {
            ActivatorBase activator = hitCollider.GetComponent<ActivatorBase>();
            if (activator != null)
            {
                activator.Active = true;
            }
            ReturnProjectile();
        }
    }

    private void ResetProjectile()
    {
        _weapon = null;
        _isFired = false;
        _lifeTimeTimer = 0;
        Mover.MovementVector = Vector3.zero;
        
        if (_constantEffectIsNotNull)
        {
            var main = ConstantEffect.main;
            _constantEffectObject.GetComponent<ParticleSystem>().Stop();
            Destroy(_constantEffectObject, main.startDelay.constant + main.startLifetime.constant);
        }

        if (_collisionEffectIsNotNull)
        {
            _collisionEffectObject = Instantiate(CollisionEffect.gameObject, transform.position, Quaternion.Inverse(transform.rotation));
            var mainModule = _collisionEffectObject.GetComponent<ParticleSystem>().main;
            mainModule.stopAction = ParticleSystemStopAction.Destroy;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, HitBoxRadius);
    }
}
