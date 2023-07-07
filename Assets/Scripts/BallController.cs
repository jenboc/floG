using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 _moveDirection;
    private bool _onScreen;
    
    private void Start()
    {
        var playerObject = GameObject.FindWithTag("Player");
        _moveDirection = playerObject.transform.position - transform.position;
    }

    private void Update()
    {
        var translation = _moveDirection * (speed * Time.deltaTime);
        transform.Translate(translation);
    }
    
    private void OnBecameInvisible() => Destroy(gameObject);
}
