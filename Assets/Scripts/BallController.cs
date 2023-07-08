using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int scoreForDodging;
    [SerializeField] private AudioClip[] swingSounds;
    [SerializeField] private AudioClip holeSound;

    private Rigidbody2D _rb;
    private GameManager _gameManager;
    
    private Vector2 _moveDirection;
    private bool _hitPlayer;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _hitPlayer = false;

        _moveDirection = CalculateMoveDirection();
        
        _gameManager.PlaySound(swingSounds[Random.Range(0, swingSounds.Length)]);

        ApplyInitialForce(); 
    }

    private void ApplyInitialForce() => _rb.AddForce(_moveDirection * speed);
    
    private Vector2 CalculateMoveDirection() => 
        (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;

    private void FixedUpdate()
    {
        if (!_gameManager.GameActive)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
    
        // Old movement: constant velocity
        // var translation = _moveDirection * (speed);
        // _rb.velocity = translation;
    }

    private void OnBecameInvisible()
    {
        if (!_hitPlayer) _gameManager.UpdateScore(scoreForDodging);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _gameManager.PlaySound(holeSound);
            Destroy(gameObject);
            _hitPlayer = true;
        }
    }
}
