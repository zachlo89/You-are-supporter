using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Archer/Quickness")]
public class Quickness : CharacterSkill
{
    public float duration;
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 5);
        nextLevelValue = defaultEffectValue + (level + 1) * 5;
    }

    public override string StatsDescription()
    {
        string stats = "Target self\n";
        if (level < 1)
        {
            stats += "Increase attack speed by: <color=yellow>" + effectValue + "%</color>";
        }
        else
        {
            stats += "Increase attack speed by: <color=yellow>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=yellow>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float value = hero.AttackRate * effectValue;
        hero.Haste((int)value, duration);
    }
}
