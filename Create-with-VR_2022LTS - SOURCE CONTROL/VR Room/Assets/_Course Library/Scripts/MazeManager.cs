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
    public GameObject[] mazeRooms;

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
    }

    public void LoadNextRoom()
    {
        DeactivateAllRooms();

        if (tutorialRoom.activeSelf)
        {
            ActivateRoom(investmentRoom);
            return;
        }

        if (investmentRoom.activeSelf && currentMazeIndex == 0)
        {
            ActivateRoom(mazeRooms[mazeRoomOrder[currentMazeIndex]]);
            return;
        }

        if (currentMazeIndex < 11)
        {
            currentMazeIndex++;
            ActivateRoom(mazeRooms[mazeRoomOrder[currentMazeIndex]]);
            return;
        }

        if (currentMazeIndex == 11)
        {
            ActivateRoom(investmentRoom);
            currentMazeIndex++;
            return;
        }

        if (investmentRoom.activeSelf)
        {
            ActivateRoom(finalRoom);
        }
    }

    private void ActivateRoom(GameObject room)
    {
        if (room != null)
        {
            room.SetActive(true);
        }
    }

    private void DeactivateAllRooms()
    {
        tutorialRoom.SetActive(false);
        investmentRoom.SetActive(false);
        finalRoom.SetActive(false);
        foreach (GameObject room in mazeRooms)
        {
            room.SetActive(false);
        }
    }

    public void LogDecision(bool followed)
    {
        if (currentMazeIndex < 12)
            followedAdvice[currentMazeIndex] = followed;
    }
}
