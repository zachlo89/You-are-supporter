using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DetailsPanel : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private TextMeshPro rarity;
    [SerializeField] private TextMeshPro itemName;
    [SerializeField] private TextMeshPro descritpion;
    [SerializeField] private TextMeshPro damageText, defenceText, attackSpeedText, healthText, critRateText, critDamageText, blockChanceText, dodgeChanceText;
    [SerializeField] private GameObject damage, defence, attackSpeed, health, critRate, critDamage, blockChance, dodgeChance;
    [SerializeField] private GameObject buttonEquip;
    [SerializeField] private GameObject buttonSell;
    [SerializeField] private GameObject buttonUnEquip;
    [SerializeField] private EquipmentPanel equipmentPanel;


    private CompareAndDisplayDetails compareAndDisplayDetails;
    private ScriptableCharacter hero;
    private ItemScriptable item;
    private int slotIndex;


    private void Start()
    {
        cam = Camera.main;
    }

    public void SetUpDetailsPanel(ScriptableCharacter hero)
    {
        this.hero = hero;
    }

    private Vector3 SetPosition()
    {
        Vector3 newPosition;
        float halfWidth = Input.mousePosition.x - Screen.width / 2;
        float multiplyerX = 3040 / Screen.width;
        float halfHeight = Input.mousePosition.y - Screen.height / 2;
        float offsetX = (GetComponent<RectTransform>().sizeDelta.x / 2) + (compareAndDisplayDetails.GetComponent<RectTransform>().sizeDelta.x / 2);
        if (halfWidth < 0)
        {
            newPosition = new Vector3(halfWidth * multiplyerX + offsetX, Mathf.Clamp(halfHeight, -180, 320), 0);
        }
        else newPosition = new Vector3(halfWidth * multiplyerX - offsetX, Mathf.Clamp(halfHeight, -180, 320), 0);
        return newPosition;
    }

    public void EquipmentDetails(ItemScriptable item, CompareAndDisplayDetails compareAndDisplayDetails)
    {
        this.item = item;
        this.compareAndDisplayDetails = compareAndDisplayDetails;
        transform.localPosition = SetPosition();
        buttonUnEquip.SetActive(true);
        buttonEquip.SetActive(false);
        buttonSell.SetActive(false);
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

    public void DisplayDetails(ItemScriptable item, CompareAndDisplayDetails compareAndDisplayDetails)
    {
        this.item = item;
        this.compareAndDisplayDetails = compareAndDisplayDetails;
        transform.localPosition = SetPosition();
        buttonUnEquip.SetActive(false);
        buttonEquip.SetActive(true);
        buttonSell.SetActive(true);
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
            if (item.armor != 0)
            {
                defence.SetActive(true);
                defenceText.text = item.armor.ToString();
            }
            else defence.SetActive(false);
            if (item.damage != 0)
            {
                damage.SetActive(true);
                damageText.text = item.damage.ToString();
            }
            else damage.SetActive(false);
            if (item.attackRate != 0)
            {
                attackSpeed.SetActive(true);
                attackSpeedText.text = item.attackRate.ToString();
            }
            else attackSpeed.SetActive(false);
            if (item.hp != 0)
            {
                health.SetActive(true);
                healthText.text = item.hp.ToString();
            }else health.SetActive(false);
            if (item.critChance != 0)
            {
                critRate.SetActive(true);
                critRateText.text = item.critChance.ToString();
            } else critRate.SetActive(false);
            if (item.critDamage != 0)
            {
                critDamage.SetActive(true);
                critDamageText.text = item.critDamage.ToString();
            } else critDamage.SetActive(false);
            if (item.blockChance != 0)
            {
                blockChance.SetActive(true);
                blockChanceText.text = item.blockChance.ToString();
            } else blockChance.SetActive(false);
            if (item.dodgeChance != 0)
            {
                dodgeChance.SetActive(true);
                dodgeChanceText.text = item.dodgeChance.ToString();
            } else dodgeChance.SetActive(false);
        }
    }

    public void AddToText(List<ItemScriptable> items)
    {
        if (items[0] != null && (this.item.slotPosition != SlotPosition.twoHandedWeapon || this.item.slotPosition != SlotPosition.bow))
        {
            if (items[0].armor != 0 || this.item.armor != 0)
            {
                defence.SetActive(true);
                if (this.item.armor == 0)
                {
                    defenceText.text += "0";
                }
                if (this.item.armor < items[0].armor)
                {
                    defenceText.text += "<color=red> (" + (this.item.armor - items[0].armor) + ")";
                }
                else defenceText.text += "<color=green> (" + (this.item.armor - items[0].armor) + ")";

            }
            if (items[0].damage != 0 || this.item.damage != 0)
            {
                damage.SetActive(true);
                if (this.item.damage == 0)
                {
                    damageText.text += "0";
                }
                if (this.item.damage < items[0].damage)
                {
                    damageText.text += "<color=red> (" + (this.item.damage - items[0].damage) + ")";
                }
                else damageText.text += "<color=green> (" + (this.item.damage - items[0].damage) + ")";
            }
            if (items[0].attackRate != 0 || this.item.attackRate != 0)
            {
                attackSpeed.SetActive(true);
                if (this.item.attackRate == 0)
                {
                    attackSpeedText.text += "0";
                }
                if (this.item.attackRate < items[0].attackRate)
                {
                    attackSpeedText.text += "<color=red> (" + (this.item.attackRate - items[0].attackRate) + ")";
                }
                else attackSpeedText.text += "<color=green> (" + (this.item.attackRate - items[0].attackRate) + ")";
            }
            if (items[0].hp != 0 || this.item.hp != 0)
            {
                health.SetActive(true);
                if (this.item.hp == 0)
                {
                    healthText.text += "0";
                }
                if (this.item.hp < items[0].hp)
                {
                    healthText.text += "<color=red> (" + (this.item.hp - items[0].hp) + ")";
                }
                else healthText.text += "<color=green> (" + (this.item.hp - items[0].hp) + ")";
            }
            if (items[0].critChance != 0 || this.item.critChance != 0)
            {
                critRate.SetActive(true);
                if (this.item.critChance == 0)
                {
                    critRateText.text += "0";
                }
                if (this.item.critChance < items[0].critChance)
                {
                    critRateText.text += "<color=red> (" + (this.item.critChance - items[0].critChance) + ")";
                }
                else critRateText.text += "<color=green> (" + (this.item.critChance - items[0].critChance) + ")";
            }
            if (items[0].critDamage != 0 || this.item.critDamage != 0)
            {
                critDamage.SetActive(true);
                if (this.item.critDamage == 0)
                {
                    critDamageText.text += "0";
                }
                if (this.item.critDamage < items[0].critDamage)
                {
                    critDamageText.text += "<color=red> (" + (this.item.critDamage - items[0].critDamage) + ")";
                }
                else critDamageText.text += "<color=green> (" + (this.item.critDamage - items[0].critDamage) + ")";
            }
            if (items[0].blockChance != 0 || this.item.blockChance != 0)
            {
                blockChance.SetActive(true);
                if (this.item.blockChance == 0)
                {
                    blockChanceText.text += "0";
                }
                if (this.item.blockChance < items[0].blockChance)
                {
                    blockChanceText.text += "<color=red> (" + (this.item.blockChance - items[0].blockChance) + ")";
                }
                else blockChanceText.text += "<color=green> (" + (this.item.blockChance - items[0].blockChance) + ")";
            }
            if (items[0].dodgeChance != 0 || this.item.dodgeChance != 0)
            {
                dodgeChance.SetActive(true);
                if (this.item.dodgeChance == 0)
                {
                    dodgeChanceText.text += "0";
                }
                if (this.item.dodgeChance < items[0].dodgeChance)
                {
                    dodgeChanceText.text += "<color=red> (" + (this.item.dodgeChance - items[0].dodgeChance) + ")";
                }
                else dodgeChanceText.text += "<color=green> (" + (this.item.dodgeChance - items[0].dodgeChance) + ")";
            }
        }
        else if (items[0] != null && (this.item.slotPosition == SlotPosition.twoHandedWeapon || this.item.slotPosition == SlotPosition.bow))
        {
            if (items[0].armor != 0 || items[1].armor != 0 || this.item.armor != 0)
            {
                int temp = 0;
                if (items[0] != null)
                {
                    temp = items[0].armor;
                }
                if (items[1] != null)
                {
                    temp += items[1].armor;
                }
                defence.SetActive(true);
                if (this.item.armor == 0)
                {
                    defenceText.text += "0";
                }
                if (this.item.armor < items[0].armor)
                {
                    defenceText.text += "<color=red> (" + (this.item.armor - temp) + ")";
                }
                else defenceText.text += "<color=green> (" + (this.item.armor - temp) + ")";
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
                if (this.item.damage == 0)
                {
                    damageText.text += "0";
                }
                if (this.item.damage < items[0].damage)
                {
                    damageText.text += "<color=red> (" + (this.item.damage - temp) + ")";
                }
                else damageText.text += "<color=green> (" + (this.item.damage - temp) + ")";
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
                if (this.item.attackRate == 0)
                {
                    attackSpeedText.text += "0";
                }
                if (this.item.attackRate < items[0].attackRate)
                {
                    attackSpeedText.text += "<color=red> (" + (this.item.attackRate - temp) + ")";
                }
                else attackSpeedText.text += "<color=green> (" + (this.item.attackRate - temp) + ")";
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
                if (this.item.hp == 0)
                {
                    healthText.text += "0";
                }
                if (this.item.hp < items[0].hp)
                {
                    healthText.text += "<color=red> (" + (this.item.hp - temp) + ")";
                }
                else healthText.text += "<color=green> (" + (this.item.hp - temp) + ")";
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
                if (this.item.critChance == 0)
                {
                    critRateText.text += "0";
                }
                if (this.item.critChance < items[0].critChance)
                {
                    critRateText.text += "<color=red> (" + (this.item.critChance - temp) + ")";
                }
                else critRateText.text += "<color=green> (" + (this.item.critChance - temp) + ")";
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
                if (this.item.critDamage == 0)
                {
                    critDamageText.text += "0";
                }
                if (this.item.critDamage < items[0].critDamage)
                {
                    critDamageText.text += "<color=red> (" + (this.item.critDamage - temp) + ")";
                }
                else critDamageText.text += "<color=green> (" + (this.item.critDamage - temp) + ")";
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
                if (this.item.blockChance == 0)
                {
                    blockChanceText.text += "0";
                }
                if (this.item.blockChance < items[0].blockChance)
                {
                    blockChanceText.text += "<color=red> (" + (this.item.blockChance - temp) + ")";
                }
                else blockChanceText.text += "<color=green> (" + (this.item.blockChance - temp) + ")";
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
                if (this.item.dodgeChance == 0)
                {
                    dodgeChanceText.text += "0";
                }
                if (this.item.dodgeChance < items[0].dodgeChance)
                {
                    dodgeChanceText.text += "<color=red> (" + (this.item.dodgeChance - temp) + ")";
                }
                else dodgeChanceText.text += "<color=green> (" + (this.item.dodgeChance - temp) + ")";
            }
        }
    }

    public void Equip()
    {
        compareAndDisplayDetails.EquipItem(slotIndex);
        gameObject.SetActive(false);
    }

    public void Unequip()
    {
        compareAndDisplayDetails.UnEquip(slotIndex);
        gameObject.SetActive(false);
    }

    public void SellItem()
    {
        compareAndDisplayDetails.SellItem();
        gameObject.SetActive(false);
    }


}
