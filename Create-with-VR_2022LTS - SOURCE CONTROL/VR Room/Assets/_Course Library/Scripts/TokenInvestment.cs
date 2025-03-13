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

    void Start()
    {
        int availableTokens = GameObject.Find("Maze Manager").GetComponent<MazeManager>().playerTokens;

        instructionText.text = $"You've got {availableTokens} tokens! " +
            "\r\nPlease choose the number of tokens you want to invest to the avatar. " +
            "\r\nThe number of tokens will be multiplied by four while being transferred. " +
            "\r\nAfterwards, the avatar will decide to return to you half of the tokens or none.";

        // Update displayed token amount
        UpdateTokenDisplay();
    }

    public void DecreaseToken()
    {
        if (tokenAmount > 0) // Ensure it doesn't go negative
        {
            tokenAmount--;
            UpdateTokenDisplay();
        }
    }

    public void IncreaseToken()
    {
        if (tokenAmount < 51) // Ensure it doesn't go above the tokens that the player has
        {
            tokenAmount++;
            UpdateTokenDisplay();
        }
    }

    void UpdateTokenDisplay()
    {
        tokenAmountText.text = tokenAmount.ToString();
    }

    public void ConfirmSelection()
    {
        Debug.Log("Tokens Invested: " + tokenAmount);
        // Here you can add logic to store the tokenAmount for the avatar.

        // Disable the buttons after confirming
        //minusButton.interactable = false;
        //plusButton.interactable = false;
        //confirmButton.interactable = false;
    }
}
