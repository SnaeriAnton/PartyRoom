using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class Wining : MonoBehaviour
{
    [SerializeField] private PartyRoom _partyRoom;
    [SerializeField] private Transform _transform;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PostProcessVolume _filter;

    private float _offsetPositionX = 0.02f;
    private float _offsetPositionY = -8.01f;
    private float _offsetPositionZ = 4.74f;
    private float _speed = 1f;
    private float _rotationX = 53f;
    private bool _isWining = false;
    private float _timer = 0;
    private float _delayTime = 5f;

    public bool IsWining => _isWining;

    private void OnEnable()
    {
        _partyRoom.Entered += OnEnter;
    }

    private void OnDisable()
    {
        _partyRoom.Entered -= OnEnter;
    }

    private void Update()
    {
        if (_isWining == true)
        {
            ShowPanel();
        }
    }

    private void OnEnter(Vector3 position)
    {
        _filter.enabled = true;
        _isWining = true;
        _transform.DOMove(new Vector3(position.x - _offsetPositionX, position.y - _offsetPositionY, position.z - _offsetPositionZ), _speed);
        _transform.DORotateQuaternion(Quaternion.Euler(_rotationX, 0, 0), _speed);
        _inputManager.enabled = false;
    }

    private void ShowPanel()
    {
        if (_delayTime < _timer)
        {
            _panelWin.SetActive(true);
        }
        else
        {
            IncreaseTimer();
        }
    }

    private void IncreaseTimer()
    {
        _timer += Time.deltaTime;
    }
}
