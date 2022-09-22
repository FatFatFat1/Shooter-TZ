using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    private PlayerController _pControll;
    private Rigidbody2D _rb2;
    private Vector3 _mousePosition;
    private Vector3 oldPos;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _bullet;

    private void Awake()
    {
        _pControll = new PlayerController();
        _rb2 = GetComponent<Rigidbody2D>();
        _pControll.Player.Shot.started += _ => Shot();
    }
    private void Update()
    {
        Move(_pControll.Player.Move.ReadValue<Vector2>());
        Rotate();
    }

    private void OnEnable()
    {
        _pControll.Enable();
    }
    private void OnDisable()
    {
        _pControll.Disable();
    }
    void Move(Vector2 dir)
    {
        _rb2.velocity = dir * Time.deltaTime * _speed;
    }

    void Shot()
    {
        Instantiate(_bullet, transform.position + transform.right, transform.rotation);
    }
    void Rotate()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_mousePosition != oldPos)
        {
            Vector3 difference = _mousePosition - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
            oldPos = _mousePosition;
        }
    }
}
