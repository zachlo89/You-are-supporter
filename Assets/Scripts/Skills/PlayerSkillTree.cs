using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerSkillTree")]
public class PlayerSkillTree : ScriptableObject
{
    public List<PlayerScriptableSkill> skillTree = new List<PlayerScriptableSkill>();
    public int counter;
    public int SetCounter()
    {
        counter = 0;
        foreach(PlayerScriptableSkill skill in skillTree)
        {
            counter += skill.level;
        }
        return counter;
    }
}
