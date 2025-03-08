using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public Transform trialRoom;             // Assign in Inspector
    public Transform investmentRoom;        // Assign in Inspector
    public Transform finalRoom;             // Assign in Inspector
    public List<Transform> randomRooms;     // Assign all potential randomized rooms in Inspector
    public GameObject player;               // Assign XR Rig

    private List<Transform> roomSequence = new List<Transform>();
    private int currentRoomIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoomSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRoomSequence()
    {
        // Ensure the correct room order
        roomSequence.Add(trialRoom); // 1st: Trial Room
        roomSequence.Add(investmentRoom); // 2nd: First Investment Room

        // Select 12 unique random rooms (excluding trial and investment rooms)
        List<Transform> shuffledRooms = new List<Transform>(randomRooms);
        ShuffleList(shuffledRooms);
        for (int i = 0; i < 12; i++)
        {
            roomSequence.Add(shuffledRooms[i]);
        }

        roomSequence.Add(investmentRoom); // 15th: Second Investment Room (same as first)
        roomSequence.Add(finalRoom); // 16th: Final Room
    }

    public void MoveToNextRoom(int choice)
    {
        if (currentRoomIndex < roomSequence.Count - 1)
        {
            currentRoomIndex++;
            player.transform.position = roomSequence[currentRoomIndex].position;
        }
    }

    void ShuffleList(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Transform temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
