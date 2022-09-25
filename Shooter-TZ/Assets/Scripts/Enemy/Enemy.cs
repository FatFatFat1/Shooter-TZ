using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _mylayer = 1 << 8;
    private const string PLAYER = "Player";
    private GameObject _myEnemy;
    private GameObject _target;
    private float _reloadingDeltaTime = 0;
    private bool _isReload = false;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _timeOfReloading;

    private void Start()
    {
        _myEnemy = GameObject.FindGameObjectWithTag(PLAYER);
    }
    private void FixedUpdate()
    {
        TryShot();
        if(_isReload)
        {
            _reloadingDeltaTime += Time.deltaTime;
            if(_reloadingDeltaTime > _timeOfReloading)
            {
                _isReload = !_isReload;
            }
        }
    }
    bool RicochetCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, ~_mylayer);

        foreach (Collider2D i in hits)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, i.transform.position - transform.position, Mathf.Infinity, ~_mylayer);
            Vector2 hitPoint = new Vector2(hit.point.x - transform.position.x, hit.point.y - transform.position.y);
            Vector2 reflectDirection = Vector2.Reflect(hitPoint.normalized, hit.normal);
            RaycastHit2D checkHit = Physics2D.Raycast(hit.point, reflectDirection, Mathf.Infinity, ~_mylayer);
            if (checkHit.collider && checkHit.collider.gameObject == _myEnemy)
            {
                _target = i.gameObject;
                return true;
            }
        }
        return false;
    }

    void TryShot()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, _myEnemy.transform.position - transform.position, Mathf.Infinity, ~_mylayer);
        if (playerHit.collider && playerHit.collider.gameObject == _myEnemy)
        {
            Shot(_myEnemy);
        }
        else
        {
            if (RicochetCheck())
            {
                Shot(_target);
            }
        }

    }
    void Shot(GameObject target)
    {
        if(_isReload == false)
        {
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _bullet.GetComponent<Bullet>().Shot(gameObject, angle);
            Vector3 difference = target.transform.position - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
            _reloadingDeltaTime = 0;
            _isReload = true;
        }
    }

}
