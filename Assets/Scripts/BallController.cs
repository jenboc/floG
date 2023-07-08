using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] protected float initialForce;
    [SerializeField] private int scoreForDodging;
    [SerializeField] private AudioClip[] swingSounds;
    [SerializeField] private AudioClip holeSound;

    protected Rigidbody2D _rb;
    private GameManager _gameManager;
    
    protected Vector2 _moveDirection;
    private bool _hitPlayer;

    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _hitPlayer = false;
    }
    
    private void Start()
    {
        _gameManager.PlaySound(swingSounds[Random.Range(0, swingSounds.Length)]);
        CalculateMoveDirection();
        ApplyInitialForce();
    }

    protected void ApplyInitialForce(ForceMode2D mode = ForceMode2D.Force) 
        => _rb.AddForce(_moveDirection * initialForce, mode);
    
    protected void CalculateMoveDirection() => 
        _moveDirection = (_player.position - transform.position).normalized;

    
    protected virtual void FixedUpdate()
    {
        if (!_gameManager.GameActive)
        {
            _rb.velocity = Vector2.zero;
        }
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
