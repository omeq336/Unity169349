using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coinsGatheredLabel;
    [SerializeField] private TMPro.TextMeshProUGUI healthRemainingLabel;
    [SerializeField] private TMPro.TextMeshProUGUI minigameTimeRemaining;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;

    public void UpdateCoinCounter(int amountOfCoins)
    {
        coinsGatheredLabel.text = amountOfCoins.ToString();
    }

    public void UpdateHealthRemaining(int amountOfHealth)
    {
        healthRemainingLabel.text = amountOfHealth.ToString();
    }

    public void UpdateMinigameTimeRemaining(int currentTime)
    {
        if (currentTime >= 0)
        {
            minigameTimeRemaining.text = (currentTime).ToString();

        }
    }

    public void showVictoryPanel()
    {
        victoryPanel.SetActive(true);
    }

    public void showGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

}
