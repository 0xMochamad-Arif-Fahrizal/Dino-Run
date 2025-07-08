using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        // Mengkonversi volume slider ke dB
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
