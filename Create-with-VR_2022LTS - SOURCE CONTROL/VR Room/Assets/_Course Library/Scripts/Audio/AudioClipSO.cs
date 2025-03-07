using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Clip", menuName = "Audio/Audio Clip")]
public class AudioClipSO : ScriptableObject
{
    public AudioClip audioClip;   // The actual audio clip
    public string clipName;       // Name for easy reference
    public bool loop;             // Whether the audio should loop
    public float volume = 1f;     // Volume level for the audio clip
    public float pitch = 1f;      // Pitch level for the audio clip
}
