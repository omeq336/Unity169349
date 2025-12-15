using System.IO;
using UnityEngine;

public class RPGCharacter : MonoBehaviour
{
    private CharacterInfo characterData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Material defaultColor;

    // Klasa postaci
    // private class class;

    // Statystyki punktowe
    [SerializeField]
    private int strength;
    [SerializeField]
    private int dexterity;
    [SerializeField]
    private int constitution;
    [SerializeField]
    private int agility;
    [SerializeField]
    private int inteligence;
    [SerializeField]
    private int magicka;

    /*
    private float maxHealth = 20f;
    private float damageResistance = 0f;
    private float movementSpeed = 1f;
    private float attackSpeed = 1f;
    private float pushStrength = 1f;
    private float pushResistane = 1f;
    private float mana = 10f;
    private float magicDamage = 5f;
    private float magicResistance = 1f;
    */
    private int damage = 2;

    // Player info
    [SerializeField] private string nickname;
    private int level = 1;
    private float exp = 0;
    [SerializeField] private int gold = 0;

    // Eq

    //[SerializeField]
    //private Weapon weaponEquipped;
    //[SerializeField] private Transform rightHandTransform;
    //private GameObject weaponInstance;

    // For material editing
    [SerializeField] private Renderer chestRenderer;
    [SerializeField] private Renderer leftArmRenderer;
    [SerializeField] private Renderer rightArmRenderer;

    // private Armor armorEquipped

    // Gettery
    public int MaxHealth => CalculateMaxHealth();
    // public float DamageResistance => CalculateDamageResistance();
    public float MovementSpeed => CalculateMovementSpeed();
    public float JumpStrength => CalculateJumpStrength();
    public int Gold => gold;
    public PlayerData SharedPlayerData => playerData;
    // public int Damage => CalculateDamageDealt();
    // public float AttackSpeed => CalculateAttackSpeed();
    // public float PushStrength => CalculatePushStrength();
    // public float PushResistane => CalculatePushResistance();
    // public float Mana => CalculateMana();
    // public float MagicDamage => CalculateMagicDamage();
    // public float MagicResistance => CalculateMagicResistance();

    void Start()
    {
        characterData = playerData.chosenCharacterInfo;

        // Color setup
        Material characterColor = playerData.chosenCharacterInfo.CharacterColor;
        if (characterColor == null) characterColor = defaultColor;


        chestRenderer.material = characterColor;
        leftArmRenderer.material = characterColor;
        rightArmRenderer.material = characterColor;

        nickname = playerData.chosenCharacterInfo.Nickname;
        strength = playerData.chosenCharacterInfo.Strength;
        dexterity = playerData.chosenCharacterInfo.Dexterity;
        constitution = playerData.chosenCharacterInfo.Constitution;
        agility = playerData.chosenCharacterInfo.Agility;
        inteligence = playerData.chosenCharacterInfo.Inteligence;
        magicka = playerData.chosenCharacterInfo.Magicka;

    }

    private int CalculateMaxHealth()
    {
        int healthPerCon = 3;
        int initialMaxHealth = 17;

        return initialMaxHealth + (healthPerCon * constitution);
    }

    private float CalculateDamageResistance()
    {
        float drPerDex = 1;
        float initialDamageResistance = 1f;

        return initialDamageResistance + (drPerDex * dexterity);
    }
    private float CalculateMovementSpeed()
    {
        float msPerAgility = 1;
        float initialMovementSpeed = 9f;

        return initialMovementSpeed + (msPerAgility * agility);
    }
    /*
    private int CalculateDamageDealt()
    {
        return damage + weaponEquipped.ItemDamage;
    }
    */
    private float CalculateJumpStrength()
    {
        float jsPerStrength = 1;
        float initialJumpStrength = 2f;

        return initialJumpStrength + (jsPerStrength * strength);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        characterData.AddGold(amount);
    }

    /*
    public void OnStrengthAdded()
    {
        strength += 1;
        damage += 2;
        pushStrength += 2;
    }
    public bool OnStrengthSubtracted()
    {
        if (strength > 1)
        {
            strength -= 1;
            damage -= 2;
            pushStrength -= 2;
            return true;
        }
        return false;

    }

    public void onDexterityAdded()
    {
        dexterity += 1;
        pushResistane += 2;
        damageResistance += 1;
    }
    public bool onDexteritySubtracted()
    {
        if (dexterity > 1)
        {
            dexterity -= 1;
            pushResistane -= 2;
            damageResistance -= 1;
            return true;

        }
        return false;

    }

    public void onConsitutionAdded()
    {
        constitution += 1;
        health += 3;
    }

    public bool onConsitutionSubtracted()
    {
        if (constitution > 1)
        {
            constitution -= 1;
            health -= 3;
            return true;

        }
        return false;

    }

    public void onAgilityAdded()
    {
        agility += 1;
        movementSpeed += 1;
        attackSpeed += 1;
    }

    public bool onAgilitySubtracted()
    {
        if (agility > 1)
        {
            agility -= 1;
            movementSpeed -= 1;
            attackSpeed -= 1;
            return true;

        }
        return false;

    }

    public void onIntelectAdded()
    {
        inteligence += 1;
        mana += 3;
    }

    public void onIntelectSubtracted()
    {
        if (inteligence > 1)
        {
            inteligence -= 1;
            mana -= 3;
        }

    }

    public void onMagickaAdded()
    {
        magicka += 1;
        magicDamage += 3;
    }

    public void onMagickaSubtracted()
    {
        if (magicka > 1)
        {
            magicka -= 1;
            magicDamage -= 3;
        }
    }

    public void onNicknameAdded(string givenNickname)
    {
        nickname = givenNickname;
    }
    */
}
