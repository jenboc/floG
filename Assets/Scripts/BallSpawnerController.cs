using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float minSpawnCooldown;
    [SerializeField] private float spawnCooldownDelta;

    private float _cooldownTimer;
    private Camera _camera;
    private GameManager _gameManager;

    private const float SPAWN_RADIUS = 15;
    
    private void Start()
    {
        _cooldownTimer = 0f;
        _camera = Camera.main;
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    
    private void Update()
    {
        if (!_gameManager.GameActive)
            return;
        
        _cooldownTimer += Time.deltaTime;
        
        if (_cooldownTimer >= spawnCooldown)
        {
            SpawnBall();
            ReduceCooldown();
            _cooldownTimer = 0f;
        }
    }

    private void ReduceCooldown()
    {
        spawnCooldown -= spawnCooldownDelta;
        
        if (spawnCooldown < minSpawnCooldown)
        {
            spawnCooldown = minSpawnCooldown;
            spawnCooldownDelta = 0;
        }
    }

    private void SpawnBall()
    {
        var spawnPos = GetSpawnPosition();
        spawnPos.z = 0; // Ensure the ball is visible on screen
        Instantiate(_ball, spawnPos, Quaternion.identity);
    }

    private Vector3 GetSpawnPosition()
    {
        var border = Random.value;

        // Left Border
        if (border <= 0.25)
        {
            return _camera.ScreenToWorldPoint(new Vector3(
                -SPAWN_RADIUS,
                Random.Range(-SPAWN_RADIUS, _camera.pixelHeight + SPAWN_RADIUS),
                0
            ));
        }
        
        // Top Border
        if (border <= 0.5)
        {
            return _camera.ScreenToWorldPoint(new Vector3(
                Random.Range(-SPAWN_RADIUS, _camera.pixelWidth + SPAWN_RADIUS),
                -SPAWN_RADIUS,
                0
            ));
        }
        
        // Right Border
        if (border <= 0.75)
        {
            return _camera.ScreenToWorldPoint(new Vector3(
                _camera.pixelWidth + SPAWN_RADIUS,
                Random.Range(-SPAWN_RADIUS, _camera.pixelHeight + SPAWN_RADIUS),
                0
            ));
        }
        
        // Bottom Border
        return _camera.ScreenToWorldPoint(new Vector3(
            Random.Range(-SPAWN_RADIUS, _camera.pixelWidth + SPAWN_RADIUS),
            _camera.pixelHeight + SPAWN_RADIUS,
            0
        ));
    }
}
