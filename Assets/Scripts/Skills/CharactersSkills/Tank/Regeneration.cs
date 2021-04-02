using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Tank/Regeneration")]
public class Regeneration : CharacterSkill
{
    public float duration;
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 2);
        nextLevelValue = defaultEffectValue + (level + 1) * 2;
    }

    public override string StatsDescription()
    {
        string stats = "Target self";
        if (level < 1)
        {
            stats = "Increase hp regeneration: <color=green>" + effectValue + "%</color>";
        }
        else
        {
            stats = "Increase hp regeneration: <color=green>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=green>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float value = hero.GetMaxHP() * effectValue / 100;
        hero.HpRegenBuff((int)value, duration);
    }
}
