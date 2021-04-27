using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Berserker/PiercingStab")]
public class PiercingStab : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 25);
        nextLevelValue = defaultEffectValue + (level + 1) * 25;
    }

    public override string StatsDescription()
    {
        string stats = "Two target\n";
        if (level < 1)
        {
            stats += "Attack first target for: <color=red>" + effectValue + "</color> of attack power\n";
            stats += "Second target for: <color=red>" + effectValue/ 2 + "</color> of attack power\n";
        }
        else
        {
            stats += "Attack first target for: <color=red>" + effectValue + "</color> of attack power\n";
            stats += "Second target for: <color=red>" + effectValue / 2 + "</color> of attack power\n";
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
        Instantiate(particleEffects, enemy.transform);
        if (CritAttack())
        {
            enemy.GetDamage((int)(damage * effectValue / 100 * hero.CriticalMultiply), true);
        } else enemy.GetDamage((int)(damage * effectValue / 100), false);
        CharacterBattle enemy1 = battleManager.GetMiddleCharacter(hero.tag);
        if(enemy1 != enemy)
        {
            if (CritAttack())
            {
                enemy1.GetDamage((int)(damage * effectValue / 100 * hero.CriticalMultiply), true);
            }
            else enemy1.GetDamage((int)(damage * effectValue / 100), false);
        }
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
