using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsPanel : MonoBehaviour
{
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private GameObject newSkillAlarm;
    [SerializeField] private ListOfHeroes listOfHeroes;
    [SerializeField] private Transform characterSpawningPoint;
    [SerializeField] private TextMeshProUGUI goldValue;
    public Transform CharacterSpawningPoint
    {
        get { return characterSpawningPoint; }
    }
    [SerializeField] private GameObject groupLeft;
    [SerializeField] private Image skillRarityImage;
    [SerializeField] private TextMeshProUGUI skillRarityText;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI effectDescription;
    [SerializeField] private TextMeshProUGUI skillLevel;
    [SerializeField] private Slider skillLevelSlider;
    [SerializeField] private TextMeshProUGUI skillLevelSliderText;
    [SerializeField] private TextMeshProUGUI skillStats;
    [SerializeField] private List<Transform> skillsSpawningPositions = new List<Transform>();
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private List<Button> buttonsList;
    [SerializeField] private TextMeshProUGUI upgradeButtonText;
    [SerializeField] private List<Transform> activeSkillsSpawningPoint = new List<Transform>();
    [SerializeField] private TextMeshProUGUI avaliablePointText;
    [SerializeField] private TextMeshProUGUI requrimentsText;

    [SerializeField] private List<int> tresholds = new List<int>();
    private PlayerScriptableSkill currentSkill;
    private CharacterSkill currentSkill1;

    private List<PlayerScriptableSkill> skillsAtHand = new List<PlayerScriptableSkill>();
    private List<ScriptableCharacter> listOfActiveHeroes = new List<ScriptableCharacter>();
    private Animator characterAnimator;
    private int charactersCounter;
    private bool isAvaliable;
    private int mainCharacterSkillsTree = 0;

    private void Start()
    {
        gameObject.SetActive(false);
        goldValue.text = inventory.Gold.value.ToString();
    }

    private void OnEnable()
    {
        listOfActiveHeroes.Clear();
        for(int i = 0; i < listOfHeroes.heroesList.Count; i++)
        {
            if (listOfHeroes.heroesList[i].isAvaliable)
            {
                listOfActiveHeroes.Add(listOfHeroes.heroesList[i]);
            }
        }
        SpawnCharacter();
    }

    private void OnDisable()
    {
        int counter = 0;
        foreach (ScriptableCharacter hero in listOfHeroes.heroesList)
        {
            if (hero.avaliableSkillPoints > 0)
            {
                newSkillAlarm.SetActive(true);
                counter += hero.avaliableSkillPoints;
            }
        }
        if (counter > 0)
        {
            newSkillAlarm.GetComponentInChildren<TextMeshProUGUI>().text = counter.ToString();
        }
        else newSkillAlarm.SetActive(false);
    }
    private void SpawnCharacter()
    {
        foreach (Transform child in characterSpawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject temp = Instantiate(listOfActiveHeroes[charactersCounter].prefab, characterSpawningPoint);
        temp.GetComponent<UpdateFaceAndBody>().SetUpFace(listOfActiveHeroes[charactersCounter]);
        temp.GetComponent<UpdateEquipment>().EquipAll(listOfActiveHeroes[charactersCounter].equipment);
        temp.GetComponentInChildren<Animator>().Play("IdleMelee");
        temp.transform.localScale *= 3;
        characterAnimator = temp.GetComponentInChildren<Animator>();
        PopulateSkillTree();
    }

    private void PopulateSkillTree()
    {
        for(int i = 0; i < skillsSpawningPositions.Count; i++)
        {
            foreach (Transform child in skillsSpawningPositions[i])
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        if (listOfActiveHeroes[charactersCounter].isMainCharacter)
        {
           
            if (mainCharacterSkillsTree <= listOfActiveHeroes[charactersCounter].playerSkillTree.Count - 1)
            {
                int counter = listOfActiveHeroes[charactersCounter].playerSkillTree[mainCharacterSkillsTree].SetCounter();
                foreach (PlayerScriptableSkill skill in listOfActiveHeroes[charactersCounter].playerSkillTree[mainCharacterSkillsTree].skillTree)
                {
                    if(skill != null)
                    {
                        switch (skill.skillDifficultyLevel)
                        {
                            case SkillDifficultyLevel.Novice:
                                GameObject temp = Instantiate(skillPrefab, skillsSpawningPositions[0]);
                                temp.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, true);
                                break;
                            case SkillDifficultyLevel.Apprentice:
                                GameObject temp1 = Instantiate(skillPrefab, skillsSpawningPositions[1]);
                                if (counter > tresholds[0])
                                {
                                    isAvaliable = true;
                                }
                                else isAvaliable = false;
                                temp1.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                                break;
                            case SkillDifficultyLevel.Adept:
                                GameObject temp2 = Instantiate(skillPrefab, skillsSpawningPositions[2]);
                                if (counter > tresholds[1])
                                {
                                    isAvaliable = true;
                                }
                                else isAvaliable = false;
                                temp2.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                                break;
                            case SkillDifficultyLevel.Expert:
                                if (counter > tresholds[2])
                                {
                                    isAvaliable = true;
                                }
                                else isAvaliable = false;
                                GameObject temp3 = Instantiate(skillPrefab, skillsSpawningPositions[3]);
                                temp3.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                                break;
                            case SkillDifficultyLevel.Master:
                                if (counter > tresholds[3])
                                {
                                    isAvaliable = true;
                                }
                                else isAvaliable = false;
                                GameObject temp4 = Instantiate(skillPrefab, skillsSpawningPositions[4]);
                                temp4.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        } else
        {
            int counter = listOfActiveHeroes[charactersCounter].characterSkillTree[mainCharacterSkillsTree].SetCounter();
            foreach (CharacterSkill skill in listOfActiveHeroes[charactersCounter].characterSkillTree[mainCharacterSkillsTree].skillTree)
            {
                if (skill != null)
                {
                    switch (skill.skillDifficultyLevel)
                    {
                        case SkillDifficultyLevel.Novice:
                            GameObject temp = Instantiate(skillPrefab, skillsSpawningPositions[0]);
                            temp.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, true);
                            break;
                        case SkillDifficultyLevel.Apprentice:
                            GameObject temp1 = Instantiate(skillPrefab, skillsSpawningPositions[1]);
                            if (counter > tresholds[0])
                            {
                                isAvaliable = true;
                            }
                            else isAvaliable = false;
                            temp1.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                            break;
                        case SkillDifficultyLevel.Adept:
                            GameObject temp2 = Instantiate(skillPrefab, skillsSpawningPositions[2]);
                            if (counter > tresholds[1])
                            {
                                isAvaliable = true;
                            }
                            else isAvaliable = false;
                            temp2.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                            break;
                        case SkillDifficultyLevel.Expert:
                            if (counter > tresholds[2])
                            {
                                isAvaliable = true;
                            }
                            else isAvaliable = false;
                            GameObject temp3 = Instantiate(skillPrefab, skillsSpawningPositions[3]);
                            temp3.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                            break;
                        case SkillDifficultyLevel.Master:
                            if (counter > tresholds[3])
                            {
                                isAvaliable = true;
                            }
                            else isAvaliable = false;
                            GameObject temp4 = Instantiate(skillPrefab, skillsSpawningPositions[4]);
                            temp4.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        SpawnActiveSkills();
        avaliablePointText.text = "Avaliable skill points: " + listOfActiveHeroes[charactersCounter].avaliableSkillPoints;
    }

    public void GetAnotherCharacter(int i)
    {
        charactersCounter += i;
        if(charactersCounter < 0)
        {
            charactersCounter = listOfActiveHeroes.Count - 1;
        }
        if (listOfActiveHeroes.Count > charactersCounter)
        {
            SpawnCharacter();
        }
        else charactersCounter = 0;
        SpawnCharacter();
    }

    public void UpdateLeftSide(PlayerScriptableSkill skill)
    {
        for(int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].gameObject.SetActive(false);
        }
        skill.Initialize(listOfActiveHeroes[charactersCounter]);
        currentSkill = skill;
        groupLeft.SetActive(true);
        skillRarityImage.sprite = currentSkill.skillBorder;
        skillLevel.text = currentSkill.level.ToString();
        skillLevelSlider.value = (float)currentSkill.level / (float)currentSkill.maxLevel;
        skillLevelSliderText.text = currentSkill.level + "/" + currentSkill.maxLevel;
        skillName.text = currentSkill.skillName;
        skillRarityText.text = currentSkill.skillDifficultyLevel.ToString();
        effectDescription.text = currentSkill.description;
        skillStats.text = currentSkill.StatsDescription();
        if(skill.isAvaliable && skill.level < skill.maxLevel)
        {
            buttonsList[0].gameObject.SetActive(true);
            if (skill.level < skill.maxLevel)
            {
                upgradeButtonText.text = "Lvl " + (skill.level + 1) + " Upgrade";
                if (listOfActiveHeroes[charactersCounter].avaliableSkillPoints <= 0)
                {
                    buttonsList[0].interactable = false;
                }
                else buttonsList[0].interactable = true;
            }
        }
        if(skill.isBought && !skill.isPassive && !listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(skill) && listOfActiveHeroes[charactersCounter].HasAvaliableSkillSlot(true))
        {
            buttonsList[1].gameObject.SetActive(true);
        } else if (listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(skill) && skill.isBought && !skill.isPassive){
            buttonsList[2].gameObject.SetActive(true);
        }
        if (skill.level == 0 && !skill.isAvaliable)
        {
            requrimentsText.gameObject.SetActive(true);
            int temp = 0;
            switch (skill.skillDifficultyLevel)
            {
                case SkillDifficultyLevel.Apprentice:
                    temp = tresholds[0];
                    break;
                case SkillDifficultyLevel.Adept:
                    temp = tresholds[1];
                    break;
                case SkillDifficultyLevel.Expert:
                    temp = tresholds[2];
                    break;
                case SkillDifficultyLevel.Master:
                    temp = tresholds[3];
                    break;
                default:
                    break;

            }
            requrimentsText.text = "Require " + temp + " skill points invested in this skill tree";
        }
        else requrimentsText.gameObject.SetActive(false);

        buttonsList[0].GetComponentInChildren<TextMeshProUGUI>().text = currentSkill.cost.ToString();
        if (currentSkill.cost > inventory.Gold.value)
        {
            buttonsList[0].interactable = false;
        }

        SpawnActiveSkills();
        avaliablePointText.text = "Avaliable skill points: " + listOfActiveHeroes[charactersCounter].avaliableSkillPoints;
    } 

    public void UpdateLeftSide(CharacterSkill skill)
    {
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].gameObject.SetActive(false);
        }
        skill.Initialize(listOfActiveHeroes[charactersCounter]);
        currentSkill1 = skill;
        groupLeft.SetActive(true);
        skillRarityImage.sprite = currentSkill1.skillBorder;
        skillLevel.text = currentSkill1.level.ToString();
        skillLevelSlider.value = (float)currentSkill1.level / (float)currentSkill1.maxLevel;
        skillLevelSliderText.text = currentSkill1.level + "/" + currentSkill1.maxLevel;
        skillName.text = currentSkill1.skillName;
        skillRarityText.text = currentSkill1.skillDifficultyLevel.ToString();
        effectDescription.text = currentSkill1.description;
        skillStats.text = currentSkill1.StatsDescription();
        if (skill.isAvaliable && skill.level < skill.maxLevel)
        {
            buttonsList[0].gameObject.SetActive(true);
            if (skill.level < skill.maxLevel)
            {
                upgradeButtonText.text = "Lvl " + (skill.level + 1) + " Upgrade";
                if (listOfActiveHeroes[charactersCounter].avaliableSkillPoints <= 0)
                {
                    buttonsList[0].interactable = false;
                }
                else buttonsList[0].interactable = true;
            }
        }
        if (skill.isBought && !skill.isPassive && !listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(skill) && listOfActiveHeroes[charactersCounter].HasAvaliableSkillSlot(true))
        {
            buttonsList[1].gameObject.SetActive(true);
        }
        else if (listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(skill) && skill.isBought && !skill.isPassive)
        {
            buttonsList[2].gameObject.SetActive(true);
        }

        if (skill.level == 0 && !skill.isAvaliable)
        {
            requrimentsText.gameObject.SetActive(true);
            int temp = 0;
            switch (skill.skillDifficultyLevel)
            {
                case SkillDifficultyLevel.Apprentice:
                    temp = tresholds[0];
                    break;
                case SkillDifficultyLevel.Adept:
                    temp = tresholds[1];
                    break;
                case SkillDifficultyLevel.Expert:
                    temp = tresholds[2];
                    break;
                case SkillDifficultyLevel.Master:
                    temp = tresholds[3];
                    break;
                default:
                    break;

            }
            requrimentsText.text = "Require " + temp + " skill points invested in this skill tree";
        }
        else requrimentsText.gameObject.SetActive(false);
        
        buttonsList[0].GetComponentInChildren<TextMeshProUGUI>().text = currentSkill1.cost.ToString();
        if(currentSkill1.cost > inventory.Gold.value)
        {
            buttonsList[0].interactable = false;
        }
        SpawnActiveSkills();
        avaliablePointText.text = "Avaliable skill points: " + listOfActiveHeroes[charactersCounter].avaliableSkillPoints;
    }

    public void Upgrade()
    {
        if (listOfActiveHeroes[charactersCounter].isMainCharacter)
        {
            if (listOfActiveHeroes[charactersCounter].avaliableSkillPoints > 0 && currentSkill.level < currentSkill.maxLevel)
            {
                --listOfActiveHeroes[charactersCounter].avaliableSkillPoints;
                ++currentSkill.level;
                currentSkill.isBought = true;
                PopulateSkillTree();
            }
            inventory.AddGold(-currentSkill.cost);
            UpdateLeftSide(currentSkill);
        } else
        {
            if (listOfActiveHeroes[charactersCounter].avaliableSkillPoints > 0 && currentSkill1.level < currentSkill1.maxLevel)
            {
                --listOfActiveHeroes[charactersCounter].avaliableSkillPoints;
                ++currentSkill1.level;
                currentSkill1.isBought = true;
                PopulateSkillTree();
            }
            inventory.AddGold(-currentSkill1.cost);
            UpdateLeftSide(currentSkill1);
        }
        goldValue.text = inventory.Gold.value.ToString();
    }

    public void EquipSkill()
    {
        if (listOfActiveHeroes[charactersCounter].isMainCharacter)
        {
            if (currentSkill != null)
            {
                if (currentSkill.isBought && !currentSkill.isPassive && !listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(currentSkill) && listOfActiveHeroes[charactersCounter].HasAvaliableSkillSlot(true))
                {
                    listOfActiveHeroes[charactersCounter].AddSkill(currentSkill);
                    UpdateLeftSide(currentSkill);
                }
            }
        }
        else if(!listOfActiveHeroes[charactersCounter].isMainCharacter)
        {
            if (currentSkill1.isBought && !currentSkill1.isPassive && !listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(currentSkill1) && listOfActiveHeroes[charactersCounter].HasAvaliableSkillSlot(false))
            {
                listOfActiveHeroes[charactersCounter].AddSkill(currentSkill1);
                UpdateLeftSide(currentSkill1);
            }
        }
        
    }

    public void UnequipSkill()
    {
        if (listOfActiveHeroes[charactersCounter].isMainCharacter)
        {
            int index = listOfActiveHeroes[charactersCounter].CheckIndex(currentSkill);
            if (index != -1)
            {
                listOfActiveHeroes[charactersCounter].RemoveSkill(index);
            }
            else Debug.Log("Ups something went wrong Skill Panel Unequip Item");
            UpdateLeftSide(currentSkill);
        } else
        {
            int index = listOfActiveHeroes[charactersCounter].CheckIndex(currentSkill1);
            if (index != -1)
            {
                listOfActiveHeroes[charactersCounter].RemoveSkill(true,index);
            }
            else Debug.Log("Ups something went wrong Skill Panel Unequip Item");
            UpdateLeftSide(currentSkill1);
        }
            
    }

    public void SpawnActiveSkills()
    {
        if (listOfActiveHeroes[charactersCounter].isMainCharacter)
        {
            for (int i = 0; i < activeSkillsSpawningPoint.Count; i++)
            {
                foreach (Transform child in activeSkillsSpawningPoint[i])
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            for (int i = 0; i < listOfActiveHeroes[charactersCounter].activeSkills.Count; i++)
            {
                if (listOfActiveHeroes[charactersCounter].activeSkills[i] != null)
                {
                    GameObject temp = Instantiate(skillPrefab, activeSkillsSpawningPoint[i]);
                    temp.GetComponent<PopulateSkillsPrefab>().Constructor(listOfActiveHeroes[charactersCounter].activeSkills[i], this, true);
                }
                else Debug.Log("Ups something went wrond SpawnActive skills");
            }
        } else
        {
            for (int i = 0; i < activeSkillsSpawningPoint.Count; i++)
            {
                foreach (Transform child in activeSkillsSpawningPoint[i])
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            for (int i = 0; i < listOfActiveHeroes[charactersCounter].characterActiveSkills.Count; i++)
            {
                if (listOfActiveHeroes[charactersCounter].characterActiveSkills[i] != null)
                {
                    GameObject temp = Instantiate(skillPrefab, activeSkillsSpawningPoint[i]);
                    temp.GetComponent<PopulateSkillsPrefab>().Constructor(listOfActiveHeroes[charactersCounter].characterActiveSkills[i], this, true);
                }
                else Debug.Log("Ups something went wrond SpawnActive skills");
            }
        }
        
    }

    public void ChangeTab(int i)
    {
        if(i > -1 && i < 4)
        {
            mainCharacterSkillsTree = i;
            PopulateSkillTree();
        }
    }

    public void PlaySkillAnimation(string skillName)
    {
        try
        {
            characterAnimator.SetTrigger(skillName);
        } catch
        {
            Debug.Log("Animation missing");
        }
    }
}
