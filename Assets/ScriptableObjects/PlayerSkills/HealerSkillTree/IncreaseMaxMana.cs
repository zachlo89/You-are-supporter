using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/IncreaseMaxMana")]
public class IncreaseMaxMana : PlayerScriptableSkill
{
    public override string StatsDescription()
    {
        string stats = "<color=white>Passive skill:\n</color>";
        if (level < 1)
        {
            stats += "Increase max mana: <color=blue>" + effectValue + "</color>";
        }
        else
        {
            stats += "Increase max mana: <color=blue>" + effectValue + "</color>\n";
            if(level < maxLevel)
            {
                stats += "Next level: <color=blue>" + nextLevelValue + "</color>";
            }
        }

        return stats;
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach (CharacterBattle hero in characters)
        {
            if (hero != null && hero.IsAlive)
            {
                hero.InreaseMaxMana((int)effectValue);
            }
        }
        Debug.Log("Skill " + skillName + " used");
    }

    public override void Use(CharacterBattle character)
    {
        if (character != null && character.IsAlive)
        {
            character.InreaseMaxMana((int)effectValue);
            Debug.Log("Skill " + skillName + " used");
        }
    }
}
