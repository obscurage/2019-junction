using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : ModeChanger
{
    public AudioSource audioSource;
    [SerializeField] private List<AudioModePair> modeAudio = new List<AudioModePair>();

    public override void ModeChange(ViewMode mode)
    {
        foreach (AudioModePair cm in modeAudio)
        {
            if (cm.Mode == mode)
            { audioSource.clip = cm.Audio; }
        }
    }
}

[System.Serializable]
public class AudioModePair
{
    [SerializeField] private AudioClip audio;
    [SerializeField] private ViewMode mode;

    public AudioClip Audio { get => audio; }
    public ViewMode Mode { get => mode; }
}