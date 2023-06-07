using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class VignetteEffect : MonoBehaviour
{
    public float intensity = 0.5f; // Intensity of the vignette effect

    private Vignette vignette;

    void Start()
    {
        // Get the Vignette component from the PostProcessVolume
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        // Update the intensity of the vignette effect
        vignette.intensity.value = intensity;
    }
}