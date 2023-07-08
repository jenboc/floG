using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int lives;
    [SerializeField] private float fireSpeed;
    [SerializeField] private int ammo; 
    [SerializeField] private TurretController turretController;
    [SerializeField] private AudioClip turretFireAudio;
    [SerializeField] private AudioClip ammoPickupAudio;
    [SerializeField] private int ammoOnPickup;
    
    private Rigidbody2D _rb;
    private Vector3 _playerSize;
    private GameManager _gameManager;
    private Camera _camera;
    private float _fireTimer;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerSize = GetComponent<SpriteRenderer>().bounds.size;
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _camera = Camera.main;
        _audioSource = GetComponent<AudioSource>();

        _fireTimer = fireSpeed;
        
        _gameManager.UpdateLives(lives);
        _gameManager.UpdateAmmo(ammo);
    }

    private void Update()
    {
        CheckInBounds();
        CheckShoot(); 
    }

    private void CheckShoot()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer < fireSpeed || !Input.GetMouseButton(0) || ammo <= 0)
            return;
        
        _audioSource.PlayOneShot(turretFireAudio);
        turretController.Fire();
        ammo--;
        _fireTimer = 0;
        _gameManager.UpdateAmmo(ammo);
    }

    private void CheckInBounds()
    {
        // The top left and bottom right corners of the boundary, for the player's midpoint specifically
        var topLeft = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)) + (_playerSize / 2);
        var bottomRight = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0)) - (_playerSize / 2);

        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, topLeft.x, bottomRight.x);
        pos.y = Mathf.Clamp(pos.y, topLeft.y, bottomRight.y);
        transform.position = pos;
    }
    
    private void FixedUpdate()
    {
        if (!_gameManager.GameActive)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        var moveDirection = ProcessInputs() * speed;
        _rb.velocity = moveDirection;
    }

    private Vector2 ProcessInputs() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Ball":
                lives--;
                _gameManager.UpdateLives(lives);
                break;
            case "Ammo":
                ammo += ammoOnPickup;
                Destroy(other.gameObject);
                _audioSource.PlayOneShot(ammoPickupAudio);
                _gameManager.UpdateAmmo(ammo);
                break;
        }
        
        if (lives == 0)
            _gameManager.PlayerDied();
    }
}
