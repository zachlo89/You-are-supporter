using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Equipment")]
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
    
    public void RemoveItem(int index)
    {
        equipment[index] = null;
    }

    public void ResetEquipment()
    {
        for(int i = 0; i < 6; i++)
        {
            equipment[i] = null;
        }
    }
}
