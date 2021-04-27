using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObject/Inventory")]
public class ScriptableItemManager : ScriptableObject
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private List<ItemScriptable> inventory = new List<ItemScriptable>();
    [SerializeField] private ScriptableInt questGold;

    public void ResetInventory()
    {
        inventory.Clear();
        gold.value = 0;
        rubins.value = 0;
        normalChestCount.value = 0;
        epicChestsCount.value = 0;
    }
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
        if(gold > 0)
        {
            questGold.value += gold;
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

    [SerializeField] private ScriptableInt energy;
    public ScriptableInt Energy
    {
        get { return energy; }
    }

    [SerializeField] private ScriptableInt maxEnergy;
    public ScriptableInt MaxEnergy
    {
        get { return maxEnergy; }
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

    public void AddEnergy(int energy)
    {
        this.energy.value += energy;
        if(this.energy.value > maxEnergy.value)
        {
            this.energy.value = maxEnergy.value;
        }
        if (delegator == null)
        {
            delegator = GameObject.FindObjectOfType<DelegateToUpdateCharacterEquipment>();
        }
        delegator.updateCount();
    }
}
