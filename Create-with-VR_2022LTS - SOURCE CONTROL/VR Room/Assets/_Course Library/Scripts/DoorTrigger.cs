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
        avatar = FindObjectOfType<AvatarFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            avatar.PlayerChoice(isRightDoor); //AdjustTokens instead of PlayerChoice? Or UpdateTokenDisplay?? from MazeManager
        }
    }
}
