﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/IncreaseMaxMana")]
public class IncreaseMaxMana : PlayerScriptableSkill
{
    public override string StatsDescription()
    {
        string stats = "<color=white>Passive skill:\n</color>";
        if (level < 1)
        {
            stats += "Increase max mana: <color=cyan>" + effectValue + "</color>";
        }
        else
        {
            stats += "Increase max mana: <color=cyan>" + effectValue + "</color>\n";
            stats += "Next level: <color=cyan>" + nextLevelValue + "</color>";
        }

        return stats;
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach (CharacterBattle hero in characters)
        {
            if (hero != null && hero.IsAlive)
            {
                hero.InreaseMaxMana((int)effectValue);
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        if (character != null && character.IsAlive)
        {
            character.InreaseMaxMana((int)effectValue);
        }
    }
}