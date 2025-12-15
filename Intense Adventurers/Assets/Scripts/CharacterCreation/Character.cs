using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private RPGCharacter rpgCharacter;
    // [SerializeField] private ActionCharacter actionCharacter;

    public void initializeCharacter(RPGCharacter loadedRPGChar, ActionCharacter loadedActionChar)
    {
        rpgCharacter = loadedRPGChar;
        // actionCharacter = loadedActionChar;
    }
    /*

    public void AddNickname(string nickname)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            rpgCharacter.onNicknameAdded("Gosc");
        }
        else
        {
            rpgCharacter.onNicknameAdded(nickname);
        }
    }

    public void AddStrength()
    {
        rpgCharacter.OnStrengthAdded();
      
    }

    public bool SubtractStrength()
    {
        bool operationSuccess = rpgCharacter.OnStrengthSubtracted();
        return operationSuccess;
    }

    public void AddDexterity()
    {

        rpgCharacter.onDexterityAdded();
    }

    public bool SubtractDexterity()
    {
        bool operationSuccess = rpgCharacter.onDexteritySubtracted();
        return operationSuccess;
    }

    public void AddConstitution()
    {
        
        rpgCharacter.onConsitutionAdded();
    }

    public bool SubtractConstitution()
    {
        bool operationSuccess = rpgCharacter.onConsitutionSubtracted();
        return operationSuccess;

    }

    public void AddAgility()
    {
        rpgCharacter.onAgilityAdded();
    }

    public bool SubtractAgility()
    {
        bool operationSuccess = rpgCharacter.onAgilitySubtracted();

        return operationSuccess;
    }
    */
}
