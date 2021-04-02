using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Archer/SnipeShoot")]
public class SnipeShoot : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 25);
        nextLevelValue = defaultEffectValue + (level + 1) * 25;
    }

    public override string StatsDescription()
    {
        string stats = "Single target";
        if (level < 1)
        {
            stats = "Attack enemy for: <color=red>" + effectValue + "%</color> of attack power\n Crit guarateed";
        }
        else
        {
            stats = "Attack enemy for: <color=red>" + effectValue + "%</color> of attack power\n Crit guarateed\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float value = (hero.Damage * effectValue / 100) * hero.CriticalMultiply;
        CharacterBattle enemy = battleManager.GetLastEnemy(hero.tag);
        enemy.GetDamage((int)value, true);
        
    }
}
