using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Model
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab => prefab;

    // Item description variables
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDescription;
    [SerializeField]
    private string rarity;

    // Item effect variables
    [SerializeField]
    private int itemDamage;
    [SerializeField]
    private int knockback = 1;

    // Shop variables
    [SerializeField]
    private int itemBuyPrice;
    [SerializeField]
    private int itemSellPrice;

    // Getters
    public int ItemDamage => itemDamage;

}
