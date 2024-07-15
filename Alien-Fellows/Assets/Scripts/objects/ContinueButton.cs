using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [Header("Door Settings ")]
    [SerializeField] private GameObject specificDoorToOpen; // Reference to the specific door GameObject that will be opened.
    [SerializeField] private AudioSource doorOpenSound; // Reference to the AudioSource component for playing the door opening sound.

    [Header("Door Rotation ")]
    [SerializeField] private float rotationDuration = 2f; // Duration (in seconds) of the door's rotation animation.
    [SerializeField] private Vector3 openRotation = new Vector3(0, 90, 0); // Target rotation vector for the door when fully opened.

    private bool isOpening = false; // Flag to check if the door is currently opening.

    private void OnMouseUpAsButton()
    {
        // This method is called when the mouse button is released over this button.
        OpenDoorWithRotation(); // Initiates the door opening process.
    }

    private void EnableAndDisableDoor() // This can is still here in case you guys don't want the rotation 
    {
        // Toggles the door's active state to open or close it.
        specificDoorToOpen.SetActive(!specificDoorToOpen.activeSelf);
        doorOpenSound.Play(); // Plays the sound associated with the door opening.
    }

    private void OpenDoorWithRotation()
    {
        // Checks if the door is not already opening before initiating the opening process.
        if (!isOpening)
        {
            doorOpenSound.Play(); // Plays the door opening sound.
            StartCoroutine(RotateDoor(openRotation, rotationDuration)); // Starts the rotation coroutine to animate the door opening.
        }
    }

    // Coroutine to smoothly rotate the door from its current rotation to the target rotation over the specified duration.
    private IEnumerator RotateDoor(Vector3 targetRotation, float duration)
    {
        isOpening = true; // Indicates that the door opening process has started.
        Quaternion startRotation = specificDoorToOpen.transform.rotation; // Stores the initial rotation of the door.
        Quaternion endRotation = Quaternion.Euler(targetRotation); // Calculates the target rotation as a Quaternion.
        float time = 0.0f; // Initializes a time counter.

        // Continuously interpolates the door's rotation from its initial to target rotation over the duration.
        while (time < duration)
        {
            specificDoorToOpen.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration); // Interpolates rotation.
            time += Time.deltaTime; // Increments the time counter.
            yield return null; // Waits for the next frame before continuing the loop.
        }

        specificDoorToOpen.transform.rotation = endRotation; // Ensures the door's rotation exactly matches the target rotation.
        isOpening = false; // Resets the flag as the door has finished opening.
    }
}