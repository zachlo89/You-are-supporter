using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Inventory")]
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
    public void AddItem(ItemScriptable item)
    {
        inventory.Add(item);
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

    public void SellItem(ItemScriptable item)
    {
        gold.value += item.sellValue;
        inventory.Remove(item);
    }
}
