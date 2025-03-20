using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class QuestionnaireLogger : MonoBehaviour
{
    public ToggleGroup question1Group;
    public ToggleGroup question2Group;

    public Button submitButton;

    private DataLogger dataLogger;

    // Start is called before the first frame update
    void Start()
    {
        dataLogger = FindObjectOfType<DataLogger>();

        submitButton.onClick.AddListener(LogQuestionnaireAnswers);
    }

    void LogQuestionnaireAnswers()
    {
        string answer1 = GetSelectedToggle(question1Group);
        string answer2 = GetSelectedToggle(question2Group);

        if (MazeManager.Instance != null)
        {
            int roomIndex = MazeManager.Instance.GetCurrentRoomIndex();
            string answers = $"{answer1},{answer2}";
            MazeManager.Instance.GetComponent<DataLogger>().LogQuestionnaire(roomIndex, answers);
        }

        Debug.Log($"Questionnaire Answers Logged: {answer1}, {answer2}");
    }

    string GetSelectedToggle(ToggleGroup group)
    {
        Toggle selected = group.ActiveToggles().FirstOrDefault();
        return selected ? selected.GetComponentInChildren<TextMeshProUGUI>().text : "No Answer";
    }
}
