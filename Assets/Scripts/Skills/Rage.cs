using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Berserker/Rage")]
public class Rage : CharacterSkill
{
    public float duration;
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 10);
        nextLevelValue = defaultEffectValue + (level + 1) * 10;
    }

    public override string StatsDescription()
    {
        string stats = "Target self\n";
        if (level < 1)
        {
            stats += "Increase attack for: <color=red>" + effectValue + "%</color>\n for <color=red>" + duration + "</color> seconds.";
            stats += "Decrease defence for: <color=yellow>" + effectValue + "%</color>\n for <color=red>" + duration + "</color> seconds.\n";
        }
        else
        {
            stats += "Increase attack for: <color=red>" + effectValue + "%</color>\n";
            stats += "Decrease defence for: <color=yellow>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float attackIncrease = hero.Damage * effectValue / 100;
        float defenceDecrease = hero.Armor * effectValue / 100;
        hero.SetArmor(hero.Armor - (int)defenceDecrease);
        hero.SetDamage(hero.Damage + (int)attackIncrease);
        GameObject effects = Instantiate(particleEffects, hero.transform);
        Destroy(effects, duration);
    }
}
