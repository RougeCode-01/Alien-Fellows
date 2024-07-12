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
    public PlayerMouseLook playerMouseLook;
    private GameObject examinedObject; // what are we looking at
    private Vector3 storedTransform; //store the examined object's original transform
    public GameObject examinePoint; //where do we place the object we're examining?
    public float lerpTime = 1.0f; // how long does it take to move the obect from it's origin to the examine point?
    private bool isLerping; // using this to make sure we can't interrupt the objects while they're moving & screw up thier default position
    private bool mouseRotating; // are we spinning something?
    public GameObject dialogueTarget; // who are we talking to?
    public bool inDialogue;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //grab the mouse on start
    }

    private void Update()
    {

        //handling object rotation
        if (mouseRotating && Input.GetButtonUp("Fire1")) //check for mouse rotation release first
        {
            mouseRotating = false;
            playerMouseLook.isRotating = false;
            Cursor.lockState = CursorLockMode.None;
        }

        //object interaction logic:
        if (isInteracting)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == examinedObject || hit.transform.IsChildOf(examinedObject.transform))     // If we click on the selected object again...
                    {
                        ObjectTrigger objectTrigger = hit.collider.gameObject.GetComponent<ObjectTrigger>();                // check if we're clicking on a button or something first...
                        //Debug.Log(objectTrigger);

                        if (objectTrigger != null)                                                                          // If we are, signal to that trigger object -
                        {
                            objectTrigger.ButtonPressed();
                        }
                            
                        else if (objectTrigger == null && hit.collider.gameObject == examinedObject)                        // If not, begin to rotate the object instead.
                        {
                            Rotate();
                        }
                    }
                    else                        // Disengage from the object if we click on a different object in the background...
                    {
                        ExitInteract();
                    }
                }
                else                            // ... Or if we click on nothing at all.
                {
                    ExitInteract();
                }

            }
        }

        //Moovy
        if (!isInteracting) // can't move while looking at stuff
        {
            move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(strafe, 0, move);
        }

        //Interact
        if (!isInteracting && !isLerping && Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Interactable")) // cast a ray from the player's camera then check if it's something we can look at
            {
                examinedObject = hit.collider.gameObject;
                storedTransform = examinedObject.transform.position;
                EnterInteract();
            }

            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("NPC"))
            {
                EnterDialogue();
            }
                
            else
            {
                //nothing doing
            }
        }
    }

    private void EnterInteract() //set our interacting state, unlock the mouse cursor, and start the Lerp coroutine to bring the object to the camera so we can look at it closely
    {
        isInteracting = true;
        playerMouseLook.isInteracting = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(LerpObjectToPlayer());
    }

    private void ExitInteract() // reverse of above
    {
        StopAllCoroutines();
        isInteracting = false;
        playerMouseLook.isInteracting = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(LerpObjectBack());
    }

    public void Rotate() // spin the object with the mouse
    {
        //Debug.Log("rotating");
        mouseRotating = true;
        Cursor.lockState = CursorLockMode.Locked;
        playerMouseLook.rotatedObject = examinedObject;
        playerMouseLook.isRotating = true;
    }

    //Lerp Coroutunes

    IEnumerator LerpObjectToPlayer() //we'll move the object to our examine location with this
    {
        Vector3 currentPosition = examinedObject.transform.position;
        float elapsedTime = 0.0f;
        //examinedObject.transform.position = Vector3.Lerp(currentPosition, examinePoint.transform.position, (elapsedTime / lerpTime));
        //elapsedTime += Time.deltaTime;
        isLerping = true;
        while (elapsedTime < lerpTime)
        {
            examinedObject.transform.position = Vector3.Lerp(currentPosition, examinePoint.transform.position, (elapsedTime / lerpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        examinedObject.transform.position = examinePoint.transform.position;
        isLerping = false;
        yield return null;
    }

    IEnumerator LerpObjectBack()
    {

        Vector3 currentPosition = examinedObject.transform.position;
        Quaternion currentRotation = examinedObject.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(storedTransform);
        float elapsedTime = 0.0f;
        isLerping = true;
        while (elapsedTime < lerpTime)
        {
            examinedObject.transform.position = Vector3.Lerp(currentPosition, storedTransform, (elapsedTime / lerpTime));
            examinedObject.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, (elapsedTime / lerpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        examinedObject.transform.position = storedTransform;
        examinedObject.transform.rotation = targetRotation;
        isLerping = false;
        yield return null;
    }

    // Dialogue stuff
    public void EnterDialogue()
    {
        inDialogue = true;
        playerMouseLook.inDialogue = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitDialogue()
    {
        inDialogue = false;
        playerMouseLook.inDialogue = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
