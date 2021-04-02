using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Tank/HeavenlyStrike")]
public class HeavenlyStrike : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 15);
        nextLevelValue = defaultEffectValue + (level + 1) * 15;
    }

    public override string StatsDescription()
    {
        string stats = "Multi target";
        if (level < 1)
        {
            stats = "Attack all enemy for: <color=red>200%\n " + effectValue + "</color>";
        }
        else
        {
            stats = "Attack all enemy for: <color=red>200%\n " + effectValue + "</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=yellow>" + nextLevelValue + "%</color>";
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
        for(int i = 0; i < enemiesList.Count; i++)
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
