using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    // Reference to the specific door GameObject you want to control
    public GameObject specificDoor;

    private void OnMouseUpAsButton()
    {
        // Toggle the door's active state to open/close it
        specificDoor.SetActive(!specificDoor.activeSelf);
    }
}
