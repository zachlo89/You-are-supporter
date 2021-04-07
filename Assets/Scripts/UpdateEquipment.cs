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
    [SerializeField] private SpriteRenderer staff;
    [SerializeField] private List<SpriteRenderer> bow = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> legs = new List<SpriteRenderer>();
    [SerializeField] private SpriteRenderer helment;
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private List<Sprite> defaultEquipment = new List<Sprite>();
    [SerializeField] private Animator animator;

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
                    armor[8].sprite = armor[3].sprite;
                    armor[9].sprite = armor[2].sprite;
                    armor[10].sprite = armor[4].sprite;
                    armor[11].sprite = armor[5].sprite;
                    armor[12].sprite = armor[6].sprite;
                    break;
                case 2:
                    animator.SetBool("hasWeapon", true);
                    if (item.slotPosition == SlotPosition.meelWeapon)
                    {
                        weaponRight.sprite = item.images[0];
                    }
                    else if (item.slotPosition == SlotPosition.staff) {
                        staff.sprite = item.images[0];
                    } else twoHandedWeapon.sprite = item.images[0];
                    break;
                case 3:
                    animator.SetBool("hasWeapon", true);
                    if(item.slotPosition == SlotPosition.bow)
                    {
                        bow[0].sprite = item.images[0];
                        bow[1].sprite = item.images[1];
                        bow[2].sprite = item.images[1];
                    } else
                    {
                        shield.sprite = item.images[0];
                    }
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
                    if(equipment.GetEquipment[3] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                    }
                    weaponRight.sprite = null;
                    break;
                case 3:
                    if (equipment.GetEquipment[2] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                    }
                    shield.sprite = null;
                    break;
                case 99:
                    if (equipment.GetEquipment[3] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                    }
                    twoHandedWeapon.sprite = null;
                    break;
                case 98:
                    if (equipment.GetEquipment[2] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                    }
                    bow[0].sprite = null;
                    bow[1].sprite = null;
                    bow[2].sprite = null;
                    break;
                case 97:
                    if (equipment.GetEquipment[3] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                    }
                    staff.sprite = null;
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
