using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rg2;
    private Vector3 _lastVel;
    private string _source;

    [SerializeField] private float _bulletSpeed;

    private const string WALL_TAG = "Wall";
    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";
    private const string ARENA_TAG = "Arena";

    public void Shot(GameObject shooter, float angle)
    {
        GameObject bullet = Instantiate(gameObject, shooter.transform.position + shooter.transform.right, Quaternion.Euler(0f, 0f, angle));
        bullet.GetComponent<Bullet>()._source = shooter.tag;
        bullet.GetComponent<Rigidbody2D>().AddForce(shooter.transform.right.normalized * _bulletSpeed);
    }
    private void Start()
    {
        _rg2 = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _lastVel = _rg2.velocity;
    }
    void Reflect(Collision2D collision)
    {
        var speed = _lastVel.magnitude;
        var direction = Vector3.Reflect(_lastVel.normalized, collision.contacts[0].normal);
        _rg2.velocity = direction * Mathf.Max(speed, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(WALL_TAG))
        {
            Reflect(collision);
        }
        if (collision.collider.CompareTag(PLAYER_TAG) || collision.collider.CompareTag(ENEMY_TAG))
        {
            string victim = collision.gameObject.tag;
            if (_source != victim)
            {
                collision.gameObject.GetComponent<CharacterData>().Scope += 1;
            }
            Destroy(gameObject);

        }
        if (collision.collider.CompareTag(ARENA_TAG))
        {
            Destroy(gameObject);
        }
    }
}
