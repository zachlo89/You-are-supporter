using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment")]
public class Equipment : ScriptableObject
{
    [SerializeField] private List<ItemScriptable> equipment = new List<ItemScriptable>();

    public List<ItemScriptable> GetEquipment
    {
        get { return equipment; }
    }

    public void AddItem(ItemScriptable item, int itemIndex)
    {
        equipment[itemIndex] = item;
    }
    
    public void RemoveItem(ItemScriptable item, int index)
    {
        equipment[index] = null;
    }
}
