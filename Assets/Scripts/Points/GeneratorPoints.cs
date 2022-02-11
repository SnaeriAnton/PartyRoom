using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorPoints : MonoBehaviour
{
    [SerializeField] private Point[] _points;
    [SerializeField] private Room[] _rooms;

    private float _offsetPositionY = -8.5f;
    private float _offsetPosition = 1.8f;

    public event UnityAction<bool> Generated;

    private void OnEnable()
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            _rooms[i].Connected += OnSetPointsInRoom;
        }

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].ChangedStatus += OnEndMove;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            _rooms[i].Connected -= OnSetPointsInRoom;
        }

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].ChangedStatus -= OnEndMove;
        }
    }

    private void OnEndMove()
    {
        Generated?.Invoke(false);
    }

    private void OnSetPointsInRoom(Vector3 position)
    {
        GeneratorPosition(position);
    }

    private void GeneratorPosition(Vector3 position)
    {
        Generated?.Invoke(true);
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].SetNewPosition(new Vector3(Random.Range(position.x - _offsetPosition, position.x + _offsetPosition), _offsetPositionY, Random.Range(position.z - _offsetPosition, position.z + _offsetPosition)));
            _points[i].ChangePosition();
        }
    }
}
