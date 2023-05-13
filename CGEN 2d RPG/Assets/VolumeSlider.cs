using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    private Slider slider;

    private void Awake()
    {
        // Get the reference to the Slider component
        slider = GetComponent<Slider>();
    }

    public void OnVolumeChanged()
    {
        // Adjust the volume of the background music based on the slider value
        backgroundMusic.volume = slider.value;
    }
}
