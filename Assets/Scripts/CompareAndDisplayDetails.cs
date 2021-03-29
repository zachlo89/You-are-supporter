using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CompareAndDisplayDetails : MonoBehaviour
{
    [SerializeField] private GameObject detailsPanel;
    [SerializeField] private TextMeshPro rarity;
    [SerializeField] private TextMeshPro itemName;
    [SerializeField] private TextMeshPro descritpion;
    [SerializeField] private TextMeshPro damageText, defenceText, attackSpeedText, healthText, critRateText, critDamageText, blockChanceText, dodgeChanceText;
    [SerializeField] private GameObject damage, defence, attackSpeed, health, critRate, critDamage, blockChance, dodgeChance;
    [SerializeField] private GameObject buttonEquip;
    [SerializeField] private GameObject buttonSell;
    [SerializeField] private GameObject buttonUnEquip;


    private EquipmentPanel equipmentPanel;
    private ScriptableCharacter hero;
    private ItemScriptable item;
    private int slotIndex;
    private int itemInventoryIndex;

    
    private void Start()
    {
        detailsPanel.SetActive(false);
    }

    public void EquipmentDetails()
    {
        buttonEquip.SetActive(false);
        buttonSell.SetActive(false);
        itemInventoryIndex = transform.GetSiblingIndex();
        if (Input.mousePosition.x < (Screen.width / 2))
        {
            detailsPanel.transform.localPosition = new Vector3(4, detailsPanel.transform.localPosition.y, detailsPanel.transform.localPosition.z);
        }
        if (Input.mousePosition.y < (Screen.height / 2))
        {
            detailsPanel.transform.localPosition = new Vector3(detailsPanel.transform.localPosition.x, 4, detailsPanel.transform.localPosition.z);
        }
        detailsPanel.transform.SetParent(equipmentPanel.transform.parent);
        List<ItemScriptable> itemsToCheck = new List<ItemScriptable>();
        itemsToCheck.Clear();
        switch (item.slotPosition)
        {
            case SlotPosition.head:
                slotIndex = 0;
                ChangeText();
                break;
            case SlotPosition.armor:
                slotIndex = 1;
                ChangeText();
                break;
            case SlotPosition.shield:
                slotIndex = 3;
                ChangeText();
                break;
            case SlotPosition.meelWeapon:
                slotIndex = 2;
                ChangeText();
                break;
            case SlotPosition.twoHandedWeapon:
                slotIndex = 99;
                ChangeText();
                break;
            case SlotPosition.bow:
                slotIndex = 99;
                ChangeText();
                break;
            case SlotPosition.shoes:
                slotIndex = 4;
                ChangeText();
                break;
            case SlotPosition.accessories:
                slotIndex = 5;
                ChangeText();
                break;
        }
    }
    public void DisplayDetails()
    {
        buttonUnEquip.SetActive(false);
        itemInventoryIndex = transform.GetSiblingIndex();
        if (Input.mousePosition.x < (Screen.width / 2)) 
        {
            detailsPanel.transform.localPosition = new Vector3(4, detailsPanel.transform.localPosition.y, detailsPanel.transform.localPosition.z);
        }
        if (Input.mousePosition.y < (Screen.height / 2))
        {
            detailsPanel.transform.localPosition = new Vector3(detailsPanel.transform.localPosition.x, 4,  detailsPanel.transform.localPosition.z);
        }
        detailsPanel.transform.SetParent(equipmentPanel.transform.parent);
        List<ItemScriptable> itemsToCheck = new List<ItemScriptable>();
        itemsToCheck.Clear();
        switch (item.slotPosition)
        {
            case SlotPosition.head:
                slotIndex = 0;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[0]);
                AddToText(itemsToCheck);
                break;
            case SlotPosition.armor:
                slotIndex = 1;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[1]);
                AddToText(itemsToCheck);
                break;
            case SlotPosition.shield:
                slotIndex = 3;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[3]);
                AddToText(itemsToCheck);
                break;
            case SlotPosition.meelWeapon:
                slotIndex = 2;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[2]);
                AddToText(itemsToCheck);
                break;
            case SlotPosition.twoHandedWeapon:
                slotIndex = 99;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[2]);
                itemsToCheck.Add(hero.equipment.GetEquipment[3]);
                AddToText(itemsToCheck);
                break;
            case SlotPosition.bow:
                slotIndex = 99;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[2]);
                itemsToCheck.Add(hero.equipment.GetEquipment[3]);
                AddToText(itemsToCheck);
                break;
            case SlotPosition.shoes:
                slotIndex = 4;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[4]);
                AddToText(itemsToCheck);
                break;
            case SlotPosition.accessories:
                slotIndex = 5;
                ChangeText();
                itemsToCheck.Add(hero.equipment.GetEquipment[5]);
                AddToText(itemsToCheck);
                break;
        }
        buttonSell.GetComponentInChildren<TextMeshProUGUI>().text = item.sellValue.ToString();
    }

    private void ClearText()
    {
        damageText.text = "";
        defenceText.text = "";
        attackSpeedText.text = "";
        healthText.text = "";
        critRateText.text = "";
        critDamageText.text = "";
        blockChanceText.text = "";
        dodgeChanceText.text = "";
    }

    private void ChangeText()
    {
        ClearText();
        rarity.text = item.rarity.ToString();
        itemName.text = item.name;
        descritpion.text = item.description;

        if (item != null)
        {
            detailsPanel.SetActive(true);
            if (item.armor != 0)
            {
                defence.SetActive(true);
                defenceText.text = item.armor.ToString();
            }
            if (item.damage != 0)
            {
                damage.SetActive(true);
                damageText.text = item.damage.ToString();
            }
            if (item.attackRate != 0)
            {
                attackSpeed.SetActive(true);
                attackSpeedText.text = item.attackRate.ToString();
            }
            if (item.hp != 0)
            {
                health.SetActive(true);
                healthText.text = item.hp.ToString();
            }
            if (item.critChance != 0)
            {
                critRate.SetActive(true);
                critRateText.text = item.critChance.ToString();
            }
            if (item.critDamage != 0)
            {
                critDamage.SetActive(true);
                critDamageText.text = item.critDamage.ToString();
            }
            if (item.blockChance != 0)
            {
                blockChance.SetActive(true);
                blockChanceText.text = item.blockChance.ToString();
            }
            if (item.dodgeChance != 0)
            {
                dodgeChance.SetActive(true);
                dodgeChanceText.text = item.dodgeChance.ToString();
            }
        } 
    }

    public void AddToText(List<ItemScriptable> items)
    {
        if (items[0] != null && (this.item.slotPosition != SlotPosition.twoHandedWeapon || this.item.slotPosition != SlotPosition.bow))
        {
            detailsPanel.SetActive(true);
            if (items[0].armor != 0 || this.item.armor != 0)
            {
                defence.SetActive(true);
                defenceText.text += "(" + (this.item.armor - items[0].armor) + ")";
            }
            if (items[0].damage != 0 || this.item.damage != 0)
            {
                damage.SetActive(true);
                damageText.text += "(" + (this.item.damage - items[0].damage) + ")";
            }
            if (items[0].attackRate != 0 || this.item.attackRate != 0)
            {
                attackSpeed.SetActive(true);
                attackSpeedText.text += "(" + (this.item.attackRate - items[0].attackRate) + ")";
            }
            if (items[0].hp != 0 || this.item.hp != 0)
            {
                health.SetActive(true);
                healthText.text += "(" + (this.item.hp - items[0].hp) + ")";
            }
            if (items[0].critChance != 0 || this.item.critChance != 0)
            {
                critRate.SetActive(true);
                critRateText.text += "(" + (this.item.critChance - items[0].critChance) + ")";
            }
            if (items[0].critDamage != 0 || this.item.critDamage != 0)
            {
                critDamage.SetActive(true);
                critDamageText.text += "(" + (this.item.critDamage - items[0].critDamage) + ")";
            }
            if (items[0].blockChance != 0 || this.item.blockChance != 0)
            {
                blockChance.SetActive(true);
                blockChanceText.text += "(" + (this.item.blockChance - items[0].blockChance) + ")";
            }
            if (items[0].dodgeChance != 0 || this.item.dodgeChance != 0)
            {
                dodgeChance.SetActive(true);
                dodgeChanceText.text += "(" + (this.item.dodgeChance - items[0].dodgeChance) + ")";
            }
        } else if (items[0] != null && (this.item.slotPosition == SlotPosition.twoHandedWeapon || this.item.slotPosition == SlotPosition.bow))
        {
            detailsPanel.SetActive(true);
            if (items[0].armor != 0 || items[1].armor != 0 || this.item.armor != 0)
            {
                int temp = 0;
                if(items[0] != null)
                {
                    temp = items[0].armor;
                }
                if(items[1] != null)
                {
                    temp += items[1].armor;
                }
                defence.SetActive(true);
                defenceText.text += "(" + (this.item.armor - temp) + ")";
            }
            if (items[0].damage != 0 || items[1].damage != 0 || this.item.damage != 0)
            {
                int temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].damage;
                }
                if (items[1] != null)
                {
                    temp += items[1].damage;
                }
                damage.SetActive(true);
                damageText.text += "(" + (this.item.damage - temp) + ")";
            }
            if (items[0].attackRate != 0 || items[1].attackRate != 0 || this.item.attackRate != 0)
            {
                int temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].attackRate;
                }
                if (items[1] != null)
                {
                    temp += items[1].attackRate;
                }
                attackSpeed.SetActive(true);
                attackSpeedText.text += "(" + (this.item.attackRate - temp) + ")";
            }
            if (items[0].hp != 0 || items[1].hp != 0 || this.item.hp != 0)
            {
                int temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].hp;
                }
                if (items[1] != null)
                {
                    temp += items[1].hp;
                }
                health.SetActive(true);
                healthText.text += "(" + (this.item.hp - temp) + ")";
            }
            if (items[0].critChance != 0 || items[1].critChance != 0 || this.item.critChance != 0)
            {
                float temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].critChance;
                }
                if (items[1] != null)
                {
                    temp += items[1].critChance;
                }
                critRate.SetActive(true);
                critRateText.text += "(" + (this.item.critChance - temp) + ")";
            }
            if (items[0].critDamage != 0 || items[1].critDamage != 0 || this.item.critDamage != 0)
            {
                float temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].critDamage;
                }
                if (items[1] != null)
                {
                    temp += items[1].critDamage;
                }
                critDamage.SetActive(true);
                critDamageText.text += "(" + (this.item.critDamage - temp) + ")";
            }
            if (items[0].blockChance != 0 || items[1].blockChance != 0 || this.item.blockChance != 0)
            {
                int temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].blockChance;
                }
                if (items[1] != null)
                {
                    temp += items[1].blockChance;
                }
                blockChance.SetActive(true);
                blockChanceText.text += "(" + (this.item.blockChance - temp) + ")";
            }
            if (items[0].dodgeChance != 0 || items[1].dodgeChance != 0 || this.item.dodgeChance != 0)
            {
                int temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].dodgeChance;
                }
                if (items[1] != null)
                {
                    temp += items[1].dodgeChance;
                }
                dodgeChance.SetActive(true);
                dodgeChanceText.text += "(" + (this.item.dodgeChance - temp) + ")";
            }
        }
    }

    public void SetItemAndEquipment(ItemScriptable item, ScriptableCharacter hero, EquipmentPanel equipmentPanel)
    {
        this.equipmentPanel = equipmentPanel;
        if(item != null && hero != null)
        {
            this.item = item;
            this.hero = hero;
        }    
    }

    public void SetItem(ItemScriptable item)
    {
        this.item = item;
    }

    public void HideDetailsPanel()
    {
        StartCoroutine(HideDetail());
    }

    IEnumerator HideDetail()
    {
        yield return new WaitForSeconds(.1f);
        detailsPanel.SetActive(false);
    }


    public void EquipItem()
    {
        StopAllCoroutines();
        itemInventoryIndex = transform.GetSiblingIndex();
        
        if (slotIndex < 10)
        {
            if(hero.equipment.GetEquipment[slotIndex] != null)
            {
                equipmentPanel.EquipAndUnEquip(item, hero.equipment.GetEquipment[slotIndex], slotIndex, itemInventoryIndex);
                return;
            }
            equipmentPanel.EquipItem(item, slotIndex, itemInventoryIndex, detailsPanel);
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
                equipmentPanel.EquipItem(item, 2, itemInventoryIndex, detailsPanel);
            }  
        }
        detailsPanel.SetActive(false);
    }

    public void UnEquip()
    {
        StopAllCoroutines();
        equipmentPanel.UnEquipItem(item, slotIndex);
        detailsPanel.SetActive(false);
    }

    public void SellItem()
    {
        StopAllCoroutines();
        itemInventoryIndex = transform.GetSiblingIndex();
        equipmentPanel.SellItem(itemInventoryIndex);
        detailsPanel.SetActive(false);
    }
    
}
