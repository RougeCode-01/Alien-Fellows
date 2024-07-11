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
    private Transform storedTransform; //store the examined object's original transform
    public GameObject examinePoint; //where do we place the object we're examining?
    public float lerpTime = 1.0f; // how long does it take to move the obect from it's origin to the examine point?


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //grab the mouse on start
    }

    private void Update()
    {

        if (isInteracting)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && !hit.collider.CompareTag("Interactable")) // cast a ray from the player's camera then check if it's something we can look at
                {
                    ExitInteract();
                }
                else
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

        //Interacty
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Interactable")) // cast a ray from the player's camera then check if it's something we can look at
            {
                examinedObject = hit.collider.gameObject;
                storedTransform = examinedObject.transform;
                EnterInteract();
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

    //Lerp Coroutunes

    IEnumerator LerpObjectToPlayer() //we'll move the object to our examine location with this
    {
        Vector3 currentPosition = examinedObject.transform.position;
        float elapsedTime = 0.0f;
        //examinedObject.transform.position = Vector3.Lerp(currentPosition, examinePoint.transform.position, (elapsedTime / lerpTime));
        //elapsedTime += Time.deltaTime;

        while (elapsedTime < lerpTime)
        {
            examinedObject.transform.position = Vector3.Lerp(currentPosition, examinePoint.transform.position, (elapsedTime / lerpTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        examinedObject.transform.position = examinePoint.transform.position;
        yield return null;
    }

    IEnumerator LerpObjectBack()
    {

        Vector3 currentPosition = examinedObject.transform.position;
        float elapsedTime = 0.0f;
        while (elapsedTime < lerpTime)
        {
            examinedObject.transform.position = Vector3.Lerp(currentPosition, storedTransform.position, (elapsedTime / lerpTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        examinedObject.transform.position = storedTransform.position;
        yield return null;
    }
}
