using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLootGenerator : MonoBehaviour
{
    [SerializeField] private ScriptableItemManager listOfAllItems;
    private List<ItemScriptable> commonItems = new List<ItemScriptable>();
    private List<ItemScriptable> uncommonItems = new List<ItemScriptable>();
    private List<ItemScriptable> rareItems = new List<ItemScriptable>();
    private List<ItemScriptable> epicItems = new List<ItemScriptable>();

    private void Start()
    {
        LoadItems();
        gameObject.SetActive(false);
    }

    private void LoadItems()
    {
        foreach(ItemScriptable item in listOfAllItems.GetInvevtory)
        {
            if (item.rarity == Rarity.Common)
            {
                commonItems.Add(item);
            }
            else if (item.rarity == Rarity.Uncommon)
            {
                uncommonItems.Add(item);
            }
            else if (item.rarity == Rarity.Rare)
            {
                rareItems.Add(item);
            }
            else if (item.rarity == Rarity.Epic)
            {
                epicItems.Add(item);
            }
        }
    }

    public ItemScriptable GetRandomItems(int lootRarityLevel)
    {
        int random = Random.Range(0, 100);
        switch (lootRarityLevel)
        {
            case 0:
                if(random < 40)
                {
                    return commonItems[Random.Range(0, commonItems.Count)];
                } else if (random < 70)
                {
                    return uncommonItems[Random.Range(0, uncommonItems.Count)];
                } else
                {
                    return rareItems[Random.Range(0, rareItems.Count)];
                }
                
            case 1:
                if (random < 40)
                {
                    return uncommonItems[Random.Range(0, uncommonItems.Count)];
                }
                else if (random < 70)
                {
                    return rareItems[Random.Range(0, rareItems.Count)];
                }
                else
                {
                    return epicItems[Random.Range(0, epicItems.Count)];
                }
            default:
                {
                    Debug.Log("GetRandomItem: Something went wrong");
                    return null;
                }
        }
    }
}
