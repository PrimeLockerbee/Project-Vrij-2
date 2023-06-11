using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds for the effect
    public float vignetteIntensity = 0f; // Initial intensity of the vignette
    public float heartbeatVolume = 0.05f; // Initial volume of the heartbeat sound (starting value)
    public float textDisplayInterval = 5f; // Time interval between text displays

    public TextMeshProUGUI textDisplay; // Reference to the TextMeshProUGUI component
    public List<string> textsToDisplay; // Text options to be displayed

    private float initialHeartbeatVolume; // Store the initial volume for reference
    private float timeElapsed = 0f; // Current time elapsed
    private float textDisplayTimer = 0f; // Timer for text display
    private int currentTextIndex = 0; // Index of the current text being displayed

    private AudioSource heartbeatAudio; // Reference to the audio source playing the heartbeat sound
    private GameObject parentObject; // Reference to the parent GameObject

    void Start()
    {
        heartbeatAudio = GetComponent<AudioSource>();

        initialHeartbeatVolume = heartbeatVolume;
        heartbeatAudio.volume = initialHeartbeatVolume;

        parentObject = textDisplay.transform.parent.gameObject;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        textDisplayTimer += Time.deltaTime;

        float progress = Mathf.Clamp01(timeElapsed / totalTime);

        // Check if it's time to display text
        if (textDisplayTimer >= textDisplayInterval)
        {
            textDisplay.gameObject.SetActive(true);
            textDisplay.text = GetNextTextToDisplay();
            StartCoroutine(HideTextAfterDelay(3f)); // Hide the text after a delay (3 seconds in this example)

            textDisplayTimer = 0f; // Reset the text display timer

            // Activate the parent GameObject
            parentObject.SetActive(true);
        }

        // Update other effects (vignette, heartbeat volume)...
    }

    private string GetNextTextToDisplay()
    {
        if (textsToDisplay.Count == 0)
        {
            return "";
        }

        string text = textsToDisplay[currentTextIndex];
        currentTextIndex = (currentTextIndex + 1) % textsToDisplay.Count;

        return text;
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textDisplay.gameObject.SetActive(false);

        // Deactivate the parent GameObject
        parentObject.SetActive(false);
    }
}