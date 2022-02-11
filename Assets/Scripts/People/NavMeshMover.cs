using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMover : MonoBehaviour
{
    [SerializeField] private Transform _movePosition;
    [SerializeField] private NavMeshAgent _naveMeshAgent;

    private void Update()
    {
        _naveMeshAgent.destination = _movePosition.position;
    }
}
