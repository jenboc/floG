using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject ammoCrate;
    [SerializeField] private float minSpawnCooldown;
    [SerializeField] private float maxSpawnCooldown;

    private float _spawnCooldown;
    private float _cooldownTimer;
    private Camera _camera;
    private Vector2 _crateSize;

    private void Start()
    {
        _camera = Camera.main;
        NewSpawnCooldown();
        _cooldownTimer = _spawnCooldown;

        _crateSize = ammoCrate.GetComponent<SpriteRenderer>().size;
    }

    private void NewSpawnCooldown() => _spawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (_cooldownTimer < _spawnCooldown)
            return;

        SpawnCrate();
        NewSpawnCooldown();
        _cooldownTimer = 0f;
    }

    private void SpawnCrate() => Instantiate(ammoCrate, NewSpawnPosition(), Quaternion.identity);

    private Vector3 NewSpawnPosition()
    {
        var topLeft = (Vector2)_camera.ScreenToWorldPoint(new Vector3(0, 0, 0)) + (_crateSize / 2);
        var bottomRight = 
            (Vector2)_camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0)) -
                          (_crateSize / 2);

        return new Vector3(
            Random.Range(topLeft.x, bottomRight.x),
            Random.Range(topLeft.y, bottomRight.y),
            0
        );
    }
}
