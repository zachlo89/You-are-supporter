using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/DefenseBuff")]
public class DefenseBuff : PlayerScriptableSkill
{
    public float buffDuration;
    public override string StatsDescription()
    {
        string stats = "Single target";
        if (level < 1)
        {
            stats = "Increase defense: <color=yellow>" + effectValue + "</color> for " + buffDuration + " seconds";
        }
        else
        {
            stats = "Increase defense: <color=yellow>" + effectValue + "</color> for " + buffDuration + " seconds\n";
            if(level < maxLevel)
            {
                stats += "Next level: <color=yellow>" + nextLevelValue + "</color>";
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
                hero.Defence((int)effectValue, buffDuration);
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        if (character != null && character.IsAlive)
        {
            character.Defence((int)effectValue, buffDuration);
        }
    }
}
