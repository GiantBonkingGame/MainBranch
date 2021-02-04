using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider slider = null;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioManager.instance.GetVolume();
    }

    public void VolumeChange(float volume)
    {
        AudioManager.instance.SetVolume(volume);
    }
}
