using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionDetector : MonoBehaviour
{
    PlayerController playerController;
    private int collisions = 0;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Ground Collider++");
        collisions++;
        if (playerController.isGrounded != true)
        {
            playerController.isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Ground Collider--");
        collisions--;
        if (playerController.isGrounded == true && collisions == 0)
        {
            playerController.isGrounded = false;
        }
    }
}
