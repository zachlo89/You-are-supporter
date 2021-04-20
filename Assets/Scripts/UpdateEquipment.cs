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
<<<<<<< Updated upstream
                    weaponRight.sprite = item.images[0];
=======
                    animator.SetBool("hasWeapon", true);
                    if (item.slotPosition == SlotPosition.meelWeapon)
                    {
                        weaponRight.sprite = item.images[0];
                    }
                    else if (item.slotPosition == SlotPosition.staff)
                    {
                        staff.sprite = item.images[0];
                        animationFunction.MagicMIssleConstructor(item.effects);
                    }
                    else
                    {
                        twoHandedWeapon.sprite = item.images[0];
                    }
                    try
                    {
                        DestroyFXEffects();
                        if(SceneManager.GetActiveScene().name == "GameScene")
                        {
                            twoHandedWeapon.transform.GetChild(0).gameObject.GetComponent<Assets.SimpleSpriteTrails.Scripts.MeleeWeaponTrail>().Build();
                            weaponRight.transform.GetChild(0).gameObject.GetComponent<Assets.SimpleSpriteTrails.Scripts.MeleeWeaponTrail>().Build();
                        }
                    }
                    catch
                    {

                    }
                    
                    
>>>>>>> Stashed changes
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
                    break;
                case 2:
<<<<<<< Updated upstream
                    weaponRight.sprite = null;
                    break;
                case 3:
                    shield.sprite = null;
                    break;
                case 99:
                    twoHandedWeapon.sprite = null;
                    break;
=======
                    if(equipment.GetEquipment[3] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                        DestroyFXEffects();
                    }
                    weaponRight.sprite = null;
                    break;
                case 3:
                    if (equipment.GetEquipment[2] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                        DestroyFXEffects();
                    }
                    shield.sprite = null;
                    break;
                case 99:
                    if (equipment.GetEquipment[3] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                        DestroyFXEffects();
                    }
                    twoHandedWeapon.sprite = null;
                    break;
                case 98:
                    if (equipment.GetEquipment[2] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                        DestroyFXEffects();
                    }
                    bow[0].sprite = null;
                    bow[1].sprite = null;
                    bow[2].sprite = null;
                    break;
                case 97:
                    if (equipment.GetEquipment[3] == null)
                    {
                        animator.SetBool("hasWeapon", false);
                        DestroyFXEffects();
                    }
                    staff.sprite = null;
                    break;
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======
    private void DestroyFXEffects()
    {
        StopAllCoroutines();
        try
        {
            Destroy(twoHandedWeapon.transform.GetChild(0).GetChild(0));
        }
        catch
        {
            Debug.Log("No effects to destroy");
        }
        try
        {
            Destroy(weaponRight.transform.GetChild(0).GetChild(0));
        }
        catch
        {
            Debug.Log("No effects to destroy");
        }
    }
>>>>>>> Stashed changes
}
