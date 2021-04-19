﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum PlayerSkill
{
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Inactive
}

public class PlayerSKills : MonoBehaviour
{
    [SerializeField] private ScriptableInt playersSkills;
    [SerializeField] private GameObject normalHitParticleEffects;
    [SerializeField] private GameObject critHitParticleEffects;
    [SerializeField] private Team team;
    private PlayerSkill selectedSkill;
    [SerializeField] private List<Button> skillsListButtons = new List<Button>();
    private List<PlayerScriptableSkill> activeSkills = new List<PlayerScriptableSkill>();
    private List<int> skillsCost = new List<int>();
    private int currentMana;
    private CharacterBattle mainCharacter;
    private List<bool> canInterac = new List<bool>();
    [SerializeField] private float attackRate;

    private void Start()
    {
        for(int i = 0; i < team.heroesList.Count; i++)
        {
            if (team.heroesList[i].isMainCharacter)
            {
                activeSkills = team.heroesList[i].activeSkills;
                break;
            }
        }
        SwapItemIcons();
        selectedSkill = PlayerSkill.Inactive;
    }

    public void SetUpMainCharacter(CharacterBattle mainCharacter)
    {
        this.mainCharacter = mainCharacter;
    }

    public void SetCurrentMana(int currentMana)
    {
        this.currentMana = currentMana;
        CheckIfSufficentMana(currentMana);
    }

    public void CheckIfSufficentMana(int currentMana)
    {
        this.currentMana = currentMana;
        for(int i = 0; i < skillsCost.Count; i++)
        {
            if(skillsCost[i] <= currentMana && canInterac[i] && !mainCharacter.IsStunned && !mainCharacter.Blinded)
            {
                skillsListButtons[i].interactable = true;
            } else skillsListButtons[i].interactable = false;
        }
    }

    public void BlockSkills(float duration)
    {
        StartCoroutine(BlockSKillUsage(duration));
    }

    IEnumerator BlockSKillUsage(float duration)
    {
        CheckIfSufficentMana(currentMana);
        yield return new WaitForSeconds(duration + .2f);
        CheckIfSufficentMana(currentMana);
    }

    private void SwapItemIcons()
    {
        for(int i = 0; i < activeSkills.Count; i++)
        {
            if (activeSkills[i] != null)
            {
                skillsListButtons[i].transform.parent.gameObject.SetActive(true);
                skillsListButtons[i].gameObject.GetComponent<Image>().sprite = activeSkills[i].icon;
                skillsListButtons[i].transform.parent.GetComponent<Image>().sprite = activeSkills[i].icon;
                skillsCost.Add(activeSkills[i].manaCost);
                canInterac.Add(true);
            }
            else skillsListButtons[i].gameObject.SetActive(false);
        }
    }

    public void SelectSkill1()
    {
        selectedSkill = PlayerSkill.Skill1;
    }
    public void SelectSkill2()
    {
        selectedSkill = PlayerSkill.Skill2;
    }
    public void SelectSkill3()
    {
        selectedSkill = PlayerSkill.Skill3;
    }
    public void SelectSkill4()
    {
        selectedSkill = PlayerSkill.Skill4;
    }

    public void UseSkill(CharacterBattle character)
    {
        if(selectedSkill != PlayerSkill.Inactive)
        {
            ++playersSkills.value;
        }
        switch (selectedSkill)
        {
            case PlayerSkill.Skill1:
                if (!activeSkills[0].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[0]);
                    activeSkills[0].Use(character);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(0));
                }
                break;
            case PlayerSkill.Skill2:
                if (!activeSkills[1].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[1]);
                    activeSkills[1].Use(character);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(1));
                }
                break;
            case PlayerSkill.Skill3:
                if (!activeSkills[2].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[2]);
                    activeSkills[2].Use(character);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(2));
                }
                break;
            case PlayerSkill.Skill4:
                if (!activeSkills[2].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[3]);
                    activeSkills[3].Use(character);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(3));
                }
                break;
            default:
                return;
        }
        selectedSkill = PlayerSkill.Inactive;
    }

    public void UseNegativeSkill(CharacterBattle enemy)
    {
        if (selectedSkill != PlayerSkill.Inactive)
        {
            ++playersSkills.value;
        }
        switch (selectedSkill)
        {
            case PlayerSkill.Skill1:
                if (activeSkills[0].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[0]);
                    activeSkills[0].Use(enemy);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(0));
                }
                break;
            case PlayerSkill.Skill2:
                if (activeSkills[1].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[1]);
                    activeSkills[1].Use(enemy);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(1));
                }
                break;
            case PlayerSkill.Skill3:
                if (activeSkills[2].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[2]);
                    activeSkills[2].Use(enemy);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(2));
                }
                break;
            case PlayerSkill.Skill4:
                if (activeSkills[2].isDebuf)
                {
                    mainCharacter.MainPlayerUseMana(skillsCost[3]);
                    activeSkills[3].Use(enemy);
                    selectedSkill = PlayerSkill.Inactive;
                    CheckIfSufficentMana(currentMana);
                    StartCoroutine(ButtonCooldown(3));
                }
                break;
            default:
                if(!mainCharacter.IsStunned && !mainCharacter.Blinded)
                {
                    mainCharacter.NormalAttack();
                }
                return;
        }
        selectedSkill = PlayerSkill.Inactive;
    }

    IEnumerator ButtonCooldown(int i)
    {
        canInterac[i] = false;
        skillsListButtons[i].interactable = false;
        skillsListButtons[i].gameObject.GetComponent<Image>().fillAmount = 0;
        float timer = activeSkills[i].coolDown / 100;
        for(int j = 0; j < 100; j++)
        {
            yield return new WaitForSeconds(timer);
            skillsListButtons[i].gameObject.GetComponent<Image>().fillAmount += 0.01f;
        }
        skillsListButtons[i].gameObject.GetComponent<Button>().interactable = true;
        canInterac[i] = true;
    }
    
}
