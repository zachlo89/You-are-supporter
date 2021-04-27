using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Berserker/HeavyBlow")]

public class HeavyBlow : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 5);
        nextLevelValue = defaultEffectValue + (level + 1) * 5;
    }

    public override string StatsDescription()
    {
        string stats = "Single target\n";
        if (level < 1)
        {
            stats += "Attack for: <color=red> 200%</color>\n Bleed enemy for: <color=red>" + effectValue + "</color> seconds.";
        }
        else
        {
            stats += "Attack for: <color=red> 200%</color>\n Bleed enemy for: <color=red>" + effectValue + "</color> seconds.\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        
        int damage = hero.Damage;
        CharacterBattle enemy = battleManager.GetFrontCharacter(hero.tag);
        enemy.GetDamage(damage * 2, false);
        enemy.Bleed(effectValue);
        Instantiate(particleEffects, enemy.transform);
    }
}
