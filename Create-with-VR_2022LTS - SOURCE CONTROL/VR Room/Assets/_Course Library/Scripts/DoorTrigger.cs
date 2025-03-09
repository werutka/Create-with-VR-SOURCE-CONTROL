using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorTrigger : MonoBehaviour
{
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
            avatar.PlayerChoice(isRightDoor);
        }
    }
}
