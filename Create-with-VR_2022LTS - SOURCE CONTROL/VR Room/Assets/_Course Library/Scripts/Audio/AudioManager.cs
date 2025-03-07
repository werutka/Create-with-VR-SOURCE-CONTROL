using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  // Singleton reference

    [Header("Audio Clip References")]
    public List<AudioClipSO> audioClips;  // List to store Scriptable Objects

    private AudioSource audioSource;       // Reference to the AudioSource component

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();  // Get AudioSource component attached to the same GameObject
    }

    // Play a specific audio clip
    public void PlayAudio(string clipName)
    {
        AudioClipSO clipSO = audioClips.Find(clip => clip.clipName == clipName);

        if (clipSO != null)
        {
            audioSource.clip = clipSO.audioClip;
            audioSource.loop = clipSO.loop;
            audioSource.volume = clipSO.volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"AudioClip with name {clipName} not found!");
        }
    }

    // Stop the current audio
    public void StopAudio()
    {
        audioSource.Stop();
    }

    // Optionally, you can add a fade in/out or other audio effects later
}
