using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Archer/ShadowStep")]
public class ShadowStep : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel));
        nextLevelValue = defaultEffectValue + (level + 1);
    }

    public override string StatsDescription()
    {
        string stats = "Passive skill\n";
        if (level < 1)
        {
            stats += "Increase dodge chance: <color=cyan>" + effectValue + "</color>";
        }
        else
        {
            stats += "Increase dodge chance: <color=cyan>" + effectValue + "</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=cyan>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        hero.SetNewDodgeChance(hero.DodgeChance + (int)effectValue);
    }
}
