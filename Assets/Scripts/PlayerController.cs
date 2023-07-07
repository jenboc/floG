using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int lives;
    
    private Rigidbody2D _rb;
    private GameManager _gameManager;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    
    private void FixedUpdate()
    {
        var moveDirection = ProcessInputs() * speed;
        _rb.velocity = moveDirection;
    }

    private Vector2 ProcessInputs() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ball"))
            lives--;

        if (lives == 0)
            _gameManager.PlayerDied();
    }
}
