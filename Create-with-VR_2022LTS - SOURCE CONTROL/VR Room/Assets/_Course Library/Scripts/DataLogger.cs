using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    private string filePath;
    private float roomEntryTime; // Track when player enters a room

    private void Start()
    {
        // Set up file path
        filePath = Application.persistentDataPath + "/MazeData.csv";

        // If the file doesn't exist, create it with a header row
        if (!File.Exists(filePath))
        {
            string header = "RoomIndex,RoomType,TokensInvested,TotalTokens,AvatarSuggestion,PlayerChoice,FollowedAdvice,TimeSpent,QuestionnaireAnswers";
            File.WriteAllText(filePath, header + "\n");
        }
    }

    public void LogRoomEntry()
    {
        roomEntryTime = Time.time; // Store timestamp when entering a room
    }

    public void LogInvestment(int roomIndex, int tokensInvested, int totalTokens)
    {
        string data = $"{roomIndex},Investment,{tokensInvested},{totalTokens},,,,{Time.time - roomEntryTime},";
        AppendToFile(data);
    }

    public void LogMazeChoice(int roomIndex, bool avatarSuggestsRight, bool playerChoseRight, bool followedAdvice, int totalTokens)
    {
        string data = $"{roomIndex},Maze,,{totalTokens},{avatarSuggestsRight},{playerChoseRight},{followedAdvice},{Time.time - roomEntryTime},";
        AppendToFile(data);
    }

    public void LogQuestionnaire(int roomIndex, string answers)
    {
        string data = $"{roomIndex},Questionnaire,,,,,,,{answers}";
        AppendToFile(data);
    }

    private void AppendToFile(string data)
    {
        File.AppendAllText(filePath, data + "\n");
    }
}
