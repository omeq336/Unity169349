using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerPosition;
    public string currentTileId;
    public bool wasTileEventResolved;
    public CharacterInfo chosenCharacterInfo;
    public int lifesOnBoard;

    public void ResetPlayerData()
    {
        playerPosition = null;
        currentTileId = null;
        wasTileEventResolved = false;
        chosenCharacterInfo = null;
        lifesOnBoard = 0;
    }

#if UNITY_EDITOR
    private string _initialJson = string.Empty;
#endif

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        _initialJson = EditorJsonUtility.ToJson(this);
#endif
    }

#if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        switch (obj)
        {
            case PlayModeStateChange.ExitingPlayMode:
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                EditorJsonUtility.FromJsonOverwrite(_initialJson, this);
                break;
        }
    }
#endif
}
