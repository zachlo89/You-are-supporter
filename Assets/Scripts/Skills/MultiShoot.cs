using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Archer/MultiShoot")]
public class MultiShoot : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        InitializeSkillCost();
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 10);
        nextLevelValue = defaultEffectValue + (level + 1) * 10;
    }

    public override string StatsDescription()
    {
        string stats = "Multi target\n";
        if (level < 1)
        {
            stats += "Attack all enemies by: <color=red>" + effectValue + "%</color> of attack power";
        }
        else
        {
            stats += "Attack all enemies by: <color=red>" + effectValue + "%</color> of attack power\n";
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
        bool crit = false;
        if (CritAttack())
        {
            value = (int)(value * hero.CriticalMultiply);
            crit = true;
        }
        List<CharacterBattle> enemiesList = new List<CharacterBattle>();
        enemiesList = battleManager.GetAllEniemies(hero.tag);
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (enemiesList[i].IsAlive)
            {
                enemiesList[i].GetDamage((int)value, crit);
            }
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
