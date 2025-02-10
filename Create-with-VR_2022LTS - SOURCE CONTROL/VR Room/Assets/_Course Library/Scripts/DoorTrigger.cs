using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public int choiceIndex; // 0 for left door, 1 for right door
    private MazeManager mazeManager;

    // Start is called before the first frame update
    void Start()
    {
        mazeManager = FindObjectOfType<MazeManager>(); // Get reference to MazeManager
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers it
        {
            mazeManager.MoveToNextRoom(choiceIndex);
        }
    }
}
