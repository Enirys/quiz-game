using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundList
{
    ButtonClick,One,Two,Three,Go,Ready,Win,Lose,TimeUp,HurryUp,FinalRound
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioClip> clips = new List<AudioClip>();

    private AudioSource _audioSource;

    public float timeBetweenSounds;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundList sound)
    {
        int soundIndex = (int)sound;
        _audioSource.clip = clips[soundIndex];
        _audioSource.Play();
    }

    public IEnumerator WaitBetweenSounds()
    {
        yield return new WaitForSeconds(timeBetweenSounds);
    }

    public void PlayStartRoundSound()
    {
        List<SoundList> sounds = new List<SoundList>();
        sounds.Add(SoundList.Ready);
        sounds.Add(SoundList.Three);
        sounds.Add(SoundList.Two);
        sounds.Add(SoundList.One);
        sounds.Add(SoundList.Go);
        foreach (SoundList sound in sounds)
        {
            PlaySound(sound);
            StartCoroutine(WaitBetweenSounds());
        }
    }
}
