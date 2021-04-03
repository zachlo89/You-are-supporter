using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Archer/QuickDraw")]
public class QuickDraw : CharacterSkill
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
            stats += "Increase critical chance: <color=red>" + effectValue + "</color>";
        }
        else
        {
            stats += "Increase critical chance: <color=red>" + effectValue + "</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        hero.SetNewCriticalChance(hero.CriticalChance + effectValue);
    }
}
