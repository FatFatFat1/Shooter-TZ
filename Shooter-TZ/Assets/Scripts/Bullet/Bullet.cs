using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rg2;
    private Vector3 _lastVel;
    private GameObject _victim;
    [SerializeField] private float _bulletSpeed;

    private const string WALL_TAG = "Wall";
    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";
    private const string ARENA_TAG = "Arena";

    private void Awake()
    {
        _rg2 = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        _rg2.AddForce(transform.right.normalized * _bulletSpeed);
    }

    private void Update()
    {
        _lastVel = _rg2.velocity;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(WALL_TAG))
        {
            var speed = _lastVel.magnitude;
            var direction = Vector3.Reflect(_lastVel.normalized, collision.contacts[0].normal);
            _rg2.velocity = direction * Mathf.Max(speed, 1f);
        }
        if (collision.collider.CompareTag(PLAYER_TAG)|| collision.collider.CompareTag(ENEMY_TAG))
        {
            _victim = collision.gameObject;
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag(ARENA_TAG))
        {
            Destroy(gameObject);
        }
    }
}
