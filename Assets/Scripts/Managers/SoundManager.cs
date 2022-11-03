using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;
    AudioSource audioSource;

    private void Awake()
    {
        i = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sfx)
    {
        audioSource.PlayOneShot(sfx);
    }
}
