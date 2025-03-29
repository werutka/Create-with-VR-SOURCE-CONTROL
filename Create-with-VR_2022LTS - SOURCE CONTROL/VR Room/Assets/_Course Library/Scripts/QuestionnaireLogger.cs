using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestionnaireLogger : MonoBehaviour
{
    public ToggleGroup faithAnswers;
    public ToggleGroup trustAnswers;

    private DataLogger dataLogger;

    // Start is called before the first frame update
    void Start()
    {
        dataLogger = FindObjectOfType<DataLogger>();
    }

    public void LogQuestionnaireAnswers()
    {
        string answer1 = GetSelectedToggle(faithAnswers);
        string answer2 = GetSelectedToggle(trustAnswers);

        if (MazeManager.Instance != null)
        {
            int roomIndex = MazeManager.Instance.GetCurrentRoomIndex();
            string answers = $"{answer1},{answer2}";
            MazeManager.Instance.GetComponent<DataLogger>().LogQuestionnaire(roomIndex, answers);
        }

        Debug.Log($"Questionnaire Answers Logged: {answer1}, {answer2}");
    }

    string GetSelectedToggle(ToggleGroup toggleGroup)
    {
        var group = toggleGroup.ActiveToggles();

        foreach (var toggle in group)
        {
            if (toggle.isOn)
            {
                TextMeshProUGUI textComponent = toggle.GetComponentInChildren<TextMeshProUGUI>();
                return textComponent != null ? textComponent.text : "No Text Found";
            }
        }
        return "No Answer";
        
        //Toggle selectedToggle = group.GetActive();
        //if (selectedToggle != null)
        {
            //TextMeshProUGUI textComponent = selectedToggle.GetComponentInChildren<TextMeshProUGUI>();
            //return textComponent != null ? textComponent.text : "No Answer";
        }
        //return "No Answer";
    }
}