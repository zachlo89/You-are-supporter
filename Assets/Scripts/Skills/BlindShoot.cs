using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Archer/BlindShoot")]
public class BlindShoot : CharacterSkill
{
    public float duration;
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 25);
        nextLevelValue = defaultEffectValue + (level + 1) * 25;
    }

    public override string StatsDescription()
    {
        string stats = "Single target\n";
        if (level < 1)
        {
            stats += "Attack random enemy for: <color=red>" + effectValue + "%</color> of attack power\n Blind it for:  <color=red>" + duration + " </color>";
        }
        else
        {
            stats += "Attack random enemy for: <color=red>" + effectValue + "%</color> of attack power\n Blind it for:  <color=red>" + duration + " </color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float value = (hero.Damage * effectValue / 100);
        CharacterBattle enemy = battleManager.GetMiddleCharacter(hero.tag);
        enemy.GetDamage((int)value, false);
        enemy.Blind(duration);
    }
}
