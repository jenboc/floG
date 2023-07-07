using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int lives;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        var moveDirection = ProcessInputs() * speed;
        _rb.velocity = moveDirection;
    }

    private Vector2 ProcessInputs() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    private void OnTriggerEnter2D(Collider2D collider)
    {
        lives--;
        Debug.Log(lives);
    }
}
