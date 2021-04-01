using System.Collections;
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
    [SerializeField] private Team team;
    private PlayerSkill selectedSkill;
    [SerializeField] private List<GameObject> skillsListButtons = new List<GameObject>();
    private List<PlayerScriptableSkill> activeSkills = new List<PlayerScriptableSkill>();
    private List<int> skillsCost = new List<int>();
    private int currentMana;
    private CharacterBattle mainCharacter;

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
            if(skillsCost[i] <= currentMana)
            {
                skillsListButtons[i].SetActive(true);
            } else skillsListButtons[i].SetActive(false);
        }
    }

    private void SwapItemIcons()
    {
        for(int i = 0; i < activeSkills.Count; i++)
        {
            if (activeSkills[i] != null)
            {
                skillsListButtons[i].SetActive(true);
                skillsListButtons[i].GetComponent<Image>().sprite = activeSkills[i].icon;
                skillsCost.Add(activeSkills[i].manaCost);
            }
            else skillsListButtons[i].SetActive(false);
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
        switch (selectedSkill)
        {
            case PlayerSkill.Skill1:
                mainCharacter.MainPlayerUseMana(skillsCost[0]);
                activeSkills[0].Use(character);
                selectedSkill = PlayerSkill.Inactive;
                CheckIfSufficentMana(currentMana);
                break;
            case PlayerSkill.Skill2:
                mainCharacter.MainPlayerUseMana(skillsCost[1]);
                activeSkills[1].Use(character);
                selectedSkill = PlayerSkill.Inactive;
                CheckIfSufficentMana(currentMana);
                break;
            case PlayerSkill.Skill3:
                mainCharacter.MainPlayerUseMana(skillsCost[2]);
                activeSkills[2].Use(character);
                selectedSkill = PlayerSkill.Inactive;
                CheckIfSufficentMana(currentMana);
                break;
            default:
                return;
        }
    }


    
}
