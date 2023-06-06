using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds for the effect
    public float vignetteIntensity = 0f; // Initial intensity of the vignette
    public float heartbeatVolume = 0.05f; // Initial volume of the heartbeat sound (starting value)

    private float initialHeartbeatVolume; // Store the initial volume for reference
    private float timeElapsed = 0f; // Current time elapsed
    private VignetteEffect vignetteEffect; // Reference to the vignette post-processing effect
    private AudioSource heartbeatAudio; // Reference to the audio source playing the heartbeat sound

    // Add the Start() and Update() methods
    void Start()
    {
        // Get the vignette effect and audio source components
        vignetteEffect = Camera.main.GetComponent<VignetteEffect>();
        heartbeatAudio = GetComponent<AudioSource>();

        // Set the initial intensity and volume
        vignetteEffect.intensity = vignetteIntensity;
        initialHeartbeatVolume = heartbeatVolume;
        heartbeatAudio.volume = initialHeartbeatVolume;
    }

    void Update()
    {
        // Update the timer
        timeElapsed += Time.deltaTime;

        // Calculate the progress as a value between 0 and 1
        float progress = Mathf.Clamp01(timeElapsed / totalTime);

        // Update the intensity of the vignette effect
        vignetteEffect.intensity = vignetteIntensity * progress;

        // Update the volume of the heartbeat sound
        heartbeatAudio.volume = initialHeartbeatVolume + (1f - initialHeartbeatVolume) * progress;
    }
}