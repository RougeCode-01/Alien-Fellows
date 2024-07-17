using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
   [Header("Block Settings")]
   [SerializeField] private GameObject light; // Reference to the GameObject that contains the light.
   [Header("Keypad Settings")]
   [SerializeField] private TextMeshProUGUI pinNumberText; // Reference to the TextMeshProUGUI component for displaying the pin number.
   [SerializeField] private string pinNumber; // The correct pin number to unlock the door.
   [Header("Keypad UI")]
   [SerializeField] private GameObject keypadUI; // Reference to the GameObject that contains the keypad UI.
   [Header("Keypad Sound")]
   [SerializeField] private AudioSource keypadSound; // Reference to the AudioSource component for playing the keypad sound.
   [SerializeField] private AudioSource correctPinSound; // Reference to the AudioSource component for playing the correct pin sound.
   [SerializeField] private AudioSource wrongPinSound; // Reference to the AudioSource component for playing the wrong pin sound.

   private void Awake()
   {
      //Make sure the keypad UI is deactivated
      keypadUI.SetActive(false);
      //Make sure the light is deactivated
      light.SetActive(false);
      //Set the pin number text to be blank
      pinNumberText.text = "";
   }

   private void Update()
   {
      //Check if the player has pressed the correct pin number
      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
         pinNumberText.text += "1";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha2))
      {
         pinNumberText.text += "2";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha3))
      {
         pinNumberText.text += "3";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha4))
      {
         pinNumberText.text += "4";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha5))
      {
         pinNumberText.text += "5";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha6))
      {
         pinNumberText.text += "6";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha7))
      {
         pinNumberText.text += "7";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha8))
      {
         pinNumberText.text += "8";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha9))
      {
         pinNumberText.text += "9";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Alpha0))
      {
         pinNumberText.text += "0";
         keypadSound.Play();
      }
      else if (Input.GetKeyDown(KeyCode.Backspace))
      {
         pinNumberText.text = "";
         keypadSound.Play();
      }

      //Check if the player has entered the correct pin number
      if (pinNumberText.text == pinNumber)
      {
         //Play the correct pin sound
         correctPinSound.Play();
         //Deactivate the keypad ui
         keypadUI.SetActive(false);
         //Reset the pin number text
         pinNumberText.text = "";
         //Make the block fly up
         MoveBlock();
      }
      
      if(pinNumberText.text.Length > 4)
      {
         pinNumberText.text = "";
         wrongPinSound.Play();
      }
   }

   private void OnCollisionEnter(Collision other)
   {
      //Activate the keypad ui
      keypadUI.SetActive(true);
   }

   private void OnCollisionExit(Collision other)
   {
      //Deactivate the keypad ui
      keypadUI.SetActive(false);
   }
   
   // private void LoadEndingScene()
   // {
   //    //SceneManager.LoadScene("EndingScene");
   //    Debug.Log("Ending Scene Loaded");
   // }
   
   void MoveBlock()
   {
      StartCoroutine(MoveBlockSmoothly());
   }

// Coroutine for smooth movement
   IEnumerator MoveBlockSmoothly()
   {
      Vector3 startPosition = transform.position;
      Vector3 endPosition = startPosition + new Vector3(0, 16, 0); // Adjust the Y value as needed
      float duration = 2.0f; // Duration of the movement in seconds
      float elapsedTime = 0;

      while (elapsedTime < duration)
      {
         transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / duration));
         elapsedTime += Time.deltaTime;
         yield return null;
      }

      transform.position = endPosition; // Ensure the block reaches the final position
      light.SetActive(true); // Activate the light
   }
}
