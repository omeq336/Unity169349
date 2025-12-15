using UnityEngine;

public class BoardUi : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coinsGatheredLabel;
    [SerializeField] private TMPro.TextMeshProUGUI healthRemainingLabel;

    [SerializeField] private GameObject minigamePanel;
    [SerializeField] private BoardPlayer currentPlayer;

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

    public void ShowMinigamePanel()
    {
        minigamePanel.SetActive(true);
    }

    public void CloseMinigamePanel()
    {
        minigamePanel.SetActive(false);
    }

    public void SavePlayerCurrentTile()
    {
        currentPlayer.SendCurrentTileId();
    }

    public void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

}
