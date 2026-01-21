using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public enum ItemType
    {
        Consumable,
        Weapon,
        Armour,
        Key,

    }

    [Header("Item Basics")]
    public int itemID;
    public ItemType type = ItemType.Consumable;
    public string itemName = "New Item";

    [Header("Visuals & Description")] 
    public Sprite sprite;
    public string itemDescription = "New Item Description";

    [Header("Equipment Basics")]
    public int attackDamage;
    public int defenceBonus;

}
