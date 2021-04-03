using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/DamageBuff")]
public class DamageBuff : PlayerScriptableSkill
{
    public float buffDuration;
    public override string StatsDescription()
    {
        string stats = "Single target";
        if (level < 1)
        {
            stats = "Increase attack: <color=red>" + effectValue + "</color> single target for " + buffDuration + " seconds";
        }
        else
        {
            stats = "Increase attack: <color=red>" + effectValue + "</color> single target for " + buffDuration + " seconds\n";
            stats += "Next level: <color=red>" + nextLevelValue + "</color>";
        }
        return stats;
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach (CharacterBattle hero in characters)
        {
            if(hero != null && hero.IsAlive)
            {
                hero.AttackBuff((int)effectValue, buffDuration);
                GameObject temp = Instantiate(particleEffect, hero.transform);
                Destroy(temp, buffDuration);
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        if(character != null)
        {
            character.AttackBuff((int)effectValue, buffDuration);
            GameObject temp = Instantiate(particleEffect, character.transform);
            Destroy(temp, buffDuration);
        }
    }

}
