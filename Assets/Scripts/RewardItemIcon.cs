using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemIcon : MonoBehaviour
{
    [SerializeField] private Image border;
    [SerializeField] private Image torso, armL, armR, pelvis, weapon, shield, helmet, shoeR, shoeL, staff, riser, limbL, limbR;

    public void PopulateRewardIcon(ItemScriptable item)
    {
        switch (item.slotPosition)
        {
            case SlotPosition.armor:
                torso.enabled = true;
                armL.enabled = true;
                armR.enabled = true;
                pelvis.enabled = true;
                torso.sprite = item.images[0];
                armL.sprite = item.images[1];
                armR.sprite = item.images[2];
                pelvis.sprite = item.images[7];
                border.sprite = item.border;
                break;
            case SlotPosition.meelWeapon:
                weapon.enabled = true;
                weapon.sprite = item.images[0];
                border.sprite = item.border;
                break;
            case SlotPosition.twoHandedWeapon:
                weapon.enabled = true;
                weapon.sprite = item.images[0];
                border.sprite = item.border;
                break;
            case SlotPosition.shield:
                shield.enabled = true;
                shield.sprite = item.images[0];
                border.sprite = item.border;
                break;
            case SlotPosition.head:
                helmet.enabled = true;
                helmet.sprite = item.images[0];
                border.sprite = item.border;
                break;
            case SlotPosition.shoes:
                shoeL.enabled = true;
                shoeR.enabled = true;
                shoeL.sprite = item.images[1];
                shoeR.sprite = item.images[1];
                border.sprite = item.border;
                break;
            case SlotPosition.staff:
                staff.enabled = true;
                staff.sprite = item.images[0];
                border.sprite = item.border;
                break;
            case SlotPosition.bow:
                riser.enabled = true;
                limbL.enabled = true;
                limbR.enabled = true;
                riser.sprite = item.images[0];
                limbL.sprite = item.images[1];
                limbR.sprite = item.images[1];
                border.sprite = item.border;
                break;

        }
    }
}
