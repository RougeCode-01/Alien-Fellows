using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTracker : MonoBehaviour
{
    public PlayerController playerController;
    //private GameObject examinedObject;

    //I'm going to set this up so we're coupling the event tracker to each essential game object - bad form given what we were just learning on monday, I know,
    //but considering the scope it's probably easier to set it up this way so we can stitch it all together for the final push.

    //assign each essential object with the keycode pieces here
    /*
    public GameObject essentialObject01;
    public GameObject essentialObject02;
    public GameObject essentialObject03;
    public GameObject essentialObject04;
    public GameObject essentialObject05;

    //place the OpenDoor script on the door's pivot point and assign them here
    public OpenDoor door01;
    public OpenDoor door02;
    public OpenDoor door03;
    public OpenDoor door04;
    public OpenDoor door05;
    */
    public void CheckExaminedObject(GameObject examinedObject) //here's a gruesome series of if else if statements that will check
                                                               //if we're holding the thing needed to open the next door
    {
        if ( examinedObject.GetComponent<PickupEvent>())
        {
            examinedObject.GetComponent<PickupEvent>().PickedUp();
        }
        /*
        if (examinedObject == essentialObject01)
        {
            door01.OpenEvent();
        }
        else if (examinedObject == essentialObject02)
        {
            door02.OpenEvent();
        }
        else if (examinedObject == essentialObject03)
        {
            door03.OpenEvent();
        }
        else if (examinedObject == essentialObject04)
        {
            door04.OpenEvent();
        }
        else if (examinedObject == essentialObject04)
        {
            door05.OpenEvent();
        }
        else 
        {
            
        }
        */
    }
}
