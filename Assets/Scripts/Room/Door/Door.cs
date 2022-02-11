using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _current;
    [SerializeField] private Threshold _threshold;

    private float _speed = 0.6f;
    private float _offset = -9.9f;
    private float _defaultPositionY;
    private Vector3 _direction;
    private float _timer = 0;
    private float _delayCloseDoor = 0.26f;
    private bool _isClosed = true;
    private bool _peoplInThisRoom;

    public event UnityAction OpenedRoom;
    public event UnityAction<Vector3> OpenedDoor;
    public event UnityAction<bool> VerifiedPeople;

    private void OnEnable()
    {
        _threshold.OpenedDoor += OnOpen;
    }

    private void OnDisable()
    {
        _threshold.OpenedDoor -= OnOpen;
    }

    private void Start()
    {
        _direction = _current.localPosition;
        _defaultPositionY = _current.localPosition.y;
    }

    private void Update()
    {
        _current.localPosition = Vector3.MoveTowards(_current.localPosition, _direction, _speed);
        DelayClosing(_isClosed);
    }

    public void SetStatusPeopeInRoom(bool peopleHere)
    {
        _peoplInThisRoom = peopleHere;
        VerifiedPeople?.Invoke(_peoplInThisRoom);
    }

    private void DelayClosing(bool isClosed)
    {
        if (isClosed == false)
        {
            if (_timer >= _delayCloseDoor)
            {
                _direction = new Vector3(_current.localPosition.x, _defaultPositionY, _current.localPosition.z);
                _timer = 0;
                _isClosed = true;
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
    }

    private void OnOpen()
    {
        OpenedRoom?.Invoke();
        OpenedDoor?.Invoke(_current.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Door>(out Door door))
        {
            _direction = new Vector3(_current.localPosition.x, _offset, _current.localPosition.z);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Door>(out Door door))
        {
            _isClosed = false;
        }
    }

}
