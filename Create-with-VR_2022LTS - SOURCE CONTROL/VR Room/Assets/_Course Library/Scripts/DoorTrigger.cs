using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorTrigger : MonoBehaviour
{
    // Take array from MazeManager with the suggestions for doors
    // Check if the player followed avatar's advice (clicked the correct door)
    // Call AdjustTokens

    public bool isRightDoor;
    private AvatarFollow avatar;

    private void Start()
    {
        // Assign avatar reference
        avatar = FindObjectOfType<AvatarFollow>();

        if (avatar == null)
        {
            Debug.LogWarning($"No AvatarFollow found in {gameObject.scene.name}. This might be a questionnaire room.");
        }
    }

    public void OnDoorSelected(XRBaseInteractor interactor)
    {
        Debug.Log($"Door Selected: {gameObject.name}, IsRightDoor: {isRightDoor}");

        if (avatar != null)
        {
            avatar.PlayerChoice(isRightDoor); // Call PlayerChoice() with correct door info
        }
        else
        {
            Debug.LogError("AvatarFollow script reference is missing!");
        }
    }
}
