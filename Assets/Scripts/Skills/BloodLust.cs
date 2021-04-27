using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Berserker/BloodLust")]
public class BloodLust : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 0.1f);
        nextLevelValue = defaultEffectValue + (level + 1) * 0.1f;
    }

    public override string StatsDescription()
    {
        string stats = "Passive skill\n";
        if (level < 1)
        {
            stats += "Increase attack for: <color=red>" + effectValue + "%</color>\n for each 1% missing\n";
        }
        else
        {
            stats += "Increase attack for: <color=red>" + effectValue + "%</color>\n for each 1% missing\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        hero.BloodLust(effectValue);
    }
}
