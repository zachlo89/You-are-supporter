using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Berserker/QuickSlash")]
public class QuickSlash : CharacterSkill
{
    public int slashesCount;
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
            stats += "Attack 3 times for: <color=red>" + effectValue + "%</color> of attack power";
        }
        else
        {
            stats += "Attack 3 times for: <color=red>" + effectValue + "%</color> of attack power\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=red>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        float value = hero.Damage * effectValue / 100;
        CharacterBattle enemy = battleManager.GetFrontCharacter(hero.tag);
        try
        {
            if (CritAttack())
            {
                enemy.GetDamage((int)(value * hero.CriticalMultiply), true);
            }
            else enemy.GetDamage((int)(value), true);
            Instantiate(particleEffects, enemy.transform);
        } catch
        {
            Debug.Log("Enemy is dead");
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
