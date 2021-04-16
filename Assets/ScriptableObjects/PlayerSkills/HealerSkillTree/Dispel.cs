﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/Dispel")]
public class Dispel : PlayerScriptableSkill
{
    public override string StatsDescription()
    {
        return "Single target";
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach (CharacterBattle hero in characters)
        {
            if (hero != null && hero.IsAlive)
            {
                hero.Dispel();
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        character.Dispel();
    }
}