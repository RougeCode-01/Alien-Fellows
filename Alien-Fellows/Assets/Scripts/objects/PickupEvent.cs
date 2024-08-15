using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupEvent : MonoBehaviour
{
    public UnityEvent Interacted = new UnityEvent();

    public void PickedUp()
    {
        Interacted.Invoke();
    }
}
