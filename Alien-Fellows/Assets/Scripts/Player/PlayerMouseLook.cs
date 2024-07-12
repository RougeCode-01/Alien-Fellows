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


    void Update()
    {
        //Looky
        if (!isInteracting && !inDialogue)
        {
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            mouselook += mouseDelta;
            mouselook.y = Mathf.Clamp(mouselook.y, -90f, 90f);
            transform.localRotation = Quaternion.AngleAxis(mouselook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouselook.x, character.transform.up);
        }

        if (isRotating)
        {
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            objectRotation += mouseDelta;
            //mouselook.y = Mathf.Clamp(mouselook.y, -90f, 90f);
            rotatedObject.transform.rotation = Quaternion.Euler(objectRotation.y, objectRotation.x, 0);
            //rotatedObject.transform.localRotation = Quaternion.AngleAxis(objectRotation.x, Vector3.up);
        }

    }
}
