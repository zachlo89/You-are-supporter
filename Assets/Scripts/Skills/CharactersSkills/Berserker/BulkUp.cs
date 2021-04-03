using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Berserker/BulkUp")]
public class BulkUp : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 2);
        nextLevelValue = defaultEffectValue + (level + 1) * 2;
    }

    public override string StatsDescription()
    {
        string stats = "Passive skill\n";
        if (level < 1)
        {
            stats += "Increase attack: <color=red>" + effectValue + "%</color>\n";
            stats += "Increase health: <color=green>" + effectValue + "%</color>\n";
        }
        else
        {
            stats += "Increase attack: <color=red>" + effectValue + "%</color>\n";
            stats += "Increase health: <color=green>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
                stats += "Next level: <color=green>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        hero.SetHP(hero.GetMaxHP() + (int)(hero.GetMaxHP() * effectValue / 100));
        hero.SetDamage(hero.Damage + (int)(hero.Damage * effectValue / 100));
    }
}
