using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    public AudioSource[] sfxAudioSources;
    public Slider slider;

    private void Start()
    {
        // Add a listener to the slider's OnValueChanged event
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // Adjust the volume of all the SFX audio sources based on the slider value
        for (int i = 0; i < sfxAudioSources.Length; i++)
        {
            sfxAudioSources[i].volume = value;
        }
    }
}
