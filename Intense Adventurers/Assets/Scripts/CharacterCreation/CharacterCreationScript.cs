using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationScript : MonoBehaviour
{
    [SerializeField] private CharacterInfo currentCharacter;
    [SerializeField] private Renderer characterTemplateChest;
    [SerializeField] private Renderer characterTemplateLeftArm;
    [SerializeField] private Renderer characterTemplateRightArm;


    [SerializeField] CharacterDatabase characterDatabase;
    [SerializeField] CharacterClothMaterialsDatabase clothMaterialsDatabase;
    private int currentMaterialIndex = 0;

    [SerializeField] private UnityEngine.UI.Button colorButton;
    [SerializeField] private List<string> AvailableColors;

    [SerializeField] private TMPro.TextMeshProUGUI pointsRemainingLabel;

    [SerializeField] private TMPro.TMP_InputField nicknameInput;
    private string defaultNickname = "Gosc";

    [SerializeField] private TMPro.TextMeshProUGUI strLabel;
    [SerializeField] private TMPro.TextMeshProUGUI dexLabel;
    [SerializeField] private TMPro.TextMeshProUGUI conLabel;
    [SerializeField] private TMPro.TextMeshProUGUI agiLabel;

    private int pointsToSpend = 8;

    private void Start()
    {
        RefreshRemainingPoints();

        currentCharacter.SetNickname(defaultNickname);

        Material chosenClothColor = clothMaterialsDatabase.materials[currentMaterialIndex];

        currentCharacter.SetColor(chosenClothColor);
        ChangeButtonColor();
        ChangeCharacterTemplateColor(chosenClothColor);
    }

    public void AddNickname()
    {
        string givenNickname = nicknameInput.text;
        if (string.IsNullOrEmpty(givenNickname))
        {
            currentCharacter.SetNickname(defaultNickname);
        }
        else
        {
            currentCharacter.SetNickname(givenNickname);
        }
    }

    public void AddStrength()
    {
        if (pointsToSpend > 0)
        {
            currentCharacter.AddStrength();
            int newValue = int.Parse(strLabel.text) + 1;
            strLabel.text = newValue.ToString();

            pointsToSpend--;
            RefreshRemainingPoints();
        }
    }
    public void SubtractStrength()
    {
        bool operationSuccess = currentCharacter.SubtractStrength();
        if (operationSuccess)
        {
            int newValue = int.Parse(strLabel.text) - 1;
            strLabel.text = newValue.ToString();
            pointsToSpend++;
            RefreshRemainingPoints();
        }
        else
        {
            Debug.Log("Statystyka nie mo¿e siêgn¹æ poni¿ej zera!!");
        }
    }
    /*
    public void AddDexterity()
    {
        if (pointsToSpend > 0)
        {
            currentCharacter.AddDexterity();
            int newValue = int.Parse(dexLabel.text) + 1;
            dexLabel.text = newValue.ToString();

            pointsToSpend--;
            RefreshRemainingPoints();
        }
    }
    public void SubtractDexterity()
    {
        bool operationSuccess = currentCharacter.SubtractDexterity();
        if (operationSuccess)
        {
            int newValue = int.Parse(dexLabel.text) - 1;
            dexLabel.text = newValue.ToString();
            pointsToSpend++;
            RefreshRemainingPoints();

        }
        else
        {
            Debug.Log("Statystyka nie mo¿e siêgn¹æ poni¿ej zera!!");
        }
    }
    */
    public void AddConstitution()
    {
        if (pointsToSpend > 0)
        {
            currentCharacter.AddConstitution();
            int newValue = int.Parse(conLabel.text) + 1;
            conLabel.text = newValue.ToString();

            pointsToSpend--;
            RefreshRemainingPoints();
        }
    }
    public void SubtractConstitution()
    {
        bool operationSuccess = currentCharacter.SubtractConstitution();
        if (operationSuccess)
        {
            int newValue = int.Parse(conLabel.text) - 1;
            conLabel.text = newValue.ToString();
            pointsToSpend++;
            RefreshRemainingPoints();

        }
        else
        {
            Debug.Log("Statystyka nie mo¿e siêgn¹æ poni¿ej zera!!");
        }
    }
    public void AddAgility()
    {
        if (pointsToSpend > 0)
        {
            currentCharacter.AddAgility();
            int newValue = int.Parse(agiLabel.text) + 1;
            agiLabel.text = newValue.ToString();

            pointsToSpend--;
            RefreshRemainingPoints();
        }
    }
    public void SubtractAgility()
    {
        bool operationSuccess = currentCharacter.SubtractAgility();
        if (operationSuccess)
        {
            int newValue = int.Parse(agiLabel.text) - 1;
            agiLabel.text = newValue.ToString();
            pointsToSpend++;
            RefreshRemainingPoints();

        }
        else
        {
            Debug.Log("Statystyka nie mo¿e siêgn¹æ poni¿ej zera!!");
        }
        /*
    public void AddIntelect()
    {
        currentCharacter.onIntelectAdded();
    }
    public void SubtractIntelect()
    {
        bool operationSuccess = currentCharacter.onIntelectSubtracted();
        if (operationSuccess)
        {
            int newValue = int.Parse(strLabel.text) - 1;
            strLabel.text = newValue.ToString();
        }
        else
        {
            Debug.Log("Statystyka nie mo¿e siêgn¹æ poni¿ej zera!!");
        }
    public void AddMagicka()
    {
        currentCharacter.onMagickaAdded();
    }
    public void SubtractMagicka()
    {
        bool operationSuccess = currentCharacter.onMagickaSubtracted();
        if (operationSuccess)
        {
            int newValue = int.Parse(strLabel.text) - 1;
            strLabel.text = newValue.ToString();
        }
        else
        {
            Debug.Log("Statystyka nie mo¿e siêgn¹æ poni¿ej zera!!");
        }
}
        */
    }

    public void SaveCharacter()
    {
        characterDatabase.AddCharacter(currentCharacter);
    }

    public void OnCharacterColorChanged()
    {
        List<Material> materials = clothMaterialsDatabase.materials;

        currentMaterialIndex++;

        if (currentMaterialIndex >= materials.Count)
        {
            currentMaterialIndex = 0;
        }

        Material chosenClothColor = clothMaterialsDatabase.materials[currentMaterialIndex];

        ChangeButtonColor();
        ChangeCharacterTemplateColor(chosenClothColor);
        currentCharacter.SetColor(chosenClothColor);
    }

    private void ChangeButtonColor()
    {
        ///Ten fragment zosta³ wygnerowany chatem gpt
        Image buttonImage = colorButton.GetComponent<Image>();
        Color hexColor;

        if (ColorUtility.TryParseHtmlString(AvailableColors[currentMaterialIndex], out hexColor))
        {
            buttonImage.color = hexColor;
        }
    }

    private void ChangeCharacterTemplateColor(Material clothColor)
    {
        characterTemplateChest.material = clothColor;
        characterTemplateLeftArm.material = clothColor;
        characterTemplateRightArm.material = clothColor;
    }

    private void RefreshRemainingPoints()
    {
        pointsRemainingLabel.text = pointsToSpend.ToString();
    }
}