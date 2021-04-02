using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Archer/Precission")]
public class Precission : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 10);
        nextLevelValue = defaultEffectValue + (level + 1) * 10;
    }

    public override string StatsDescription()
    {
        string stats = "Passive skill";
        if (level < 1)
        {
            stats = "Increase critical damage by: <color=red>" + effectValue + "%</color>";
        }
        else
        {
            stats = "Increase critical damage by: <color=red>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float boost = hero.CriticalMultiply * effectValue / 100;
        hero.SetNewCriticalDamage(hero.CriticalMultiply + boost);
    }
}
