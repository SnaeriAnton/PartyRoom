
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pusher : MonoBehaviour
{
    private float directionThreshold = 0.9f;
    private float _delayBetweenMove = 0.35f;
    private float _timer = 0;

    public event UnityAction Redeted;


    public void PushRoom(Vector3 direction, bool isRoom, Room room)
    {
        if (CheakDirectionPush(direction, isRoom))
        {
            HandOverDirection(direction, room);
        }
    }

    private bool CheakDirectionPush(Vector3 direction, bool isRoom)
    {
        if (_timer > _delayBetweenMove)
        {
            if (isRoom == true && direction != Vector3.zero)
            {
                return true;
            }
            return false;
        }
        else
        {
            _timer += Time.deltaTime;
            Redeted?.Invoke();
            return false;
        }
    }

    private void HandOverDirection(Vector3 direction, Room room)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Debug.Log("Up");
            room.Move(new Vector2(1, 0));
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Down");
            room.Move(new Vector2(-1, 0));
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Right");
            room.Move(new Vector2(0, 1));
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Left");
            room.Move(new Vector2(0, -1));
        }
        _timer = 0;
        Redeted?.Invoke();
    }
}
