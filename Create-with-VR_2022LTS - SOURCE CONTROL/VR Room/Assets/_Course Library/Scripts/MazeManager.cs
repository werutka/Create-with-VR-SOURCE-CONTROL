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

    private DataLogger dataLogger;

    public GameObject player;
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
        dataLogger = GetComponent<DataLogger>();
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

        // Investment Room -> First Questionnaire Room
        if (investmentRoom.activeSelf && currentMazeIndex == 0)
        {
            DeactivateAllRooms();
            ActivateRoom(questionnaireRoom);
            return;
        }

        if (questionnaireRoom.activeSelf && currentMazeIndex == 14)
        {
            DeactivateAllRooms();
            ActivateRoom(finalRoom);
            return;
        }

        // Handle Maze Room -> Questionnaire Room sequence
        if (!isQuestionnaireNext && currentMazeIndex <= 12)
        {
            // If we have completed 12 maze-questionnaire cycles, go to the second investment room
            if (currentMazeIndex == 12)
            {
                DeactivateAllRooms();
                ActivateRoom(investmentRoom);
                currentMazeIndex++;
                return;
            }

            // Activate the next maze room
            DeactivateAllRooms();
            ActivateRoom(mazeRooms[mazeRoomOrder[currentMazeIndex]]);
            isQuestionnaireNext = true;
            return;
        }
        else
        {
            // After a maze room, activate the questionnaire
            DeactivateAllRooms();
            ActivateRoom(questionnaireRoom);
            isQuestionnaireNext = false;
            currentMazeIndex++;
            return;
        }        
    }

    private void ActivateRoom(GameObject room)
    {
        if (room != null)
        {
            room.SetActive(true);
            dataLogger.LogRoomEntry(); // Log timestamp when entering a room

            // Reset player position
            player.transform.position = new Vector3(-2.3f, 0f, 0f);

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
        int mazeRoomIndex = currentMazeIndex - 1; // Adjust because `currentMazeIndex` increments after LoadNextRoom()

        if (mazeRoomIndex == 3) // 4th maze room (0-based index)
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
        {
            followedAdvice[currentMazeIndex] = followed;
            AdjustTokens(followed);

            bool playerChoseRight = followed == suggestionSequence[currentMazeIndex]; // Infer choice
            dataLogger.LogMazeChoice(currentMazeIndex, suggestionSequence[currentMazeIndex], playerChoseRight, followed, playerTokens);
        }
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
