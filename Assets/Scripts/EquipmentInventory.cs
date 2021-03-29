using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EquipmentInventory : MonoBehaviour
{
    [SerializeField] private GameObject detailsPanel;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private GameObject emptyPrefab;
    [SerializeField] private GameObject parentLocation;
    [SerializeField] private TextMeshProUGUI inventoryCount;
    [SerializeField] private EquipmentPanel equipmentPanel;
    [SerializeField] private TextMeshProUGUI goldValue;

    private int sortMetod;

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
                temp.GetComponent<CompareAndDisplayDetails>().SetItemAndEquipment(inventory.GetInvevtory[i], equipmentPanel.GetHero(), equipmentPanel, detailsPanel);
                //temp.AddComponent<DraggableComponent>();
                //Destroy(temp.GetComponent<Button>());
                //Destroy(temp.GetComponent<EventTrigger>());
            }

            inventoryCount.text = inventory.GetInvevtory.Count + "/200";
        }
        goldValue.text = inventory.Gold.value.ToString();
    }

    public void SortItems(int j)
    {
        sortMetod = j;
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
                        temp.GetComponent<CompareAndDisplayDetails>().SetItemAndEquipment(inventory.GetInvevtory[i], equipmentPanel.GetHero(), equipmentPanel, detailsPanel);
                        //temp.AddComponent<DraggableComponent>();
                        //Destroy(temp.GetComponent<Button>());
                        //Destroy(temp.GetComponent<EventTrigger>());
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
                        temp.GetComponent<CompareAndDisplayDetails>().SetItemAndEquipment(inventory.GetInvevtory[i], equipmentPanel.GetHero(), equipmentPanel, detailsPanel);
                        //temp.AddComponent<DraggableComponent>();
                        //Destroy(temp.GetComponent<Button>());
                        //Destroy(temp.GetComponent<EventTrigger>());
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
                        temp.GetComponent<CompareAndDisplayDetails>().SetItemAndEquipment(inventory.GetInvevtory[i], equipmentPanel.GetHero(), equipmentPanel, detailsPanel);
                        //temp.AddComponent<DraggableComponent>();
                        //Destroy(temp.GetComponent<Button>());
                        //Destroy(temp.GetComponent<EventTrigger>());
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
        GameObject temp = Instantiate(iconPrefab, parentLocation.transform);
        temp.GetComponent<SetItemIcon>().UpdateIconUI(item);
        temp.GetComponent<CompareAndDisplayDetails>().SetItemAndEquipment(item, equipmentPanel.GetHero(), equipmentPanel, detailsPanel);
        temp.transform.SetSiblingIndex(0);
        inventoryCount.text = inventory.GetInvevtory.Count + "/200";
    }

    public void SwapItems(ItemScriptable item, int index)
    {
        inventory.SwapItems(item, index);
        GameObject temp = parentLocation.transform.GetChild(index).gameObject;
        temp.GetComponent<SetItemIcon>().UpdateIconUI(item);
        temp.GetComponent<CompareAndDisplayDetails>().SetItemAndEquipment(item, equipmentPanel.GetHero(), equipmentPanel, detailsPanel);
    }

    public void RemoveItem(int index)
    {
        inventory.RemoveItem(index);
        Transform temp = parentLocation.transform.GetChild(index);
        Destroy(temp.gameObject);
        inventoryCount.text = inventory.GetInvevtory.Count + "/200";
    }

    public void SellItem(int index)
    {
        inventory.SellItem(index);
        Transform temp = parentLocation.transform.GetChild(index);
        Destroy(temp.gameObject);
        inventoryCount.text = inventory.GetInvevtory.Count + "/200";
        goldValue.text = inventory.Gold.value.ToString();
    }
}
