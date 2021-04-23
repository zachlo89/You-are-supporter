using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private Image armR, armL, accessories;
    [SerializeField] private List<Sprite> iconsList = new List<Sprite>();
    [SerializeField] private Sprite possibleEquipment, disabledEquipment;
    [SerializeField] private GameObject detailsPanel;
    private ScriptableCharacter hero;
    [SerializeField] private List<GameObject> equipmentList = new List<GameObject>();
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expirenceText;
    [SerializeField] private Slider expirienceSlider;
    [SerializeField] private TextMeshProUGUI damage, armor, attackSpeed, maxHP, critChanceText, critDamageText, blockChanceText, dodgeChanceText;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private EquipmentInventory equipmentInventory;
    private GameObject heroPrefab;
    int damageValue;
    int armorValue;
    int attackSpeedValue;
    int maxHealth;
    float critChance;
    float critDamageMultiplay;
    int blockChance;
    int dodgeChance;

    public ScriptableCharacter GetHero()
    {
        return hero;
    }

    public void UpdateUI(ScriptableCharacter hero)
    {
        detailsPanel.GetComponent<DetailsPanel>().SetUpDetailsPanel(hero);
        for(int i = 0; i < stars.Count; i++)
        {
            stars[i].SetActive(false);
        }
        this.hero = hero;
        heroPrefab = hero.prefab;

        if(hero.characterClass == CharacterClass.Archer)
        {
            armR.sprite = iconsList[3];
            armR.transform.parent.GetComponent<Image>().sprite = disabledEquipment;
            armL.sprite = iconsList[0];
            armL.transform.parent.GetComponent<Image>().sprite = possibleEquipment;
        } else if (hero.characterClass == CharacterClass.Berserker || hero.characterClass == CharacterClass.Supporter)
        {
            armL.sprite = iconsList[3];
            armL.transform.parent.GetComponent<Image>().sprite = disabledEquipment;
            armR.sprite = iconsList[0];
            armR.transform.parent.GetComponent<Image>().sprite = possibleEquipment;
        } else
        {
            armL.sprite = iconsList[1];
            armL.transform.parent.GetComponent<Image>().sprite = possibleEquipment;
            armR.sprite = iconsList[0];
            armR.transform.parent.GetComponent<Image>().sprite = possibleEquipment;
        }

        UpdateEquipment();
       
        foreach (Transform child in spawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        heroPrefab = Instantiate(hero.prefab, spawningPoint);
        heroPrefab.GetComponent<UpdateFaceAndBody>().SetUpFace(hero);
        heroPrefab.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
        heroPrefab.GetComponentInChildren<Animator>().Play("IdleMelee");
        heroPrefab.GetComponentInChildren<Light2D>().enabled = false;
        heroPrefab.transform.localScale *= 2;
        heroName.text = hero.characterName;
        levelText.text = hero.level.ToString();
        expirenceText.text = hero.expirence.ToString() + "/" + hero.toNextLevel.ToString();
        expirienceSlider.value = (float)hero.expirence / (float)hero.toNextLevel;

        UpdateStatsUI();

        for(int i=0; i< hero.stars; i++)
        {
            stars[i].SetActive(true);
        }
        equipmentInventory.PopulateInventory();

    }

    private void UpdateEquipment()
    {
        for (int i = 0; i < equipmentList.Count; i++)
        {
            try
            {
                GameObject iconToRemove = equipmentList[i].gameObject.GetComponentInChildren<SetItemIcon>().gameObject;
                if (iconToRemove != null)
                {
                    Destroy(iconToRemove);
                }
            }
            catch
            {
            }
        }


        for (int i = 0; i < hero.equipment.GetEquipment.Count; i++)
        {
            if (hero.equipment.GetEquipment[i] != null)
            {
                GameObject item = Instantiate(itemPanel, equipmentList[i].transform);
                item.GetComponent<SetItemIcon>().UpdateIconUI(hero.equipment.GetEquipment[i]);
                item.GetComponent<CompareAndDisplayDetails>().SetItemAndEquipment(hero.equipment.GetEquipment[i], hero, this, detailsPanel);
            }

        }
    }

    private void UpdateStatsUI()
    {
        damageValue = hero.damage;
        armorValue = hero.armor;
        attackSpeedValue = hero.attackRate;
        maxHealth = hero.maxHealt;
        critChance = hero.critChance;
        critDamageMultiplay = hero.critDamageMultiplay;
        blockChance = hero.blockChance;
        dodgeChance = hero.dogdeChance;

        for (int i = 0; i<hero.equipment.GetEquipment.Count; i++)
        {
            if(hero.equipment.GetEquipment[i] != null)
            {
                damageValue += hero.equipment.GetEquipment[i].damage;
                armorValue += hero.equipment.GetEquipment[i].armor;
                attackSpeedValue -= hero.equipment.GetEquipment[i].attackRate;
                maxHealth += hero.equipment.GetEquipment[i].hp;
                critChance += hero.equipment.GetEquipment[i].critChance;
                critDamageMultiplay += hero.equipment.GetEquipment[i].critDamage;
                blockChance += hero.equipment.GetEquipment[i].blockChance;
                dodgeChance += hero.equipment.GetEquipment[i].dodgeChance;
            }
        }

        damage.text = damageValue.ToString();
        armor.text = armorValue.ToString();
        attackSpeed.text = ((float) attackSpeedValue / 100).ToString();
        maxHP.text = maxHealth.ToString();
        critChanceText.text = critChance.ToString();
        critDamageText.text = critDamageMultiplay.ToString();
        blockChanceText.text = blockChance.ToString(); ;
        dodgeChanceText.text = dodgeChance.ToString();
        heroPrefab.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
        delegator.changeSprites();
    }

    public void EquipItem(ItemScriptable item, int index, int indexToRemove)
    {
        if(item != null)
        {
            hero.equipment.AddItem(item, index);
            equipmentInventory.RemoveItem(indexToRemove);
            UpdateEquipment();
            UpdateStatsUI();
        }
    }

    public void UnEquipItem(ItemScriptable item, int index)
    {
        GameObject iconToRemove = equipmentList[index].gameObject.GetComponentInChildren<SetItemIcon>().gameObject;
        hero.equipment.RemoveItem(index);
        equipmentInventory.AddItem(item);
        Destroy(iconToRemove);
        UpdateStatsUI();
    }

    public void EquipAndUnEquip(ItemScriptable itemToEquip, ItemScriptable itemToUnEquip, int equipmentIndex, int inventoryIndex)
    {
        hero.equipment.RemoveItem(equipmentIndex);
        hero.equipment.AddItem(itemToEquip, equipmentIndex);
        equipmentInventory.SwapItems(itemToUnEquip, inventoryIndex);
        UpdateEquipment();
        UpdateStatsUI();
    }

    public void SellItem(int index)
    {
        equipmentInventory.SellItem(index);
        delegator.changeSprites();
    }

}
