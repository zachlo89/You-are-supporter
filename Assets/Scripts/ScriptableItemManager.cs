using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObject/Inventory")]
public class ScriptableItemManager : ScriptableObject
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private List<ItemScriptable> inventory = new List<ItemScriptable>();
    public List<ItemScriptable> GetInvevtory
    {
        get { return inventory; }
    }
    [SerializeField] private ScriptableInt gold;
    public ScriptableInt Gold
    {
        get { return gold; }
    }

    public void AddGold(int gold)
    {
        this.gold.value += gold;
        if(delegator == null)
        {
            delegator = GameObject.FindObjectOfType<DelegateToUpdateCharacterEquipment>();
        }
        delegator.updateCount();
    }

    [SerializeField] private ScriptableInt rubins;

    public ScriptableInt Rubins
    {
        get { return rubins; }
    }

    public void AddRubins(int rubins)
    {
        this.rubins.value += rubins;
        if (delegator == null)
        {
            delegator = GameObject.FindObjectOfType<DelegateToUpdateCharacterEquipment>();
        }
        delegator.updateCount();
    }

    [SerializeField] private ScriptableInt normalChestCount;
    public ScriptableInt GetNormalChestCount
    {
        get { return normalChestCount; }
    }

    public void AddNormalChest(int value)
    {
        this.normalChestCount.value += value;
        if (delegator == null)
        {
            delegator = GameObject.FindObjectOfType<DelegateToUpdateCharacterEquipment>();
        }
        delegator.updateCount();
    }

    [SerializeField] private ScriptableInt epicChestsCount;
    public ScriptableInt EpicChestsCount
    {
        get { return epicChestsCount; }
    }

    public void AddEipicChest(int value)
    {
        this.epicChestsCount.value += value;
        if (delegator == null)
        {
            delegator = GameObject.FindObjectOfType<DelegateToUpdateCharacterEquipment>();
        }
        delegator.updateCount();
    }
    public void AddItem(ItemScriptable item)
    {
        inventory.Insert(0, item);
    }

    public void SwapItems(ItemScriptable item, int index)
    {
        inventory[index] = item;
    }

    public void RemoveItem(int index)
    {
        inventory.RemoveAt(index);
    }

    public void BuyItem(ItemScriptable item)
    {
        if (gold.value >= item.buyValue)
        {
            gold.value -= item.buyValue;
            AddItem(item);
        }
    }

    public void SellItem(int index)
    {
        AddGold(inventory[index].sellValue);
        inventory.Remove(inventory[index]);
    }
}
