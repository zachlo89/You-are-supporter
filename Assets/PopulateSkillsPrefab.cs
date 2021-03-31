using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSkillsPrefab : MonoBehaviour
{
    private SkillsPanel skillsPanel;
    private PlayerScriptableSkill skill;
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

    public void UpdateDetails()
    {
        skillsPanel.UpdateLeftSide(skill);
    }
}
