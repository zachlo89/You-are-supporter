using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Tank/Vitality")]
public class Vitality : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel));
        nextLevelValue = defaultEffectValue + (level + 1);
    }

    public override string StatsDescription()
    {
        string stats = "Passive skill\n";
        if (level < 1)
        {
            stats += "Increase max hp: <color=red>" + effectValue + "%</color>";
        }
        else
        {
            stats += "Increase max hp: <color=red>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float maxHealth = hero.GetMaxHP() * effectValue / 100;
        hero.SetHP(hero.GetMaxHP() + (int)maxHealth);
    }
}
