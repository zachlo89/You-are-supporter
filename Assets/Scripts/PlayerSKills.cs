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
    [SerializeField] private List<Button> skillsListButtons = new List<Button>();
    private List<PlayerScriptableSkill> activeSkills = new List<PlayerScriptableSkill>();
    private List<int> skillsCost = new List<int>();
    private int currentMana;
    private CharacterBattle mainCharacter;
    private List<bool> canInterac = new List<bool>();

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
            if(skillsCost[i] <= currentMana && canInterac[i])
            {
                skillsListButtons[i].interactable = true;
            } else skillsListButtons[i].interactable = false;
        }
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
        switch (selectedSkill)
        {
            case PlayerSkill.Skill1:
                mainCharacter.MainPlayerUseMana(skillsCost[0]);
                activeSkills[0].Use(character);
                selectedSkill = PlayerSkill.Inactive;
                CheckIfSufficentMana(currentMana);
                StartCoroutine(ButtonCooldown(0));
                break;
            case PlayerSkill.Skill2:
                mainCharacter.MainPlayerUseMana(skillsCost[1]);
                activeSkills[1].Use(character);
                selectedSkill = PlayerSkill.Inactive;
                CheckIfSufficentMana(currentMana);
                StartCoroutine(ButtonCooldown(1));
                break;
            case PlayerSkill.Skill3:
                mainCharacter.MainPlayerUseMana(skillsCost[2]);
                activeSkills[2].Use(character);
                selectedSkill = PlayerSkill.Inactive;
                CheckIfSufficentMana(currentMana);
                StartCoroutine(ButtonCooldown(2));
                break;
            case PlayerSkill.Skill4:
                mainCharacter.MainPlayerUseMana(skillsCost[3]);
                activeSkills[3].Use(character);
                selectedSkill = PlayerSkill.Inactive;
                CheckIfSufficentMana(currentMana);
                StartCoroutine(ButtonCooldown(3));
                break;
            default:
                return;
        }
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
