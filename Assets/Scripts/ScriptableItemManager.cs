using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObject/Inventory")]
public class ScriptableItemManager : ScriptableObject
{
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

    [SerializeField] private ScriptableInt rubins;

    public ScriptableInt Rubins
    {
        get { return rubins; }
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
        gold.value += inventory[index].sellValue;
        inventory.Remove(inventory[index]);
    }
}
