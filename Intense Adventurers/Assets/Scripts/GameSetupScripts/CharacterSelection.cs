using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private CharacterDatabase database;
    [SerializeField] private GameObject playerEntry;
    [SerializeField] private Transform characterList;
    [SerializeField] private MonoPlayerData choosingPlayer;

    private CharacterInfo selectedCharacter;

    [SerializeField] private TMPro.TextMeshProUGUI chosenCharacterLabel;

    void Start()
    {
        FillList();
    }

    private void FillList()
    {
        foreach (CharacterInfo savedCharacter in database.characters)
        {
            GameObject characterButton = Instantiate(playerEntry, characterList);

            TextMeshProUGUI characterButtonText = characterButton.GetComponentInChildren<TextMeshProUGUI>();
            CharacterUIEntry entry = characterButton.GetComponent<CharacterUIEntry>();

            entry.character = savedCharacter;
            entry.selectionMenu = this;

            if (characterButtonText != null)
            {
                characterButtonText.text = savedCharacter.Nickname;
            }
        }
    }

    public void SelectCharacter(CharacterInfo character)
    {
        selectedCharacter = character;
        chosenCharacterLabel.text = character.Nickname;

        choosingPlayer.InitializePlayer(selectedCharacter);
        Debug.Log("Wybrano postaæ: " + character.Nickname);
    }
}
