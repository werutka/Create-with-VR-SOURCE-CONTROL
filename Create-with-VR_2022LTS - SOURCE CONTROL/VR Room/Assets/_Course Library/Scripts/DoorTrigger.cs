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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MazeManager mazeManager = MazeManager.Instance;
            if (mazeManager == null) return;

            int currentRoomIndex = mazeManager.GetCurrentRoomIndex();
            bool avatarSuggestedRight = mazeManager.GetAvatarSuggestionForRoom(currentRoomIndex);

            // Check if the player followed the avatar's suggestion
            bool followedAdvice = (isRightDoor == avatarSuggestedRight);

            // Log the decision and adjust tokens
            mazeManager.LogDecision(followedAdvice);

            // Move to the next room
            //mazeManager.LoadNextRoom();
        }
    }
}
