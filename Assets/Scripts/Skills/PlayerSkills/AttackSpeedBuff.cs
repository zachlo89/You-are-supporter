using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/AttackSpeedBuff")]
public class AttackSpeedBuff : PlayerScriptableSkill
{
    public float buffDuration;
    public override string StatsDescription()
    {
        string stats = "Single target";
        if (level < 1)
        {
            stats = "Increase attack speed: <color=yellow>" + effectValue + "</color> single target for " + buffDuration + " seconds";
        }
        else
        {
            stats = "Increase attack speed:: <color=yellow>" + effectValue + "</color> single target for " + buffDuration + " seconds\n";
            if (level < maxLevel)
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
                hero.Haste((int)effectValue, buffDuration);
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        if (character != null && character.IsAlive)
        {
            character.Haste((int)effectValue, buffDuration);
        }
    }
}
