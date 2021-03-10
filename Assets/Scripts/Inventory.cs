using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemScriptable> inventory = new List<ItemScriptable>();
    public List<ItemScriptable> GetInvevtory
    {
        get { return inventory; }
    }
    [SerializeField] private ScriptableInt gold;
    public void AddItem(ItemScriptable item)
    {
        if(item != null)
        {
            inventory.Add(item);
        }
    }

    public void RemoveItem(ItemScriptable item)
    {
        if(item != null)
        {
            inventory.Remove(item);
        }
    }

    public void BuyItem(ItemScriptable item)
    {
        if(gold.value >= item.buyValue)
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
