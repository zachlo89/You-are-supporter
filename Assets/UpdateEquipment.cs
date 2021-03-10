using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEquipment : MonoBehaviour
{
    [SerializeField] private Equipment equipment;
    [SerializeField] private List<SpriteRenderer> armor = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> weaponLeft = new List<SpriteRenderer>();
    [SerializeField] private SpriteRenderer weapoenRight;
    [SerializeField] private List<SpriteRenderer> legs = new List<SpriteRenderer>();
    [SerializeField] private SpriteRenderer helment;
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private List<Sprite> defaultEquipment = new List<Sprite>();


    public void Equip(ItemScriptable item, int slotPosition)
    {
        if(item != null)
        {
            switch (slotPosition)
            {
                case 0:
                    helment.sprite = item.images[0];
                    hair.enabled = false;
                    break;
                case 1:
                    for (int i = 0; i < item.images.Count; i++)
                    {
                        armor[i].sprite = item.images[i];
                    }
                    break;
                case 2:
                    if (item.slotPosition == SlotPosition.meelWeapon)
                    {
                        weaponLeft[0].sprite = item.images[0];
                        weaponLeft[1].sprite = null;
                    }
                    else
                    {
                        weaponLeft[1].sprite = item.images[0];
                        weaponLeft[0].sprite = null;
                    }
                    break;
                case 3:
                    weapoenRight.sprite = item.images[0];
                    break;
                case 4:
                    legs[0].sprite = item.images[0];
                    legs[1].sprite = item.images[0];
                    legs[2].sprite = item.images[1];
                    legs[3].sprite = item.images[1];
                    break;
            }
        }

        if(item == null)
        {
            switch (slotPosition)
            {
                case 0:
                    helment.sprite = null;
                    hair.enabled = true;
                    break;
                case 1:
                    for (int i = 0; i < 8; i++)
                    {
                        armor[i].sprite = defaultEquipment[i];
                    }
                    break;
                case 2:
                    weaponLeft[0].sprite = null;
                    weaponLeft[1].sprite = null;

                    break;
                case 3:
                    weapoenRight.sprite = null;
                    break;
                case 4:
                    legs[0].sprite = defaultEquipment[8];
                    legs[1].sprite = defaultEquipment[8];
                    legs[2].sprite = defaultEquipment[9];
                    legs[3].sprite = defaultEquipment[9];
                    break;
            }
        }
    }

    public void EquipAll(Equipment equipment)
    {
        if(equipment != null)
        {
            this.equipment = equipment;
            for (int i = 0; i < equipment.GetEquipment.Count; i++)
            {
                Equip(equipment.GetEquipment[i], i);
            }
        }
    }

}
