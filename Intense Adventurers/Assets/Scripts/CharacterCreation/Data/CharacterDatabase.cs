using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "CharacterDatabase")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterInfo> characters = new List<CharacterInfo>();

    public void AddCharacter(CharacterInfo character)
    {
        characters.Add(character);
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
