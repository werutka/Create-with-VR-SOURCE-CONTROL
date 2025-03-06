using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorTrigger : MonoBehaviour
{
    public Transform nextRoomSpawnPoint; // Assign in Inspector
    public GameObject player; // Assign the XR Rig (player)

    private void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnDoorClicked);
    }

    private void OnDoorClicked(SelectEnterEventArgs args)
    {
        if (player != null && nextRoomSpawnPoint != null)
        {
            player.transform.position = nextRoomSpawnPoint.position;
        }
    }
}
