using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSkillsPrefab : MonoBehaviour
{
    private SkillsPanel skillsPanel;
    private PlayerScriptableSkill skill;
    private CharacterSkill skill1;
    [SerializeField] private Image image;


    public void Constructor(PlayerScriptableSkill skill, SkillsPanel skillsPanel, bool isAvaliable)
    {
        this.skill = skill;
        this.skill.isAvaliable = isAvaliable;
        if (skill.isBought)
        {
            image.sprite = skill.icon;
        }
        else image.sprite = skill.notOwnSprite;
        this.skillsPanel = skillsPanel;
    }

    public void Constructor(CharacterSkill skill1, SkillsPanel skillsPanel, bool isAvaliable)
    {
        this.skill1 = skill1;
        this.skill1.isAvaliable = isAvaliable;
        if (skill1.isBought)
        {
            image.sprite = skill1.icon;
        }
        else image.sprite = skill1.notOwnSprite;
        this.skillsPanel = skillsPanel;
    }

    public void UpdateDetails()
    {
        if(skill != null)
        {
            skillsPanel.UpdateLeftSide(skill);
        } else {
            skillsPanel.UpdateLeftSide(skill1);
        }
        
    }
}
