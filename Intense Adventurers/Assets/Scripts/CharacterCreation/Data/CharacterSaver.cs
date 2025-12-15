using System.Collections.Generic;
using UnityEngine;

public class CharacterSaver : MonoBehaviour
{
    public CharacterDatabase characterDatabase;
    //[SerializeField] private List<Character> characterList;

    private void Start()
    {
        //characterList = new List<Character>();
        Debug.Log("database: " + characterDatabase);
    }

    public void AddCharacter(Character character)
    {
        //characterList.Add(character);
    }


}
