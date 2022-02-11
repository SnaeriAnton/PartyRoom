using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
public class Room : MonoBehaviour
{
    [SerializeField] private Mover _move;
    [SerializeField] private MainPanel _mainPanel;
    [SerializeField] private Door _door;
    [SerializeField] private Transform _position;

    private bool _peopleHere;
    delegate Vector3 Moved(Vector2 direction, int numberLineX, int numberLineY);
    private Moved _moved;

    private int _numberRoomLineX;
    private int _numberRoomLineY;

    public Transform Position => _position;

    public event UnityAction<Vector3> Connected;
    public event UnityAction<Vector3> Transformed;

    private void OnEnable()
    {
        _door.OpenedRoom += OnConnecte;
    }

    private void OnDisable()
    {
        _door.OpenedRoom -= OnConnecte;
    }

    private void Start()
    {
        _moved = _mainPanel.GetPositionPanel;
    }

    public void Move(Vector2 direction)
    {
        _move.SetDirection(_moved(direction, _numberRoomLineX, _numberRoomLineY));
    }

    public void SetNumberRoom(int numberLineX, int numberLineY)
    {
        _numberRoomLineX = numberLineX;
        _numberRoomLineY = numberLineY;
        if (_peopleHere == true)
        {
            Transformed?.Invoke(_position.position);
        }
    }

    private void OnConnecte()
    {
        if (_peopleHere == false)
        {
            Connected?.Invoke(_position.position);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Human>(out Human human))
        {
            _peopleHere = true;
            _door.SetStatusPeopeInRoom(_peopleHere);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Human>(out Human human))
        {
            _peopleHere = false;
        }
    }
}
