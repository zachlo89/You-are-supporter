using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterSkillTree")]
public class CharacterSkillTree : ScriptableObject
{
    public List<CharacterSkill> skillTree = new List<CharacterSkill>();
    public int counter;
    public int SetCounter()
    {
        counter = 0;
        foreach (CharacterSkill skill in skillTree)
        {
            try
            {
                counter += skill.level;
            }
            catch
            {
                Debug.Log("No skills avaliable");
            }


        }
        return counter;
    }
}
