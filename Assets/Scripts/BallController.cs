using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int scoreForDodging;

    private Rigidbody2D _rb;
    private GameManager _gameManager;
    
    private Vector3 _moveDirection;
    private bool _hitPlayer;
    
    private void Start()
    {
        var playerObject = GameObject.FindWithTag("Player");
        _moveDirection = playerObject.transform.position - transform.position;
        _moveDirection.Normalize();
        _rb = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _hitPlayer = false;
    }

    private void FixedUpdate()
    {
        var translation = _moveDirection * (speed);
        _rb.velocity = translation;
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
            Destroy(gameObject);
            _hitPlayer = true;
        }
    }
}
