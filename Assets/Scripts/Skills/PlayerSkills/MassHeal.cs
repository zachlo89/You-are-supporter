using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/MassHeal")]
public class MassHeal : PlayerScriptableSkill
{
    public override string StatsDescription()
    {
        string stats = "Multitarget";
        if (level < 1)
        {
            stats = "Heal: <color=green>" + effectValue + "</color>";
        }
        else
        {
            stats = "Heal: <color=green>" + effectValue + "</color> single target\n";
            stats += "Next level: <color=green>" + nextLevelValue + "</color>";
        }

        return stats;
    }

    public override void Use(CharacterBattle character)
    {
        List<CharacterBattle> teamMembers = new List<CharacterBattle>();
        teamMembers = character.BattleManager.GetAllEniemies("Enemy");
        foreach (CharacterBattle hero in teamMembers)
        {
            if (hero != null && hero.IsAlive)
            {
                hero.Heal((int)effectValue);
                Instantiate(particleEffect, hero.transform);
            }
        }
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach (CharacterBattle hero in characters)
        {
            if (hero != null && hero.IsAlive)
            {
                hero.Heal((int)effectValue);
                Instantiate(particleEffect, hero.transform);
            }
        }
    }
}
