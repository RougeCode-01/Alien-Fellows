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
    public GameObject rotatedObject;


    void Update()
    {
        //Looky
        if (!isInteracting)
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
            rotatedObject.transform.localRotation = Quaternion.AngleAxis(objectRotation.y, Vector3.right);
            rotatedObject.transform.localRotation = Quaternion.AngleAxis(objectRotation.x, rotatedObject.transform.up);
        }

    }
}
