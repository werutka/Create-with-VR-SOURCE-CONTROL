using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TokenInvestment : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI tokenAmountText;
    public Button minusButton;
    public Button plusButton;
    public Button confirmButton;

    private int tokenAmount = 25; // Default value

    private MazeManager mazeManager;

    void Start()
    {
        mazeManager = GameObject.Find("Maze Manager").GetComponent<MazeManager>();

        int availableTokens = mazeManager.playerTokens;

        instructionText.text = $"You've got {availableTokens} tokens! " +
            "\r\nPlease choose the number of tokens you want to invest to the avatar. " +
            "\r\nThe number of tokens will be multiplied by four while being transferred. " +
            "\r\nAfterwards, the avatar will decide to return to you half of the tokens or none.";

        // Update displayed token amount
        UpdateTokens();
    }

    public void DecreaseToken()
    {
        if (tokenAmount > 0) // Ensure it doesn't go negative
        {
            tokenAmount--;
            UpdateTokens();
        }
    }

    public void IncreaseToken()
    {
        int availableTokens = mazeManager.playerTokens;

        if (tokenAmount < availableTokens) // Ensure it doesn't go above the tokens that the player has
        {
            tokenAmount++;
            UpdateTokens();
        }
    }

    void UpdateTokens()
    {
        tokenAmountText.text = tokenAmount.ToString();
    }

    public void ConfirmSelection()
    {
        int totalTokens = mazeManager.playerTokens;
        mazeManager.GetComponent<DataLogger>().LogInvestment(MazeManager.Instance.GetCurrentRoomIndex(), tokenAmount, totalTokens);

        Debug.Log("Tokens Invested: " + tokenAmount);

        //Update Token Display: decrease it with the number of invested tokens
        mazeManager.playerTokens -= tokenAmount;
        
        mazeManager.UpdateTokenDisplay();

        // Disable the buttons after confirming
        //minusButton.interactable = false;
        //plusButton.interactable = false;
        //confirmButton.interactable = false;
    }
}
