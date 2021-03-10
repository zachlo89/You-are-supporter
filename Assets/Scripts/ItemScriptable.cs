using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Mythical,
    Legendary
}
public enum SlotPosition
{
    head,
    armor,
    meelWeapon,
    twoHandedWeapon,
    shield,
    bow,
    shoes,
    accessories
}

[CreateAssetMenu (menuName = "ItemScriptable")]
public class ItemScriptable : ScriptableObject
{
    public int damage;
    public int armor;
    public int attackRate;
    public int hp;
    public List<Sprite> images = new List<Sprite>();
    public Sprite border;
    public SlotPosition slotPosition;
    public Rarity rarity;
    public string description;
    public int sellValue;
    public int buyValue;
}
