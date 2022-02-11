using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Panel : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    private bool _freePanel = true;
    private int _numberPanelLineX;
    private int _numberPanelLineY;

    public bool FreePanel => _freePanel;

    public void SetNumberPanel(int numberLineX, int numberLineY)
    {
        _numberPanelLineX = numberLineX;
        _numberPanelLineY = numberLineY;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Room>(out Room room))
        {
            _freePanel = false;
            room.SetNumberRoom(_numberPanelLineX, _numberPanelLineY);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Room>(out Room room))
        {
            _freePanel = true;
        }
    }
}
