using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{

    //camera
    public GameObject character;                //turn the man
    public PlayerController playerController;   //grab this so we can disable mouselook when we interact with stuff
    public float mouseSensitivity = 2.0f;
    private Vector2 mouselook;
    private Vector2 objectRotation;
    public bool isInteracting;
    public bool isRotating;
    public bool inDialogue;
    public GameObject rotatedObject;
    
    public bool isMouseInverted = false;


    void Update()
    {
        //Looky
        if (!isInteracting && !inDialogue)
        {
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            
            if (isMouseInverted)
            {
                mouseDelta.y *= -1;
            }

            mouselook += mouseDelta;
            mouselook.y = Mathf.Clamp(mouselook.y, -90f, 90f);
            transform.localRotation = Quaternion.AngleAxis(mouselook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouselook.x, character.transform.up);
        }

        if (isRotating) //lots of experimenting here zzzz
        {
            // this one works! rotates the object nicely relative to the camera rather than it's own axes.

            float rotateX = Input.GetAxisRaw("Mouse X");
            float rotateY = Input.GetAxisRaw("Mouse Y");

            Vector3 right = Vector3.Cross(transform.up, rotatedObject.transform.position - transform.position);
            Vector3 up = Vector3.Cross(transform.position - rotatedObject.transform.position, right);

            rotatedObject.transform.rotation = Quaternion.AngleAxis(-rotateX, up) * rotatedObject.transform.rotation;
            rotatedObject.transform.rotation = Quaternion.AngleAxis(rotateY, right) * rotatedObject.transform.rotation;

            /* this works ok but all the rotation is based on the axes of the object itself, not relating to the axes of the camera - a little counter intuitive I don't love it
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            objectRotation += mouseDelta;
            rotatedObject.transform.rotation = Quaternion.Euler(objectRotation.y, objectRotation.x, 0);
            */
        }

    }
    
    public void ToggleMouseInversion()
    {
        isMouseInverted = !isMouseInverted;
    }
}
