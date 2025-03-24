using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Event", menuName = "Audio/Sound Event", order = 3)]
public class SoundEventSO : ScriptableObject
{
    public SoundID soundID;         // Unique identifier for this sound event
    public AudioClip audioClip;     // The actual audio clip for this event
    public float volume = 1f;       // Default volume
    public float pitch = 1f;        // Default pitch
    public bool loop = false;       // Should the audio loop?
}
