﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/HPRegenBuff")]
public class HPRegenBuff : PlayerScriptableSkill
{
    public float duration;
    public override string StatsDescription()
    {
        string stats = "Single target\n";
        if (level < 1)
        {
            stats += "Increase hp regeneration: <color=green>" + effectValue + "</color> for " + duration + " seconds\n";
            stats += "Mana cost: <color=blue>" + manaCost + "</color>\n";
        }
        else
        {
            stats += "Increase hp regeneration: <color=green>" + effectValue + "</color> for " + duration + " seconds\n";
            stats += "Mana cost: <color=blue>" + manaCost + "</color>\n";
            if(level < maxLevel)
            {
                stats += "Next level: <color=green>" + nextLevelValue + "</color>\n";
                stats += "Mana cost: <color+blue>" + manaCost + "</color>\n";
            }
        }

        return stats;
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach (CharacterBattle hero in characters)
        {
            if (hero != null && hero.IsAlive)
            {
                hero.HpRegenBuff((int)effectValue, duration);
                GameObject temp = Instantiate(particleEffect, hero.transform);
                Destroy(temp, duration);
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        if (character != null && character.IsAlive)
        {
            character.HpRegenBuff((int)effectValue, duration);
            GameObject temp = Instantiate(particleEffect, character.transform);
            Destroy(temp, duration);
        }
    }
}
