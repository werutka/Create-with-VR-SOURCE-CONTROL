using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TokenInvestment : MonoBehaviour
{
    public TextMeshProUGUI tokenAmountText;
    public Button minusButton;
    public Button plusButton;
    public Button confirmButton;

    private int tokenAmount = 25; // Default value

    void Start()
    {
        // Update displayed token amount
        UpdateTokenDisplay();

        // Assign button actions
        //minusButton.onClick.AddListener(DecreaseToken);
        //plusButton.onClick.AddListener(IncreaseToken);
        //confirmButton.onClick.AddListener(ConfirmSelection);
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
        tokenAmount++;
        UpdateTokenDisplay();
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
