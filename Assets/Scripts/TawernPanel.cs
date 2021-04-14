using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TawernPanel : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private List<ItemScriptable> defaultItems = new List<ItemScriptable>();
    [SerializeField] private Image characterClassLogo;
    [SerializeField] private List<Sprite> characterClassesIcons = new List<Sprite>();
    [SerializeField] private Button buyButton;
    [SerializeField] private RandomGenerateBodies randomGenerator;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private GameObject heroPrefab;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI heroCostText;
    [SerializeField] private GameObject rightPanel;
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private TextMeshProUGUI damageText, armorText, attackRateText, healthText, critChanceText, critDamageText, blockChanceText, dodgeChanceText, characterClass, characterName, description;
    //Keep it as serilizeField during development
    [SerializeField] private ListOfHeroes allCharacter;
    private List<ScriptableCharacter> listOfCharacters = new List<ScriptableCharacter>();
    private List<ScriptableCharacter> listOfNotOwnCharacters = new List<ScriptableCharacter>();
    private ScriptableCharacter selectedHero;
    private int index = -1;

    private int damage, armor, attackRate, hp, blockChance, dodgeChance, heroCost;
    private float critChance, critDamage;
    private int newCharactersCount = 3;
    private TawernCharacterIcon tawernCharacterIcon;
    private void Start()
    {
        delegator.updateCount += UpdateGoldValue;
        listOfNotOwnCharacters.Clear();
        PopulateListOfNotOwnCharacter();
        PopulateTawern();
        rightPanel.SetActive(false);
        goldValue.text = inventory.Gold.value.ToString();
    }

    public void UpdateGoldValue()
    {
        goldValue.text = inventory.Gold.value.ToString();
    }
    public void PopulateTawern()
    {
        for(int i = 0; i < newCharactersCount; i++)
        {
            ScriptableCharacter temp = GetRandomCharacter();
            try
            {
                if (!listOfCharacters.Contains(temp) && temp != null)
                {
                    listOfCharacters.Add(temp);
                }
            }
            catch
            {
                Debug.Log("Missing character");
            }
        }

        foreach(ScriptableCharacter character in listOfCharacters)
        {
            GameObject temp = Instantiate(heroPrefab, spawningPoint);
            temp.GetComponent<TawernCharacterIcon>().SetUpCharacter(character, this);
        }
    }

    private void PopulateListOfNotOwnCharacter()
    {
        foreach (ScriptableCharacter character in allCharacter.heroesList)
        {
            if (character.isAvaliable == false && !listOfNotOwnCharacters.Contains(character))
            {
                character.headSprites = randomGenerator.GetRandomBodyPart("Head");
                character.beardSprites = randomGenerator.GetRandomBodyPart("Baerd");
                character.earsSprites = randomGenerator.GetRandomBodyPart("Ears");
                character.eyebrowsSprites = randomGenerator.GetRandomBodyPart("Eyebrows");
                character.eyesSprites = randomGenerator.GetRandomBodyPart("Eyes");
                character.mouthSprites = randomGenerator.GetRandomBodyPart("Mouth");
                character.hairsSprites = randomGenerator.GetRandomBodyPart("Hairs");
                character.characterName = randomGenerator.GetRandomName();
                if (character.level + 4 < allCharacter.heroesList[0].level)
                {
                    int randoLevel = Random.Range(allCharacter.heroesList[0].level, allCharacter.heroesList[0].level + 4);
                    for (int i = character.level; i < randoLevel; i++)
                    {
                        AdjustStatsToLevel(character);
                    }
                    character.level = randoLevel;
                }

                Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                character.bodyColor = randomColor;
                randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                character.hairColor = randomColor;
                listOfNotOwnCharacters.Add(character);

                switch (character.characterClass)
                {
                    case CharacterClass.Archer:
                        character.equipment.GetEquipment[3] = defaultItems[Random.Range(0, 3)];
                        break;
                    case CharacterClass.Berserker:
                        character.equipment.GetEquipment[2] = defaultItems[Random.Range(3, 6)];
                        break;
                    case CharacterClass.Tank:
                        character.equipment.GetEquipment[3] = defaultItems[Random.Range(6, 9)];
                        break;
                }
            }
        }
    }
    private ScriptableCharacter GetRandomCharacter()
    {
        if (listOfNotOwnCharacters.Count > 0)
        {
            int random = Random.Range(0, listOfNotOwnCharacters.Count);
            ScriptableCharacter temp = listOfNotOwnCharacters[random];
            listOfNotOwnCharacters.RemoveAt(random);
            return temp;
        }
        return null;
    }
    public void CharacterClick(ScriptableCharacter character, int index, TawernCharacterIcon tawernCharacterIcon)
    {
        if(this.tawernCharacterIcon != null)
        {
            this.tawernCharacterIcon.Deselection();
        }
        this.tawernCharacterIcon = tawernCharacterIcon;
        this.index = index;
        StartCoroutine(CharacterClickDelay(character));
        if(selectedHero.buyValue > inventory.Gold.value)
        {
            buyButton.interactable = false;
        } else
        {
            buyButton.interactable = true;
        }
    }

    IEnumerator CharacterClickDelay(ScriptableCharacter character)
    {
        selectedHero = character;
        damage = character.damage;
        armor = character.armor;
        attackRate = character.attackRate;
        hp = character.maxHealt;
        blockChance = character.blockChance;
        dodgeChance = character.dogdeChance;
        critChance = character.critChance;
        critDamage = character.critDamageMultiplay;
        heroCost = character.buyValue;


        yield return new WaitForSeconds(.11f);
        rightPanel.SetActive(true);

        switch (character.characterClass)
        {
            case CharacterClass.Tank:
                characterClassLogo.sprite = characterClassesIcons[0];
                break;
            case CharacterClass.Berserker:
                characterClassLogo.sprite = characterClassesIcons[1];
                break;
            case CharacterClass.Archer:
                characterClassLogo.sprite = characterClassesIcons[2];
                break;
            default:
                characterClassLogo.sprite = characterClassesIcons[0];
                break;
        }

        damageText.text = damage.ToString();
        armorText.text = armor.ToString();
        attackRateText.text = (100/ attackRate).ToString();
        healthText.text = hp.ToString();
        critChanceText.text = critChance.ToString();
        critDamageText.text = critDamage.ToString();
        blockChanceText.text = blockChance.ToString();
        dodgeChanceText.text = dodgeChance.ToString();
        characterClass.text = character.characterClass.ToString();
        characterName.text = character.characterName;
        heroCostText.text = heroCost.ToString();
    }

    public void HideDetailsPanel()
    {
        index = -1;
        selectedHero = null;
        rightPanel.SetActive(false);
    }


    private void AdjustStatsToLevel(ScriptableCharacter hero)
    {
        switch (hero.characterClass)
        {
            case CharacterClass.Tank:
                hero.maxHealt += (int)(hero.maxHealt * 8 / 100);
                hero.damage += (int)(hero.damage * 6 / 100);
                hero.toNextLevel = (int)(hero.toNextLevel * 1.5f);
                break;
            case CharacterClass.Archer:
                hero.maxHealt += (int)(hero.maxHealt * 6 / 100);
                hero.damage += (int)(hero.damage * 8 / 100);
                hero.toNextLevel = (int)(hero.toNextLevel * 1.5f);
                break;
            case CharacterClass.Berserker:
                hero.maxHealt += (int)(hero.maxHealt * 7 / 100);
                hero.damage += (int)(hero.damage * 7 / 100);
                hero.toNextLevel = (int)(hero.toNextLevel * 1.5f);
                break;
        }
    }

    public void BuyHero()
    {
        if(selectedHero.buyValue <= inventory.Gold.value && index != -1)
        {
            selectedHero.isAvaliable = true;
            inventory.Gold.value -= selectedHero.buyValue;
            goldValue.text = inventory.Gold.value.ToString();
            spawningPoint.GetChild(index).gameObject.SetActive(false);
            Destroy(spawningPoint.GetChild(index).gameObject);
        }
        rightPanel.SetActive(false);
        delegator.updateCount();
    }
}
