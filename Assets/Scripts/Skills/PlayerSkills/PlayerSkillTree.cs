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
            try
            {
                counter += skill.level;
            } catch
            {
                Debug.Log("No skills avaliable");
            }
                

        }
        return counter;
    }
}
