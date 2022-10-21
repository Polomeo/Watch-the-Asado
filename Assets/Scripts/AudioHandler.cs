using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip gameLoopSongAudio;
    public AudioClip endGameAudio;
    public AudioClip biteSoundFX;
    public AudioClip chowSoundFX;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();     
    }


    public void PlayAudioOnce(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
