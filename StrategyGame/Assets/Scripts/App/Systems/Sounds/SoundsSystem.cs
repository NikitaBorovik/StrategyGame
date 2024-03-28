using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundsSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI volumeText;
    private void Awake()
    {
        if(PlayerPrefs.HasKey("Volume"))
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        else
            AudioListener.volume = 0.1f;
        volumeText.text = (AudioListener.volume * 10).ToString("0"); ;
    }
    public void VolumeUp()
    {
        var lastVolume = AudioListener.volume;
        if (AudioListener.volume < 1f)
            AudioListener.volume += 0.1f;
        if (AudioListener.volume > 1f)
            AudioListener.volume = 1f;
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        volumeText.text = (AudioListener.volume * 10).ToString("0"); ;
    }
    public void VolumeDown()
    {
        var lastVolume = AudioListener.volume;
        if (AudioListener.volume > 0f)
            AudioListener.volume -= 0.1f;
        if (AudioListener.volume < 0f)
            AudioListener.volume = 0f;
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        volumeText.text = (AudioListener.volume * 10).ToString("0"); ;
    }
}
