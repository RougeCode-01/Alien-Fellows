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


    void Update()
    {
        //Looky
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouselook += mouseDelta;
        mouselook.y = Mathf.Clamp(mouselook.y, -90f, 90f);
        transform.localRotation = Quaternion.AngleAxis(mouselook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouselook.x, character.transform.up);
    }
}
