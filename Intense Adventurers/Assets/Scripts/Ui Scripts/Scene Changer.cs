using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private List<string> minigameScenes = new List<string>();

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void HidePanel(GameObject givenPanel)
    {
        givenPanel.SetActive(false);
    }

    public void ShowPanel(GameObject givenPanel)
    {
        givenPanel.SetActive(true);
    }

    public void ExchangeWithOtherPanel(GameObject[] panels)
    {
        bool isPanel1Active = panels[0].activeSelf;

        panels[0].SetActive(!isPanel1Active);
        panels[1].SetActive(isPanel1Active);
    }

    public void GetRandomMinigameScene()
    {
        int randomIndex = Random.Range(0, minigameScenes.Count);

        LoadSceneByName(minigameScenes[randomIndex]);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
