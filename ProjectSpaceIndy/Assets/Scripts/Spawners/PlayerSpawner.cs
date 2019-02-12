using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Spawner<PlayerUnit>
{
    public Camera MainCamera;
    private PlayerUnit _player;

    private void Update()
    {
        if (_player == null)
        {
            _player = Spawn();
            MainCamera.GetComponent<FollowCamera>().target = _player.gameObject;
            _player.GetComponent<PlayerMover>().Camera = MainCamera;
        }
    }

    protected override void PostSpawn(PlayerUnit spawnedObject)
    {
        MainCamera.GetComponent<FollowCamera>().target = spawnedObject.gameObject;
        spawnedObject.GetComponent<PlayerMover>().Camera = MainCamera;
    }
}
