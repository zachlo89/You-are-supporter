using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Tank/Slam")]
public class Slam : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = (1 + (character.level / 10f)) * defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 5);
        nextLevelValue = (1 + (character.level / 10f)) * defaultEffectValue + (level + 1) * 5;
    }

    public override string StatsDescription()
    {
        string stats = "Single target\n";
        if (level < 1)
        {
            stats += "Attack enemy: <color=red>" + effectValue + "%</color>";
        }
        else
        {
            stats += "Attack enemy: <color=red>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        CharacterBattle enemy = battleManager.GetFrontCharacter(hero.tag);
        float damage = hero.Damage * effectValue / 100;
        if (CritAttack())
        {
            damage *= hero.CriticalMultiply;
            enemy.GetDamage((int)damage, true);
        } else enemy.GetDamage((int)damage, false);
        Instantiate(particleEffects, enemy.transform);
    }

    private bool CritAttack()
    {
        int random = Random.Range(0, 100);
        if (random <= hero.CriticalChance)
        {
            return true;
        }
        else return false;
    }
}
