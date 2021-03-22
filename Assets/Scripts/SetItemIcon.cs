using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetItemIcon : MonoBehaviour
{
    private ItemScriptable item;
    private InventoryPanel inventoryPanel;
    [SerializeField] private List<SpriteRenderer> itemImages = new List<SpriteRenderer>();
    private GameObject rightPanel;


    private void Start()
    {
        inventoryPanel = GameObject.FindObjectOfType<InventoryPanel>();
    }
    public ItemScriptable GetItem
    {
        get { return item; }
    }

    [SerializeField] private SpriteRenderer border;
    [SerializeField] private GameObject selected;
    public GameObject Selected
    {
        get { return selected; }
    }

    public void MakeDeselection(GameObject rightPanel)
    {
        this.rightPanel = rightPanel;
    }


    public void UpdateIconUI(ItemScriptable item)
    {
        this.item = item;
        ClearIcon();
        switch (item.slotPosition)
        {
            case SlotPosition.armor:
                itemImages[0].sprite = item.images[0];
                itemImages[1].sprite = item.images[1];
                itemImages[2].sprite = item.images[2];
                itemImages[3].sprite = item.images[7];
                break;
            case SlotPosition.meelWeapon:
                itemImages[4].sprite = item.images[0];
                break;
            case SlotPosition.twoHandedWeapon:
                itemImages[4].sprite = item.images[0];
                break;
            case SlotPosition.shield:
                itemImages[5].sprite = item.images[0];
                break;
            case SlotPosition.head:
                itemImages[6].sprite = item.images[0];
                break;
            case SlotPosition.shoes:
                itemImages[7].sprite = item.images[1];
                itemImages[8].sprite = item.images[1];
                break;

        }

        if (item.border != null)
        {
            border.sprite = item.border;
        }

        
        selected.SetActive(false);
    }

    public void UpdateRightPanel()
    {
        if(inventoryPanel != null)
        {
            inventoryPanel.UpdateRightPanel(item, gameObject);
        }
    }

    private void ClearIcon()
    {
        foreach(SpriteRenderer sprite in itemImages)
        {
            sprite.sprite = null;
        }
    }

    public void OnDeselect()
    {
        if(rightPanel != null)
        {
            rightPanel.SetActive(false);
        }
    }
}
