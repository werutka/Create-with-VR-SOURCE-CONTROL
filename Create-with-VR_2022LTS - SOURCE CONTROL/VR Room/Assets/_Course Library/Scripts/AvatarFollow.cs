using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AvatarFollow : MonoBehaviour
{
    public TextMeshProUGUI suggestionText;
    private bool avatarSuggestsRightDoor;

    private void Start()
    {
        avatarSuggestsRightDoor = Random.value > 0.5f;
        suggestionText.text = avatarSuggestsRightDoor ? "Take the Right Door" : "Take the Left Door";
    }

    public void PlayerChoice(bool choseRight)
    {
        bool followed = (choseRight == avatarSuggestsRightDoor);
        MazeManager.Instance.LogDecision(followed);
        MazeManager.Instance.LoadNextRoom();
    }
}
