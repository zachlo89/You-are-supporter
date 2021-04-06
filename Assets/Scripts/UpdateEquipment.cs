using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEquipment : MonoBehaviour
{
    [SerializeField] private Equipment equipment;
    [SerializeField] private List<SpriteRenderer> armor = new List<SpriteRenderer>();
    [SerializeField] private SpriteRenderer weaponRight;
    [SerializeField] private SpriteRenderer shield;
    [SerializeField] private SpriteRenderer twoHandedWeapon;
    [SerializeField] private List<SpriteRenderer> legs = new List<SpriteRenderer>();
    [SerializeField] private SpriteRenderer helment;
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private List<Sprite> defaultEquipment = new List<Sprite>();
    [SerializeField] private UpdateFaceAndBody updateFaceAndBody;

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
                    if(updateFaceAndBody != null)
                    {
                        armor[1].sprite = item.images[2];
                        armor[3].sprite = item.images[4];
                        armor[5].sprite = item.images[6];
                    }
                    break;
                case 2:
                    weaponRight.sprite = item.images[0];
                    break;
                case 3:
                    shield.sprite = item.images[0];
                    break;
                case 99:
                    twoHandedWeapon.sprite = item.images[0];
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
                    {
                        armor[1].sprite = defaultEquipment[2];
                        armor[3].sprite = defaultEquipment[4];
                        armor[5].sprite = defaultEquipment[6];
                    }
                    break;
                case 2:
                    weaponRight.sprite = null;
                    break;
                case 3:
                    shield.sprite = null;
                    break;
                case 99:
                    twoHandedWeapon.sprite = null;
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
