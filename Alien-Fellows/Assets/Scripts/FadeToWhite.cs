using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToWhite : MonoBehaviour
{
    public Image fadeImage; // Reference to the Image component for the fade effect
    public float fadeDuration = 2.0f; // The duration of the fade effect in seconds

    void Start()
    {
        // Initialize the image as fully opaque white
        fadeImage.color = new Color(255, 255, 255, 0);
    }

    public void BeginFadeIn()
    {
        // Start the fade in effect
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = new Color(255, 255, 255, 0); // Start as transparent white
        Color endColor = new Color(255, 255, 255, 1); // End as white

        Debug.Log("FadeIn started. Start color: " + startColor.ToString());

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            Debug.Log("Fading... Current color: " + fadeImage.color.ToString());
            yield return null;
        }

        fadeImage.color = endColor; // Ensure it matches the target color
        Debug.Log("FadeIn completed. End color: " + endColor.ToString());
    }
}