using UnityEngine;

public class CharacterUIEntry : MonoBehaviour
{
    public CharacterInfo character;
    public CharacterSelection selectionMenu;

    public void OnSelect()
    {
        selectionMenu.SelectCharacter(character);
    }
}
