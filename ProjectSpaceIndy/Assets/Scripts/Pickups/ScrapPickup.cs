using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrapPickup : PickupBase
{
    public int Score;
    private MeshFilter _meshFilter;
    public Mesh[] Models;
    private Mesh _randomModel;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        if(Models.Length > 0)
        {
            int random = Random.Range(0, Models.Length);
            _randomModel = Models[random];
            if (_randomModel != null)
            {
                _meshFilter.mesh = _randomModel;
            }
        }
    }

    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        GameManager.Score += Score;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            if (PickupSound != null)
            {
                Instantiate(PickupSound, transform.position, transform.rotation);
            }
            GrantEffect(player);
            ResetPickup();
            if (!PickupManager.Instance.ReturnScrap(this))
            {
                Destroy(gameObject);
            }
        }
    }
}
