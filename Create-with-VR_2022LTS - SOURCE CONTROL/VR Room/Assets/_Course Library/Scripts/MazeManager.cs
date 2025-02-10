using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public GameObject[] rooms; // Assign rooms in order
    private int currentRoomIndex = 0;
    private int maxChoices = 12;

    // Start is called before the first frame update
    void Start()
    {
        ShowRoom(0); // Start at the first room
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToNextRoom(int choice)
    {
        if (currentRoomIndex >= maxChoices - 1)
        {
            Debug.Log("End of the experiment!");
            return;
        }

        // Compute next room index based on choice
        currentRoomIndex = (currentRoomIndex * 2) + 1 + choice;

        if (currentRoomIndex < rooms.Length)
        {
            ShowRoom(currentRoomIndex);
        }
        else
        {
            Debug.LogError("Room index out of bounds!");
        }
    }

    private void ShowRoom(int index)
    {
        // Disable all rooms first
        foreach (var room in rooms)
        {
            room.SetActive(false);
        }

        // Enable the current room
        rooms[index].SetActive(true);
    }
}
