using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class MazeManager : MonoBehaviour
{
    public static MazeManager Instance;

    public GameObject tutorialRoom;
    public GameObject investmentRoom;
    public GameObject finalRoom;
    public GameObject questionnaireRoom;
    public GameObject[] mazeRooms;
    public TextMeshProUGUI tokenText;

    private int[] mazeRoomOrder = new int[12]; // Stores randomized room order
    private int currentMazeIndex = 0;
    private bool[] followedAdvice = new bool[12]; // Tracks if participant followed avatar
    private bool isQuestionnaireNext = false; // Tracks whether next room should be questionnaire
    private bool[] suggestionSequence = new bool[12]; // Stores randomized door suggestions
    public int playerTokens = 50;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GenerateMazeOrder();
        ActivateRoom(tutorialRoom);
    }

    void GenerateMazeOrder()
    {
        // Creates a randomized sequence of 12 rooms
        for (int i = 0; i < 12; i++)
        {
            mazeRoomOrder[i] = i;
        }
        System.Random rng = new System.Random();
        mazeRoomOrder = mazeRoomOrder.OrderBy(x => rng.Next()).ToArray();

        // Randomize door suggestion sequence
        for (int i = 0; i < 12; i++)
        {
            suggestionSequence[i] = rng.Next(2) == 1; // Randomly set true (Right) or false (Left)
        }
    }

    public void LoadNextRoom()
    {
        // Tutorial room -> Investment room
        if (tutorialRoom.activeSelf)
        {
            DeactivateAllRooms();
            ActivateRoom(investmentRoom);
            return;
        }

        if (isQuestionnaireNext)
        {
            // If we were supposed to go to a questionnaire, now go to the next real room
            isQuestionnaireNext = false;

            if (currentMazeIndex == 12)
            {
                DeactivateAllRooms();
                ActivateRoom(finalRoom);
                return;
            }

            if (investmentRoom.activeSelf && currentMazeIndex == 0)
            {
                DeactivateAllRooms();
                ActivateRoom(mazeRooms[mazeRoomOrder[currentMazeIndex]]);
                return;
            }

            else if (currentMazeIndex < 11)
            {
                DeactivateAllRooms();
                currentMazeIndex++;
                ActivateRoom(mazeRooms[mazeRoomOrder[currentMazeIndex]]);
                return;
            }

            else
            {
                DeactivateAllRooms();
                ActivateRoom(investmentRoom);
                currentMazeIndex++;
                return;
            }
        }

        // If not a questionnaire, go to the questionnaire next
        if (investmentRoom.activeSelf || mazeRooms[mazeRoomOrder[currentMazeIndex]].activeSelf)
        {
            isQuestionnaireNext = true;
            DeactivateAllRooms();
            ActivateRoom(questionnaireRoom);
            return;
        }

        if (investmentRoom.activeSelf)
        {
            DeactivateAllRooms();
            ActivateRoom(finalRoom);
        }
    }

    private void ActivateRoom(GameObject room)
    {
        if (room != null)
        {
            room.SetActive(true);

            // Reset canvas when the questionnaire room is activated
            if (room == questionnaireRoom)
            {
                ResetQuestionnaireCanvas();
            }
        }
    }

    private void DeactivateAllRooms()
    {
        tutorialRoom.SetActive(false);
        investmentRoom.SetActive(false);
        finalRoom.SetActive(false);
        questionnaireRoom.SetActive(false);
        foreach (GameObject room in mazeRooms)
        {
            room.SetActive(false);
        }
    }

    private void ResetQuestionnaireCanvas()
    {
        Canvas questionnaireCanvas = questionnaireRoom.GetComponentInChildren<Canvas>(true);
        if (questionnaireCanvas != null)
        {
            questionnaireCanvas.gameObject.SetActive(true);
        }
    }

    public void AdjustTokens(bool followed)
    {
        if (currentMazeIndex == 3) // 4th maze room (0-based index)
        {
            playerTokens += followed ? -10 : 5;
        }
        else
        {
            playerTokens += followed ? 5 : -10;
        }
        UpdateTokenDisplay();
    }

    private void UpdateTokenDisplay()
    {
        tokenText.text = "Tokens: " + playerTokens;
    }

    public void LogDecision(bool followed)
    {
        if (currentMazeIndex < 12)
            followedAdvice[currentMazeIndex] = followed;
            AdjustTokens(followed);
    }

    public int GetCurrentRoomIndex()
    {
        return currentMazeIndex;
    }

    public bool GetAvatarSuggestionForRoom(int roomIndex)
    {
        return suggestionSequence[roomIndex]; // Return the suggestion for this room in the randomized sequence
    }
}
