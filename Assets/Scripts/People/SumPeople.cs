using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SumPeople : MonoBehaviour
{
    [SerializeField] private PartyRoom _partyRoom;

    private void Start()
    {
        var people = gameObject.GetComponentsInChildren<Human>();
        _partyRoom.SetSumPeople(people.Length);
    }
}
