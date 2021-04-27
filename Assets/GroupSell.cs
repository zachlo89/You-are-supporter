using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupSell : MonoBehaviour
{
    [SerializeField] private List<Toggle> togglers = new List<Toggle>();
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private EquipmentInventory equipmentInventory;
    private List<Rarity> rarityList = new List<Rarity>();

    private void OnEnable()
    {
        foreach(Toggle togg in togglers)
        {
            togg.isOn = false;
        }
    }

    public void GroupSellItems()
    {
        for(int i = 0; i < togglers.Count; i++)
        {
            if (togglers[i].isOn)
            {
                AddToRarityList(i);
            }
        }

        if(inventory.GetInvevtory.Count > 0)
        {
            for (int i = inventory.GetInvevtory.Count - 1; i >= 0; i--)
            {
                foreach (Rarity rarity in rarityList)
                {
                    if (inventory.GetInvevtory[i].rarity == rarity)
                    {
                        equipmentInventory.SellItem(i);
                        break;
                    }
                }
            }
        }
    }

    private void AddToRarityList(int i)
    {
        switch (i)
        {
            case 0:
                rarityList.Add(Rarity.Common);
                break;
            case 1:
                rarityList.Add(Rarity.Uncommon);
                break;
            case 2:
                rarityList.Add(Rarity.Rare);
                break;
            case 3:
                rarityList.Add(Rarity.Epic);
                break;
            case 4:
                rarityList.Add(Rarity.Mythical);
                break;
            case 5:
                rarityList.Add(Rarity.Legendary);
                break;
        }
    }
}
