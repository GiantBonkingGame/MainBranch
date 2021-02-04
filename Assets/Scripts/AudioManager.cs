using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource source;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        source = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        source.volume = volume;
    }

    public float GetVolume()
    {
        return source.volume;
    }
}