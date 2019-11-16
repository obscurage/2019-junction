using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource orangeAudioSource;
    public AudioSource purpleAudioSource;
    public AudioSource generalAudioSource;

    public AudioClip changeToOrangeAudio;
    public AudioClip changeToPurpleAudio;

    void Awake()
    {
        if (instance == null)
        { instance = this; }
        else if (instance != this)
        { Destroy(gameObject); }

        orangeAudioSource.volume = 0f;
        purpleAudioSource.volume = 0.5f;

        orangeAudioSource.Play();
        purpleAudioSource.Play();
    }

    public void ModeChange(ViewMode mode)
    {
        generalAudioSource.PlayOneShot(changeToOrangeAudio);
        // fade out playing track and fade in current mode track
        switch (mode)
        {
            case ViewMode.orange:
                StartCoroutine(AudioManager.FadeOut(purpleAudioSource, 0.5f));
                StartCoroutine(AudioManager.FadeIn(orangeAudioSource, 0.5f));
                generalAudioSource.PlayOneShot(changeToOrangeAudio);
                break;
            case ViewMode.purple:
                StartCoroutine(AudioManager.FadeOut(orangeAudioSource, 0.5f));
                StartCoroutine(AudioManager.FadeIn(purpleAudioSource, 0.5f));
                generalAudioSource.PlayOneShot(changeToPurpleAudio);
                break;
        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.volume = 0f;
        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}