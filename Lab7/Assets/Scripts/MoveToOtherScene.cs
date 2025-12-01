using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToOtherScene : MonoBehaviour
{
    [SerializeField] private string someScene;
    [SerializeField] PlayerData testingData;

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(someScene);
    }

    void OnMouseDown()
    {
        testingData.playerHealth += 50;
        LoadSceneByName();
    }
}


