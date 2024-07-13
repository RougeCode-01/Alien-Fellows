using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microlight.MicroAudio;

public class CallMicroBGM : MonoBehaviour
{
    [SerializeField] AudioClip _bgmLoop;
    // Start is called before the first frame update
    void Start()
    {
        MicroAudio.PlayOneTrack(_bgmLoop, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
