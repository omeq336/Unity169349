using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    private string nickname;
    private int strength = 1;
    private int dexterity = 1;
    private int constitution = 1;
    private int agility = 1;
    private int inteligence = 1;
    private int magicka = 1;
    private Material characterColor;
    private int gold = 0;

    public string Nickname => nickname;
    public int Strength => strength;
    public int Dexterity => dexterity;
    public int Constitution => constitution;
    public int Agility => agility;
    public int Inteligence => inteligence;
    public int Magicka => magicka;
    public Material CharacterColor => characterColor;
    public int Gold => gold;

    public void AddStrength()
    {
        strength += 1;
    }
    public bool SubtractStrength()
    {
        if (strength > 1)
        {
            strength -= 1;
            return true;
        }
        return false;

    }

    public void AddDexterity()
    {
        dexterity += 1;
    }

    public bool SubtractDexterity()
    {
        if (dexterity > 1)
        {
            dexterity -= 1;
            return true;

        }
        return false;

    }

    public void AddConstitution()
    {
        constitution += 1;
    }

    public bool SubtractConstitution()
    {
        if (constitution > 1)
        {
            constitution -= 1;
            return true;

        }
        return false;

    }

    public void AddAgility()
    {
        agility += 1;
    }

    public bool SubtractAgility()
    {
        if (agility > 1)
        {
            agility -= 1;
            return true;

        }
        return false;

    }

    public void AddIntelect()
    {
        inteligence += 1;
    }

    public void SubtractIntelect()
    {
        if (inteligence > 1)
        {
            inteligence -= 1;
        }

    }
    public void AddMagicka()
    {
        magicka += 1;
    }

    public void SubtractMagicka()
    {
        if (magicka > 1)
        {
            magicka -= 1;
        }
    }

    public void SetNickname(string givenNickname)
    {
        nickname = givenNickname;
    }

    public void SetColor(Material color)
    {
        characterColor = color;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }
}
