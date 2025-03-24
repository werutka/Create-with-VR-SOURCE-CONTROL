using UnityEngine;

public class DoorSoundTest : MonoBehaviour
{
    // Sound IDs for the sounds we want to play
    public SoundID doorSound = SoundID.DoorOpen;
    public SoundID ambientSound = SoundID.AmbOffice;

    private void Start()
    {
        // Play DoorOpen sound
        AudioManager.Instance.PlaySound(doorSound);
    }
}
