using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class Emoji : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particalSistem;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Wining _wining;
    [SerializeField] private Transform _transform;
    [SerializeField] private Room[] _rooms;

    private float _timeShowEmoji = 7f;
    private float _timer = 0;
    private bool _isPlaying = false;

    private void OnEnable()
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            _rooms[i].Transformed += OnChangePosiyion;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            _rooms[i].Transformed -= OnChangePosiyion;
        }
    }

    private void Update()
    {
        RunningTime();
    }

    private void RunningTime()
    {
        if (_timeShowEmoji < _timer)
        {
            PlayEmoji();
        }
        else
        {
            IncreaseTimer();
        }
    }

    private void PlayEmoji()
    {
        if (_isPlaying == false && _wining.IsWining == false)
        {
            _isPlaying = true;
            _particalSistem.Play();
            _audioSource.Play();
        }
    }

    private void IncreaseTimer()
    {
        _timer += Time.deltaTime;
    }

    private void OnChangePosiyion(Vector3 position)
    {
        if (_isPlaying == false)
        {
            _transform.position = new Vector3(position.x, _transform.position.y, position.z);
        }
    }
}
