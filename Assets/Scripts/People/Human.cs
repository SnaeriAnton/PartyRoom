using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshMover))]
[RequireComponent(typeof(HumanAnimator))]
public class Human : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private NavMeshMover _navMeshMover;
    [SerializeField] private Point _point;
    [SerializeField] private Transform _transform;
    [SerializeField] private HumanAnimator _humanAnimator;

    private bool _hasReachesPoint = false;

    private void OnEnable()
    {
        _point.ChangedPosition += OnEnableMove;
    }

    private void OnDisable()
    {
        _point.ChangedPosition -= OnEnableMove;
    }

    private void OnEnableMove()
    {
        SweitchMove(true);
    }

    private void SweitchMove(bool value)
    {
        _navMeshAgent.enabled = value;
        _navMeshMover.enabled = value;
        if (value == true)
        {
            _humanAnimator.Walk();
        }
        else
        {
            _humanAnimator.Stay();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Point>(out Point point))
        {
            if (point.GetHashCode() == _point.GetHashCode())
            {
                _hasReachesPoint = true;
                SweitchMove(false);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Room>(out Room room))
        {
            _transform.SetParent(room.Position);

            if (collision.gameObject.TryGetComponent<PartyRoom>(out PartyRoom partyRoom) && _hasReachesPoint == true)
            {
                _humanAnimator.Dance();
                _hasReachesPoint = false;
            }
            else
            {
                _hasReachesPoint = false;
            }
        }
    }
}
