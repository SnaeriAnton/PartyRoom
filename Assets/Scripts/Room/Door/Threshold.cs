using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Threshold : MonoBehaviour
{
    public event UnityAction OpenedDoor;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Door>(out Door door))
        {
            OpenedDoor?.Invoke();
        }
    }
}
