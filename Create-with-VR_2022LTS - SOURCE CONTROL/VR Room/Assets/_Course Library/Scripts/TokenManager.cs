using TMPro;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    public static TokenManager Instance; // Singleton instance

    public TextMeshProUGUI tokenText; // Reference to UI text
    private int playerTokens = 0; // Player's token count

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
    }

    private void Start()
    {
        UpdateTokenDisplay();
    }

    public void AdjustTokens(int amount)
    {
        playerTokens += amount;
        UpdateTokenDisplay();
    }

    private void UpdateTokenDisplay()
    {
        tokenText.text = "Tokens: " + playerTokens;
    }
}
