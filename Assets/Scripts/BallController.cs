using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D _rb;
    
    private Vector3 _moveDirection;
    private bool _onScreen;
    
    private void Start()
    {
        var playerObject = GameObject.FindWithTag("Player");
        _moveDirection = playerObject.transform.position - transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var translation = _moveDirection * (speed);
        _rb.velocity = translation;
    }
    
    private void OnBecameInvisible() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
