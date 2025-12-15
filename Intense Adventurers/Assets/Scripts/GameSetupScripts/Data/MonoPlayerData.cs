using UnityEngine;

public class MonoPlayerData : MonoBehaviour
{
    enum PossiblePlayerPosition { P1, P2, P3, P4 }

    [SerializeField] PossiblePlayerPosition playerPosition;
    private CharacterInfo chosenCharacter; // Przypisz `PlayerDataInstance` przez Inspektor w Unity

    public PlayerData playerData;

    public void InitializePlayer(CharacterInfo character)
    {
        chosenCharacter = character;

        playerData.playerPosition = playerPosition.ToString();
        playerData.chosenCharacterInfo = chosenCharacter;
    }
}
