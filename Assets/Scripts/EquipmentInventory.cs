using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EquipmentInventory : MonoBehaviour
{
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private GameObject emptyPrefab;
    [SerializeField] private GameObject parentLocation;
    [SerializeField] private TextMeshProUGUI inventoryCount;


    private void Start()
    {
        PopulateInventory();
    }

    public void PopulateInventory()
    {
        foreach (Transform child in parentLocation.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if(inventory.GetInvevtory.Count > 0)
        {
            if(inventory.GetInvevtory.Count > 3)
            {
                inventory.GetInvevtory.Sort(delegate (ItemScriptable x, ItemScriptable y)
                {
                    return y.rarity.CompareTo(x.rarity);
                });
            }
            
            for (int i = 0; i < inventory.GetInvevtory.Count; i++)
            {
                GameObject temp = Instantiate(iconPrefab, parentLocation.transform);
                temp.GetComponent<SetItemIcon>().UpdateIconUI(inventory.GetInvevtory[i]);
                temp.AddComponent<DraggableComponent>();
                Destroy(temp.GetComponent<Button>());
                Destroy(temp.GetComponent<EventTrigger>());
            }

            inventoryCount.text = inventory.GetInvevtory.Count + "/200";
        }
    }

    public void SortItems(int j)
    {
        foreach (Transform child in parentLocation.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        switch (j)
        {
            case 0:
                for (int i = 0; i < inventory.GetInvevtory.Count; i++)
                {
                    if (inventory.GetInvevtory[i].slotPosition == SlotPosition.meelWeapon)
                    {
                        GameObject temp = Instantiate(iconPrefab, parentLocation.transform);
                        temp.GetComponent<SetItemIcon>().UpdateIconUI(inventory.GetInvevtory[i]);
                        temp.AddComponent<DraggableComponent>();
                        Destroy(temp.GetComponent<Button>());
                        Destroy(temp.GetComponent<EventTrigger>());
                    }
                }
                break;
            case 1:
                for (int i = 0; i < inventory.GetInvevtory.Count; i++)
                {
                    if (inventory.GetInvevtory[i].slotPosition == SlotPosition.head ||
                        inventory.GetInvevtory[i].slotPosition == SlotPosition.armor ||
                        inventory.GetInvevtory[i].slotPosition == SlotPosition.shoes)
                    {
                        GameObject temp = Instantiate(iconPrefab, parentLocation.transform);
                        temp.GetComponent<SetItemIcon>().UpdateIconUI(inventory.GetInvevtory[i]);
                        temp.AddComponent<DraggableComponent>();
                        Destroy(temp.GetComponent<Button>());
                        Destroy(temp.GetComponent<EventTrigger>());
                    }
                }
                break;
            case 2:
                for (int i = 0; i < inventory.GetInvevtory.Count; i++)
                {
                    if (inventory.GetInvevtory[i].slotPosition == SlotPosition.accessories)
                    {
                        GameObject temp = Instantiate(iconPrefab, parentLocation.transform);
                        temp.GetComponent<SetItemIcon>().UpdateIconUI(inventory.GetInvevtory[i]);
                        temp.AddComponent<DraggableComponent>();
                        Destroy(temp.GetComponent<Button>());
                        Destroy(temp.GetComponent<EventTrigger>());
                    }
                }
                break;
            default:
                PopulateInventory();
                break;
        }
    }

    public void AddItem(ItemScriptable item)
    {
        inventory.AddItem(item);
        inventoryCount.text = inventory.GetInvevtory.Count + "/200";
    }

    public void RemoveItem(int index)
    {
        inventory.RemoveItem(index);
        inventoryCount.text = inventory.GetInvevtory.Count + "/200";
    }

}
