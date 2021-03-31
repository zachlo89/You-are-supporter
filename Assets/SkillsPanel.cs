﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsPanel : MonoBehaviour
{
    [SerializeField] private ListOfHeroes listOfHeroes;
    [SerializeField] private Transform characterSpawningPoint;
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
    private PlayerScriptableSkill currentSkill;

    private List<PlayerScriptableSkill> skillsAtHand = new List<PlayerScriptableSkill>();
    private List<ScriptableCharacter> listOfActiveHeroes = new List<ScriptableCharacter>();
    private Animator characterAnimator;
    private int charactersCounter;
    private bool isAvaliable;
    private int mainCharacterSkillsTree = 0;

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

    private void SpawnCharacter()
    {
        foreach (Transform child in characterSpawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject temp = Instantiate(listOfActiveHeroes[charactersCounter].prefab, characterSpawningPoint);
        temp.GetComponent<UpdateFaceAndBody>().SetUpFace(listOfActiveHeroes[charactersCounter]);
        temp.GetComponent<UpdateEquipment>().EquipAll(listOfActiveHeroes[charactersCounter].equipment);
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
            int counter = listOfActiveHeroes[charactersCounter].playerSkillTree[mainCharacterSkillsTree].SetCounter();
            if (listOfActiveHeroes[charactersCounter].playerSkillTree[mainCharacterSkillsTree] != null)
            {
                foreach (PlayerScriptableSkill skill in listOfActiveHeroes[charactersCounter].playerSkillTree[mainCharacterSkillsTree].skillTree)
                {
                    switch (skill.skillDifficultyLevel)
                    {
                        case SkillDifficultyLevel.Novice:
                            GameObject temp = Instantiate(skillPrefab, skillsSpawningPositions[0]);
                            temp.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, true);
                            break;
                        case SkillDifficultyLevel.Apprentice:
                            GameObject temp1 = Instantiate(skillPrefab, skillsSpawningPositions[1]);
                            if (counter > 4)
                            {
                                isAvaliable = true;
                            }
                            else isAvaliable = false;
                            temp1.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                            break;
                        case SkillDifficultyLevel.Adept:
                            GameObject temp2 = Instantiate(skillPrefab, skillsSpawningPositions[2]);
                            if (counter > 8)
                            {
                                isAvaliable = true;
                            }
                            else isAvaliable = false;
                            temp2.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                            break;
                        case SkillDifficultyLevel.Expert:
                            if (counter > 12)
                            {
                                isAvaliable = true;
                            }
                            else isAvaliable = false;
                            GameObject temp3 = Instantiate(skillPrefab, skillsSpawningPositions[3]);
                            temp3.GetComponent<PopulateSkillsPrefab>().Constructor(skill, this, isAvaliable);
                            break;
                        case SkillDifficultyLevel.Master:
                            if (counter > 16)
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
        } else
        {
            //To do team member
        }
        SpawnActiveSkills();
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
            if(skill.level < skill.maxLevel)
            {
                upgradeButtonText.text = "Lvl" + skill.level + 1 + " Upgrade";
            }
        }
        if(skill.isBought && !skill.isPassive && !listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(skill) && listOfActiveHeroes[charactersCounter].HasAvaliableSkillSlot())
        {
            buttonsList[1].gameObject.SetActive(true);
        } else if (listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(skill) && skill.isBought && !skill.isPassive){
            buttonsList[2].gameObject.SetActive(true);
        }

        SpawnActiveSkills();
    }

    public void Upgrade()
    {
        if(listOfActiveHeroes[charactersCounter].avaliableSkillPoints > 0 && currentSkill.level < currentSkill.maxLevel)
        {
            ++currentSkill.level;
            currentSkill.isBought = true;
        }
        UpdateLeftSide(currentSkill);
    }

    public void EquipSkill()
    {
        if(currentSkill.isBought && !currentSkill.isPassive && !listOfActiveHeroes[charactersCounter].CheckIfContainsSkill(currentSkill) && listOfActiveHeroes[charactersCounter].HasAvaliableSkillSlot())
        {
            listOfActiveHeroes[charactersCounter].AddSkill(currentSkill);
            UpdateLeftSide(currentSkill);
        }
    }

    public void UnequipSkill()
    {
        int index = listOfActiveHeroes[charactersCounter].CheckIndex(currentSkill);
        if (index != -1)
        {
            listOfActiveHeroes[charactersCounter].RemoveSkill(index);
        }
        else Debug.Log("Ups something went wrong Skill Panel Unequip Item");
        UpdateLeftSide(currentSkill);
    }

    public void SpawnActiveSkills()
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
    }
}
