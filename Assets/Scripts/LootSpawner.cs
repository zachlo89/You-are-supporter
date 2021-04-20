using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LootSpawner : MonoBehaviour
{
    [SerializeField] private ScriptableInt questGold;
    [SerializeField] private ItemScriptable staff;
    [SerializeField] private ItemScriptable sword;
    [SerializeField] private ScriptableItemManager listOfAllItems;
    private int stageLevel;
    [SerializeField] private GameObject spawningPoint;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private int spawnItemMaxCount;
    private List<ItemScriptable> commonItems = new List<ItemScriptable>();
    private List<ItemScriptable> uncommonItems = new List<ItemScriptable>();
    private List<ItemScriptable> rareItems = new List<ItemScriptable>();
    private List<ItemScriptable> epicItems = new List<ItemScriptable>();
    private List<ItemScriptable> mythicalItems = new List<ItemScriptable>();
    private List<ItemScriptable> legendaryItems = new List<ItemScriptable>();
    private bool shouldDropLoot = true;

    [SerializeField] private ScriptableItemManager inventory;
    private int goldDropped;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        stageLevel = gameManager.CurrentLevel.itemsRarityDrop;
        LoadItems();
        goldDropped = 0;
    }

    private void LoadItems()
    {
        switch (stageLevel)
        {
            case 1:
                foreach(ItemScriptable item in listOfAllItems.GetInvevtory)
                {
                    if(item.rarity == Rarity.Common)
                    {
                        commonItems.Add(item);
                    }
                }
                break;
            case 2:
                foreach (ItemScriptable item in listOfAllItems.GetInvevtory)
                {
                    if (item.rarity == Rarity.Common)
                    {
                        commonItems.Add(item);
                    } else if (item.rarity == Rarity.Uncommon)
                    {
                        uncommonItems.Add(item);
                    }
                }
                break;
            case 3:
                foreach (ItemScriptable item in listOfAllItems.GetInvevtory)
                {
                    if (item.rarity == Rarity.Common)
                    {
                        commonItems.Add(item);
                    }
                    else if (item.rarity == Rarity.Uncommon)
                    {
                        uncommonItems.Add(item);
                    } else if (item.rarity == Rarity.Rare)
                    {
                        rareItems.Add(item);
                    }
                }
                break;
            case 4:
                foreach (ItemScriptable item in listOfAllItems.GetInvevtory)
                {
                    if (item.rarity == Rarity.Uncommon)
                    {
                        uncommonItems.Add(item);
                    }
                    else if (item.rarity == Rarity.Rare)
                    {
                        rareItems.Add(item);
                    }
                }
                break;
            case 5:
                foreach (ItemScriptable item in listOfAllItems.GetInvevtory)
                {
                    if (item.rarity == Rarity.Epic)
                    {
                        epicItems.Add(item);
                    }
                    else if (item.rarity == Rarity.Uncommon)
                    {
                        uncommonItems.Add(item);
                    }
                    else if (item.rarity == Rarity.Rare)
                    {
                        rareItems.Add(item);
                    }
                }
                break;
            case 6:
                foreach (ItemScriptable item in listOfAllItems.GetInvevtory)
                {
                    if (item.rarity == Rarity.Epic)
                    {
                        epicItems.Add(item);
                    }
                    else if (item.rarity == Rarity.Rare)
                    {
                        rareItems.Add(item);
                    }
                }
                break;
        }
    }

    private ItemScriptable GetRandomItems()
    {
        int random = Random.Range(0, 100);
        switch (stageLevel)
        {
            case 1:
                return commonItems[Random.Range(0, commonItems.Count)];
            case 2:
                if (random < 60)
                {
                    return commonItems[Random.Range(0, commonItems.Count)];
                }
                else return uncommonItems[Random.Range(0, uncommonItems.Count)];
            case 3:
                if (random < 50)
                {
                    return commonItems[Random.Range(0, commonItems.Count)];
                }
                else if (random < 80)
                {
                    return uncommonItems[Random.Range(0, uncommonItems.Count)];
                }
                else
                {
                    return rareItems[Random.Range(0, rareItems.Count)];
                }
            case 4:
                if (random < 60)
                {
                    return uncommonItems[Random.Range(0, uncommonItems.Count)];
                }
                else return rareItems[Random.Range(0, rareItems.Count)];
            case 5:
                if (random < 50)
                {
                    return uncommonItems[Random.Range(0, uncommonItems.Count)];
                }
                else if (random < 80)
                {
                    return rareItems[Random.Range(0, rareItems.Count)];
                }
                else
                {
                    return epicItems[Random.Range(0, epicItems.Count)];
                }
            case 6:
                if (random < 60)
                {
                    return rareItems[Random.Range(0, rareItems.Count)];
                }
                else return epicItems[Random.Range(0, epicItems.Count)];
            default:
                {
                    Debug.Log("GetRandomItem: Something went wrong");
                    return null;
                }
        }
    }

    public void SpawnItems()
    {
        if (shouldDropLoot)
        {
            shouldDropLoot = false;
            if (PlayerPrefs.GetInt("Tutorial2", -1) != 0)
            {
                int random = Random.Range(1, spawnItemMaxCount);
                for (int i = 0; i < random; i++)
                {
                    GameObject temp = Instantiate(iconPrefab, spawningPoint.transform);
                    ItemScriptable item = GetRandomItems();
                    temp.GetComponentInChildren<RewardItemIcon>().PopulateRewardIcon(item);
                    inventory.AddItem(item);
                    goldDropped = Random.Range(10 * stageLevel, 50 * stageLevel);
                    goldValue.text = goldDropped.ToString();
                    inventory.Gold.value += goldDropped;
                    questGold.value += goldDropped;
                }
            }
            else
            {
                PlayerPrefs.SetInt("Tutorial2", -1);
                GameObject temp = Instantiate(iconPrefab, spawningPoint.transform);
                temp.GetComponentInChildren<RewardItemIcon>().PopulateRewardIcon(staff);
                inventory.AddItem(staff);
                GameObject temp1 = Instantiate(iconPrefab, spawningPoint.transform);
                temp1.GetComponentInChildren<RewardItemIcon>().PopulateRewardIcon(sword);
                inventory.AddItem(sword);
                goldDropped = 200;
                goldValue.text = goldDropped.ToString();
                inventory.Gold.value += goldDropped;
                questGold.value += goldDropped;
            }
        }
    }

}
