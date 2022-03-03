using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Itemtype itemType;
    public EquipmentType equipmentType;
    public Sprite ItemImage;
    public GameObject itemPrefab;

    public int damage;
    public int defense;
    public int speed;
    public int hp_increase;
    public int sp_increase;

    public enum Itemtype
    {
        Equipment,
        Used,
        Ingredient
    }
    public enum EquipmentType
    {
        Sowrd,
        Amor,
        Helmet,
        Boots,
        Ring,
        Glove,
        etc
    }

}
