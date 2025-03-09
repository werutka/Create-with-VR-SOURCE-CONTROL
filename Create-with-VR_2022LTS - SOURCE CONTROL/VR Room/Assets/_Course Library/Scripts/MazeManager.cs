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

    private int[] mazeRoomOrder = new int[12]; // Stores randomized room order
    private int currentMazeIndex = 0;
    private bool[] followedAdvice = new bool[12]; // Tracks if participant followed avatar

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
    }

    void GenerateMazeOrder()
    {
        // Creates a randomized sequence of 12 rooms
        for (int i = 0; i < 12; i++) mazeRoomOrder[i] = i + 1;
        System.Random rng = new System.Random();
        mazeRoomOrder = mazeRoomOrder.OrderBy(x => rng.Next()).ToArray();
    }

    public void LoadNextRoom()
    {
        string nextScene = "";

        if (SceneManager.GetActiveScene().name == "TutorialRoom")
            nextScene = "InvestmentRoom";
        else if (SceneManager.GetActiveScene().name == "InvestmentRoom" && currentMazeIndex == 0)
            nextScene = $"MazeRoom_{mazeRoomOrder[currentMazeIndex]}";
        else if (currentMazeIndex < 12)
            nextScene = $"MazeRoom_{mazeRoomOrder[currentMazeIndex]}";
        else if (currentMazeIndex == 12)
            nextScene = "InvestmentRoom";
        else
            nextScene = "FinalRoom";

        SceneManager.LoadScene(nextScene);
        currentMazeIndex++;
    }

    public void LogDecision(bool followed)
    {
        if (currentMazeIndex < 12)
            followedAdvice[currentMazeIndex] = followed;
    }
}
