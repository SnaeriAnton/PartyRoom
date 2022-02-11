using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Point : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    public event UnityAction ChangedPosition;
    public event UnityAction ChangedStatus;

    public void ChangePosition()
    {
        ChangedPosition?.Invoke();
    }

    public void SetNewPosition(Vector3 newPosition)
    {
        _transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Human>(out Human human))
        {
            ChangedStatus?.Invoke();
        }
    }
}
