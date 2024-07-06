using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    public float speed = 10.0f;
    private float strafe;
    private float move;

    //what do we need for interaction stuff?
    private bool isInteracting;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //grab the mouse on start
    }

    private void Update()
    {
        //Moovy
        move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(strafe, 0, move);

    }

    private void FixedUpdate()
    {
        
    }
}
