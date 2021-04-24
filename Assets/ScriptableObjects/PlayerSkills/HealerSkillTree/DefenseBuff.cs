using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/DefenseBuff")]
public class DefenseBuff : PlayerScriptableSkill
{
    public float buffDuration;
    public override string StatsDescription()
    {
        string stats = "Single target\n";
        if (level < 1)
        {
            stats += "Increase defense: <color=yellow>" + effectValue + "</color> for " + buffDuration + " seconds\n";
            stats += "Mana cost: <color=blue>" + manaCost + "</color>\n";
        }
        else
        {
            stats += "Increase defense: <color=yellow>" + effectValue + "</color> for " + buffDuration + " seconds\n";
            stats += "Mana cost: <color=blue>" + manaCost + "</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=yellow>" + nextLevelValue + "</color>\n";
                stats += "Mana cost: <color=blue>" + nextLevelManaCost + "</color>\n";
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
                GameObject temp = Instantiate(particleEffect, hero.transform);
                Destroy(temp, buffDuration);
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        if (character != null && character.IsAlive)
        {
            character.Defence((int)effectValue, buffDuration);
            GameObject temp = Instantiate(particleEffect, character.transform);
            Destroy(temp, buffDuration);
        }
    }
}
