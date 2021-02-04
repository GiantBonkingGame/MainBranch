using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip humansSmash = null;
    [SerializeField] private AudioClip emptySmash = null;

    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private AudioSource sfxSource = null;

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

        //source = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        sfxSource.volume = volume;
    }

    public float GetVolume()
    {
        return musicSource.volume;
    }

    public void Smash(bool hit)
    {
        if (hit)
            sfxSource.PlayOneShot(humansSmash);
        else sfxSource.PlayOneShot(emptySmash);
    }
}