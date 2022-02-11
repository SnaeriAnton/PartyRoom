using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsParticle : MonoBehaviour
{
    [SerializeField] private Door[] _doors;
    [SerializeField] private ParticleSystem _star;
    [SerializeField] private Transform _position;

    private float _offsetY = 2f;

    private void OnEnable()
    {
        for (int i = 0; i < _doors.Length; i++)
        {
            _doors[i].OpenedDoor += OnPlay;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _doors.Length; i++)
        {
            _doors[i].OpenedDoor -= OnPlay;
        }
    }

    private void OnPlay(Vector3 position)
    {
        _position.position = new Vector3(position.x, position.y + _offsetY, position.z);
        _star.Play();
    }
}
