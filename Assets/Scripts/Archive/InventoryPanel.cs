using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private GameObject parentLocation;
    [SerializeField] private GameObject rightPanel;
    [SerializeField] private TextMeshProUGUI goldText;
    private ItemScriptable itemClicked;
    private GameObject iconClicked;


    //Right Panel
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private TextMeshProUGUI rarity;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI descritpion;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI damageText, defenceText, attackSpeedText, healthText;
    [SerializeField] private GameObject damage, defence, attackSpeed, health;


    private void Start()
    {
        rightPanel.SetActive(false);
        goldText.text = inventory.Gold.value.ToString();
        PopulateInventory();
    }


    public void PopulateInventory()
    {
        foreach (Transform child in parentLocation.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        inventory.GetInvevtory.Sort(delegate (ItemScriptable x, ItemScriptable y)
        {
            return y.rarity.CompareTo(x.rarity);
        });


        for (int i = 0; i < inventory.GetInvevtory.Count; i++)
        {
            GameObject temp = Instantiate(iconPrefab, parentLocation.transform);
            temp.GetComponent<SetItemIcon>().UpdateIconUI(inventory.GetInvevtory[i]);
            temp.GetComponent<SetItemIcon>().MakeDeselection(rightPanel);
        }
    }

    public void UpdateRightPanel(ItemScriptable item, GameObject iconClicked)
    {
        this.iconClicked = iconClicked;
        itemClicked = item;
        if (itemClicked != null)
        {
            rightPanel.SetActive(true);
            foreach (Transform child in spawningPoint)
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject temp = Instantiate(iconPrefab, spawningPoint);
            temp.GetComponent<SetItemIcon>().UpdateIconUI(item);
            rarity.text = itemClicked.rarity.ToString();
            itemName.text = itemClicked.name;
            descritpion.text = itemClicked.description;
            goldValue.text = itemClicked.sellValue.ToString();
            if (itemClicked.damage != 0)
            {
                damage.SetActive(true);
                damageText.text = itemClicked.damage.ToString();
            }
            else damage.SetActive(false);
            if (itemClicked.armor != 0)
            {
                defence.SetActive(true);
                defenceText.text = itemClicked.armor.ToString();
            }
            else defence.SetActive(false);
            if (itemClicked.attackRate != 0)
            {
                attackSpeed.SetActive(true);
                attackSpeedText.text = itemClicked.attackRate.ToString();
            }
            else attackSpeed.SetActive(false);
            if (itemClicked.hp != 0)
            {
                health.SetActive(true);
                healthText.text = itemClicked.hp.ToString();
            }
            else health.SetActive(false);
        }
        else rightPanel.SetActive(false);
    }

    public void SellItem()
    {
        if(itemClicked != null)
        {
            //inventory.SellItem(itemClicked);
            Destroy(iconClicked);
            UpdateRightPanel(null, null);
            goldText.text = inventory.Gold.value.ToString();
        }
    }

    public void SortItems(int j)
    {
        foreach (Transform child in parentLocation.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        switch (j){
            case 0:
                for (int i = 0; i < inventory.GetInvevtory.Count; i++)
                {
                    if (inventory.GetInvevtory[i].slotPosition == SlotPosition.meelWeapon ||
                        inventory.GetInvevtory[i].slotPosition == SlotPosition.twoHandedWeapon ||
                        inventory.GetInvevtory[i].slotPosition == SlotPosition.shield)
                    {
                        GameObject temp = Instantiate(iconPrefab, parentLocation.transform);
                        temp.GetComponent<SetItemIcon>().UpdateIconUI(inventory.GetInvevtory[i]);
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
                    }
                }
                break;
            default:
                PopulateInventory();
                break;
        }
    }
}
