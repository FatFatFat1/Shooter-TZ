using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    private PlayerController _pControll;
    private Rigidbody2D _rb2;
    [SerializeField] private float speed;
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
        _rb2.velocity = dir * Time.deltaTime * speed;
    }
    public void Shot()
    {
        Instantiate(_bullet, transform.position + transform.right, transform.rotation);
    }
}
