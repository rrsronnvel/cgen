using UnityEngine;
using UnityEngine.UI;

public class SoundsX : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource backgroundMusic;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        backgroundMusic.volume = volumeSlider.value;
    }

    public void SetVolume()
    {
        backgroundMusic.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }
}
