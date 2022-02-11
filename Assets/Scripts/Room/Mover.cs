using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _current;

    private float _time = 0.1f;
    private float _offset = 0.05f;
    private Vector3 _direction;
    private bool _action = false;

    private void Start()
    {
        _direction = _current.position;
    }

    private void Update()
    {
        if (_current.position != _direction)
        {
            Move();
        }
    }

    public void SetDirection(Vector3 target)
    {
        _direction = new Vector3(target.x, _current.position.y, target.z);
        _action = true;
    }

    private void Displace()
    {
       _current.DOMove(new Vector3(_current.position.x + (_offset * _direction.x), _current.position.y, _current.position.z + (_offset * _direction.z)), _time);
       _direction = _current.position;
       _action = false;
    }

    private void Move()
    {
        if ((_direction.x == 0 && _action == true) || (_direction.z == 0 && _action == true))
        {
            Displace();
        }
        else
        {
            _current.position = Vector3.MoveTowards(_current.position, _direction, Time.deltaTime * _speed);
            _action = false;
        }
    }
}
