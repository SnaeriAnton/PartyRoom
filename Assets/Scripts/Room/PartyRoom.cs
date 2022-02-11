using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class PartyRoom : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private ParticleSystem[] _particleSystems;
    [SerializeField] private ParticleSystem _particleSystemsGlow;
    [SerializeField] private Door _door;
    [SerializeField] private AudioSource _audioSource;

    private int _count;
    private List<Human> _people = new List<Human>();
    private int _halfOfPeople;

    public event UnityAction<Vector3> Entered;

    private void OnEnable()
    {
        _door.VerifiedPeople += OnChek;
        _halfOfPeople = _count / 2;
    }

    public void SetSumPeople(int count)
    {
        _count = count;
    }

    private void Win()
    {
        if (_halfOfPeople <= _people.Count)
        {
            Entered?.Invoke(_transform.position);
            _particleSystemsGlow.Play();
        }
    }

    private void OnChek(bool peopleHere)
    {
        if (peopleHere == true)
        {
            _audioSource.Play();
            for (int i = 0; i < _particleSystems.Length; i++)
            {
                _particleSystems[i].Play();
            }
            _door.VerifiedPeople -= OnChek;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Human>(out Human human))
        {
            _people.Add(human);
            Win();
        }
    }
}
