using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CompareAndDisplayDetails : MonoBehaviour
{
    private GameObject detailsPanel;
    private EquipmentPanel equipmentPanel;
    private ScriptableCharacter hero;
    private ItemScriptable item;
    private int itemInventoryIndex;

    public void EquipmentDetails()
    {
        detailsPanel.SetActive(true);
        detailsPanel.GetComponent<DetailsPanel>().EquipmentDetails(item, this);
    }
    public void DisplayDetails()
    {
        detailsPanel.SetActive(true);
        detailsPanel.GetComponent<DetailsPanel>().DisplayDetails(item, this);
    }

    public void SetItemAndEquipment(ItemScriptable item, ScriptableCharacter hero, EquipmentPanel equipmentPanel, GameObject detailsPanel)
    {
        this.detailsPanel = detailsPanel;
        this.equipmentPanel = equipmentPanel;
        if(item != null && hero != null)
        {
            this.item = item;
            this.hero = hero;
        }    
    }

    public void EquipItem(int slotIndex)
    {
        itemInventoryIndex = transform.GetSiblingIndex();
        
        if (slotIndex < 10)
        {
            if(hero.equipment.GetEquipment[slotIndex] != null)
            {
                equipmentPanel.EquipAndUnEquip(item, hero.equipment.GetEquipment[slotIndex], slotIndex, itemInventoryIndex);
                return;
            }
            equipmentPanel.EquipItem(item, slotIndex, itemInventoryIndex);
        } else if (slotIndex == 99)
        {
            if (hero.equipment.GetEquipment[slotIndex] != null)
            {
                equipmentPanel.EquipAndUnEquip(item, hero.equipment.GetEquipment[slotIndex], slotIndex, itemInventoryIndex);
            }
            if (hero.equipment.GetEquipment[slotIndex] != null)
            {
                equipmentPanel.UnEquipItem(item, slotIndex);
                return;
            }
            if(hero.equipment.GetEquipment[slotIndex] == null)
            {
                equipmentPanel.EquipItem(item, 2, itemInventoryIndex);
            }  
        }
    }

    public void UnEquip(int slotIndex)
    {
        equipmentPanel.UnEquipItem(item, slotIndex);
    }

    public void SellItem()
    {
        itemInventoryIndex = transform.GetSiblingIndex();
        equipmentPanel.SellItem(itemInventoryIndex);
    }
    
}
