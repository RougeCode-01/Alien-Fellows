using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    Quaternion doorRotation;

    Quaternion targetRotation;
    float rotateTime = 1.0f;


    public void OpenEvent()
    {
        doorRotation = transform.rotation;
        targetRotation = new Quaternion(transform.rotation.x, .7f, transform.rotation.z, transform.rotation.w);
        StartCoroutine(Open());
    }

    public void CloseEvent()
    {
        doorRotation = transform.rotation;
        targetRotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        StartCoroutine(Open());
    }

    IEnumerator Open()
    {
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime / rotateTime;
            doorRotation = Quaternion.Lerp(doorRotation, targetRotation, (i / rotateTime));
            transform.rotation = doorRotation;
            yield return null;
        }
        transform.rotation = targetRotation;
        yield return null;
    }
}
