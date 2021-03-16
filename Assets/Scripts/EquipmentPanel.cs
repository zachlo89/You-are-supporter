using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    private ScriptableCharacter hero;
    [SerializeField] private List<GameObject> equipmentList = new List<GameObject>();
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expirenceText;
    [SerializeField] private Slider expirienceSlider;
    [SerializeField] private TextMeshProUGUI damage, armor, attackSpeed, maxHP;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    private EquipmentInventory equipmentInventory;
    private GameObject heroPrefab;
    int damageValue;
    int armorValue;
    int attackSpeedValue;
    int maxHealth;

    private void Start()
    {
        equipmentInventory = GetComponent<EquipmentInventory>();
    }


    public void UpdateUI(ScriptableCharacter hero)
    {
        for(int i = 0; i < stars.Count; i++)
        {
            stars[i].SetActive(false);
        }
        this.hero = hero;
        heroPrefab = hero.prefab;

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
            if(hero.equipment.GetEquipment[i] != null)
            {
                GameObject item = Instantiate(itemPanel, equipmentList[i].transform);
                item.GetComponent<SetItemIcon>().UpdateIconUI(hero.equipment.GetEquipment[i]);
                item.AddComponent<DraggableComponent>();
                Destroy(item.GetComponent<Button>());
                Destroy(item.GetComponent<EventTrigger>());
            }
            
        }
        foreach (Transform child in spawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        heroPrefab = Instantiate(hero.prefab, spawningPoint);
        heroPrefab.GetComponent<UpdateFaceAndBody>().SetUpFace(hero);
        heroPrefab.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
        heroPrefab.transform.localScale *= 2;
        heroName.text = hero.name;
        levelText.text = hero.level.ToString();
        expirenceText.text = hero.expirence.ToString() + "/" + hero.toNextLevel.ToString();
        expirienceSlider.value = (float)hero.expirence / (float)hero.toNextLevel;

        UpdateStatsUI();

        for(int i=0; i< hero.stars; i++)
        {
            stars[i].SetActive(true);
        }
    }

    private void UpdateStatsUI()
    {

        damageValue = hero.damage;
        armorValue = hero.armor;
        attackSpeedValue = hero.attackRate;
        maxHealth = hero.maxHealt;
        AdjustStatsToLevel();

        for (int i = 0; i<hero.equipment.GetEquipment.Count; i++)
        {
            if(hero.equipment.GetEquipment[i] != null)
            {
                damageValue += hero.equipment.GetEquipment[i].damage;
                armorValue += hero.equipment.GetEquipment[i].armor;
                attackSpeedValue += hero.equipment.GetEquipment[i].attackRate;
                maxHealth += hero.equipment.GetEquipment[i].hp;
            }
        }

        damage.text = damageValue.ToString();
        armor.text = armorValue.ToString();
        attackSpeed.text = ((float) attackSpeedValue / 100).ToString();
        maxHP.text = maxHealth.ToString();
        heroPrefab.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
        delegator.changeSprites();
    }

    private void AdjustStatsToLevel()
    {
        switch (hero.characterClass)
        {
            case CharacterClass.Tank:
                for (int i = 0; i < hero.level; i++)
                {
                    maxHealth += (int)(maxHealth * 8 / 100);
                    damageValue += (int)(damageValue * 6 / 100);
                }
                break;
            case CharacterClass.Archer:
                maxHealth += (int)(maxHealth * 6 / 100);
                damageValue += (int)(damageValue * 8 / 100);
                break;
            case CharacterClass.Berserker:
                maxHealth += (int)(maxHealth * 7 / 100);
                damageValue += (int)(damageValue * 7 / 100);
                break;
        }
    }

    public void EquipItem(ItemScriptable item, int index, int indexToRemove)
    {
        if(item != null)
        {
            hero.equipment.AddItem(item, index);
            equipmentInventory.RemoveItem(indexToRemove);
            UpdateStatsUI();
        }
    }

    public void UnEquipItem(ItemScriptable item, int index)
    {
        hero.equipment.RemoveItem(item, index);
        equipmentInventory.AddItem(item);
        UpdateStatsUI();
    }
}
