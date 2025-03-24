using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;

    // Public List to be visible in the Inspector for easy assignment
    [SerializeField] private List<SoundEventSO> soundEventsList = new List<SoundEventSO>();

    // Dictionary to map SoundID to SoundEventSO
    private Dictionary<SoundID, SoundEventSO> soundEvents = new Dictionary<SoundID, SoundEventSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("AudioManager Initialized.");
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }

        // Register sound events when the game starts (from the list in the Inspector)
        RegisterSoundEvents();
    }

    // Register sound events using the list from the Inspector
    private void RegisterSoundEvents()
    {
        foreach (var soundEvent in soundEventsList)
        {
            if (!soundEvents.ContainsKey(soundEvent.soundID))
            {
                soundEvents.Add(soundEvent.soundID, soundEvent);
                Debug.Log($"Registered Sound Event: {soundEvent.soundID}");
            }
        }
    }

    // Play sound by SoundID
    public void PlaySound(SoundID soundID)
    {
        if (soundEvents.ContainsKey(soundID))
        {
            SoundEventSO soundEvent = soundEvents[soundID];

            audioSource.clip = soundEvent.audioClip;
            audioSource.volume = soundEvent.volume;
            audioSource.pitch = soundEvent.pitch;
            audioSource.loop = soundEvent.loop;

            audioSource.Play();
            Debug.Log($"Playing sound: {soundEvent.audioClip.name}");
        }
        else
        {
            Debug.LogWarning($"Sound with ID {soundID} not found.");
        }
    }

    // Play the specific AmbOffice sound
    public void PlayAmbOfficeSound()
    {
        // Play the sound with the SoundID AmbOffice
        PlaySound(SoundID.AmbOffice);
        Debug.Log("Playing AmbOffice sound.");
    }

    // Stop any currently playing audio
    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Audio stopped.");
        }
        else
        {
            Debug.LogWarning("No audio is currently playing.");
        }
    }


}
