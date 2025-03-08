using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeManager : MonoBehaviour
{
    public Transform trialRoom;             // Assign in Inspector
    public Transform investmentRoom;        // Assign in Inspector
    public Transform finalRoom;             // Assign in Inspector
    public List<Transform> randomRooms;     // Assign all potential randomized rooms in Inspector
    public GameObject player;               // Assign XR Rig
    public TMP_Text tokenText;                  // Assign in Inspector

    private List<Transform> roomSequence = new List<Transform>();
    private int currentRoomIndex = 0;
    private int tokens = 0;

    // Predefined sequence of avatar door suggestions (1 = Right, 0 = Left)
    private int[] avatarDoorSequence = { 1, 1, 0, 0, 0, 1, 0, 0, 1, 1, 0 };

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoomSequence();
        UpdateTokenUI();
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
            ApplyTokenRules(choice);
            currentRoomIndex++;
            player.transform.position = roomSequence[currentRoomIndex].position;
        }
    }

    void ApplyTokenRules(int playerChoice)
    {
        int suggestedChoice = avatarDoorSequence[currentRoomIndex]; // Get correct suggestion

        bool followedSuggestion = (playerChoice == suggestedChoice);

        if (currentRoomIndex == 3) // 4th room (index 3)
        {
            tokens += followedSuggestion ? -10 : +5;
        }
        else
        {
            tokens += followedSuggestion ? +5 : -10;
        }

        UpdateTokenUI();
    }

    void UpdateTokenUI()
    {
        tokenText.text = "Tokens: " + tokens;
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
