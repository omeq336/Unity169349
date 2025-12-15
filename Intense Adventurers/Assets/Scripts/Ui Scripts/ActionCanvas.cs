using UnityEngine;

public class ActionCanvas : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coinsGatheredLabel;
    [SerializeField] private TMPro.TextMeshProUGUI healthRemainingLabel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    public void UpdateCoinCounter(int amountOfCoins)
    {
        coinsGatheredLabel.text = amountOfCoins.ToString();
    }

    public void UpdateHealthRemaining(int amountOfHealth)
    {
        healthRemainingLabel.text = amountOfHealth.ToString();
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
