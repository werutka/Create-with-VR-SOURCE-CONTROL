using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    private void Start()
    {
        // Play the AmbOffice sound when the scene starts
        AudioManager.Instance.PlayAmbOfficeSound();
    }
}
