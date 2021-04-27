using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Tank/Toughness")]
public class Toughness : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        Debug.Log("Initialized");
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 2);
        nextLevelValue = defaultEffectValue + (level + 1) * 2;
    }

    public override string StatsDescription()
    {
        string stats = "Passive skill\n";
        if (level < 1)
        {
            stats += "Increase armor by: <color=yellow>" + effectValue + "%</color>";
        }
        else
        {
            stats += "Increase armor by: <color=yellow>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=yellow>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float boost = hero.Armor * effectValue / 100;
        hero.SetArmor(hero.Armor + (int)boost);
    }
}
