using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer myMixer;
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void SetVolume(float value)
    {
        float volume = Mathf.Log10(value) * 40;

        myMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.GetFloat("Volume", volume);
    }
}
