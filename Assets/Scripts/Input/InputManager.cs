using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pusher))]
public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Pusher _pusher;

    private float _maxDistance = 100000000000f;
    private bool _isRoom = false;
    private RaycastHit _object;
    private Room _room;

    private Vector2 _startPosition = Vector2.zero;
    private Vector2 _endPosition = Vector2.zero;
    private Vector3 _direction = Vector3.zero;

    private void OnEnable()
    {
        _pusher.Redeted += OnResetDirection;
    }

    private void OnDisable()
    {
        _pusher.Redeted -= OnResetDirection;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _room = GetRoom();
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _startPosition = GetPosition();
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _endPosition = GetPosition();
                _direction = CalculationDirection(_endPosition, _startPosition);
            }
        }
        _pusher.PushRoom(_direction, _isRoom, _room);
    }

    private Room GetRoom()
    {
        _isRoom = Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.GetTouch(0).position).direction, out _object, _maxDistance, _layerMask);

        if (_isRoom == true)
        {
            return _object.transform.GetComponent<Room>();
        }
        return null;
    }

    private Vector2 GetPosition()
    {
        return Input.GetTouch(0).position;
    }

    private Vector2 CalculationDirection(Vector2 endPosition, Vector2 startPosition)
    {
        Vector2 direction = endPosition - startPosition;
        return direction.normalized;
    }

    private void OnResetDirection()
    {
        _isRoom = false;
        _direction = Vector3.zero;
    }
}
