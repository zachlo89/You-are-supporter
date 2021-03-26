using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TawernPanel : MonoBehaviour
{
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
    private void Start()
    {
        listOfNotOwnCharacters.Clear();
        PopulateTawern();
        rightPanel.SetActive(false);
        goldValue.text = inventory.Gold.value.ToString();
    }
    public void PopulateTawern()
    {
        for(int i = 0; i < newCharactersCount; i++)
        {
            listOfCharacters.Add(GetRandomCharacter());
        }

        // TO DO create class to generate random heroes accordingly to level
        foreach(ScriptableCharacter character in listOfCharacters)
        {
            GameObject temp = Instantiate(heroPrefab, spawningPoint);
            temp.GetComponent<TawernCharacterIcon>().SetUpCharacter(character, this);
        }
    }

    private ScriptableCharacter GetRandomCharacter()
    {
        if(listOfNotOwnCharacters.Count <= 0)
        {
            foreach (ScriptableCharacter character in allCharacter.heroesList)
            {
                if (character.isAvaliable == false)
                {
                    character.headSprites = randomGenerator.GetRandomBodyPart("Head");
                    character.beardSprites = randomGenerator.GetRandomBodyPart("Baerd");
                    character.earsSprites = randomGenerator.GetRandomBodyPart("Ears");
                    character.eyebrowsSprites = randomGenerator.GetRandomBodyPart("Eyebrows");
                    character.eyesSprites = randomGenerator.GetRandomBodyPart("Eyes");
                    character.mouthSprites = randomGenerator.GetRandomBodyPart("Mouth");
                    character.hairsSprites = randomGenerator.GetRandomBodyPart("Hairs");
                    character.characterName = randomGenerator.GetRandomName();
                    int randoLevel = Random.Range(allCharacter.heroesList[0].level, allCharacter.heroesList[0].level + 4);
                    character.level = randoLevel;


                    Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                    character.bodyColor = randomColor;
                    randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                    character.hairColor = randomColor;
                    listOfNotOwnCharacters.Add(character);
                }
            }
        }
        
        if (listOfNotOwnCharacters.Count > 0)
        {
            int random = Random.Range(0, listOfNotOwnCharacters.Count);
            return listOfNotOwnCharacters[random];
        }
        else return null;
    }
    public void CharacterClick(ScriptableCharacter character, int index)
    {
        this.index = index;
        StartCoroutine(CharacterClickDelay(character));
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
        AdjustStatsToLevel(character);

        yield return new WaitForSeconds(.11f);
        rightPanel.SetActive(true);
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
        heroCost *= (int)(hero.level * 1.5f);
        switch (hero.characterClass)
        {
            case CharacterClass.Tank:
                for (int i = 0; i < hero.level; i++)
                {
                    hp += (int)(hp * 8 / 100);
                    damage += (int)(damage * 6 / 100);
                }
                break;
            case CharacterClass.Archer:
                hp += (int)(hp * 6 / 100);
                damage += (int)(damage * 8 / 100);
                break;
            case CharacterClass.Berserker:
                hp += (int)(hp * 7 / 100);
                damage += (int)(damage * 7 / 100);
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
    }
}
