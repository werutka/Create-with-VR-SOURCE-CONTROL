using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AvatarFollow : MonoBehaviour
{
    public TextMeshProUGUI suggestionText;
    public bool avatarSuggestsRightDoor;
    private int roomIndex;

    private void Start()
    {
        roomIndex = MazeManager.Instance.GetCurrentRoomIndex();
        avatarSuggestsRightDoor = MazeManager.Instance.GetAvatarSuggestionForRoom(roomIndex);

        Debug.Log($"Avatar Suggestion for Room {roomIndex}: {avatarSuggestsRightDoor}");

        if (roomIndex == 4) // 5th room (after mistake)
        {
            suggestionText.text = "Oh sorry. I made a mistake. Let's work together to fix it. " +
                                  (avatarSuggestsRightDoor ? "\r\nTake the Right Door" : "\r\nTake the Left Door");
        }
        else
        {
            suggestionText.text = avatarSuggestsRightDoor ? "Take the Right Door" : "Take the Left Door";
        }
    }
}
