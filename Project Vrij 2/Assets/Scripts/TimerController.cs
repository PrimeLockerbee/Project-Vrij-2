using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class TimerController : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds for the effect

    public float heartbeatVolume = 0.05f; // Initial volume of the heartbeat sound (starting value)
    public float textDisplayInterval = 5f; // Time interval between text displays

    public TextMeshProUGUI textDisplay; // Reference to the TextMeshProUGUI component
    public List<string> textsToDisplay; // Text options to be displayed

    public GameObject functionTarget; // Reference to the GameObject to call the function on
    public string functionName = "FunctionToCall"; // Name of the function to call on the target GameObject

    public PostProcessVolume postProcessVolume; // Reference to the PostProcessVolume component
    public AudioSource heartbeatAudio; // Reference to the heartbeat audio source

    private float initialHeartbeatVolume; // Store the initial volume for reference
    private float timeElapsed = 0f; // Current time elapsed
    private float textDisplayTimer = 0f; // Timer for text display
    private int currentTextIndex = 0; // Index of the current text being displayed

    private GameObject parentObject; // Reference to the parent GameObject

    private bool isTextDisplaying = true; // Flag to track if text is currently being displayed

    private Vignette vignetteEffect; // Reference to the vignette post-processing effect
    public float vignetteIntensity = 0.1f; // Initial intensity of the vignette
    public float vignetteMaxIntensity = 0.6f; // Maximum intensity of the vignette

    void Start()
    {
        initialHeartbeatVolume = heartbeatVolume;
        heartbeatAudio.volume = initialHeartbeatVolume;

        parentObject = textDisplay.transform.parent.gameObject;

        // Get the vignette effect from the PostProcessVolume
        vignetteEffect = postProcessVolume.profile.GetSetting<Vignette>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        textDisplayTimer += Time.deltaTime;

        float progress = Mathf.Clamp01(timeElapsed / totalTime);

        // Update the vignette intensity
        vignetteEffect.intensity.value = Mathf.Lerp(vignetteIntensity, vignetteMaxIntensity, progress);

        // Check if it's time to display text
        if (isTextDisplaying && textDisplayTimer >= textDisplayInterval)
        {
            textDisplay.gameObject.SetActive(true);
            textDisplay.text = GetNextTextToDisplay();

            if (currentTextIndex >= textsToDisplay.Count)
            {
                // All texts have been displayed
                isTextDisplaying = false;

                // Call the specified function on the target GameObject
                if (functionTarget != null)
                {
                    functionTarget.SendMessage(functionName);
                }
            }
            else
            {
                StartCoroutine(HideTextAfterDelay(3f)); // Hide the text after a delay (3 seconds in this example)
            }

            textDisplayTimer = 0f; // Reset the text display timer

            // Activate the parent GameObject
            parentObject.SetActive(true);
        }

        // Update heartbeat volume
        heartbeatAudio.volume = initialHeartbeatVolume + (1f - initialHeartbeatVolume) * progress;
    }

    private string GetNextTextToDisplay()
    {
        if (currentTextIndex >= textsToDisplay.Count)
        {
            return "";
        }

        string text = textsToDisplay[currentTextIndex];
        currentTextIndex++;

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