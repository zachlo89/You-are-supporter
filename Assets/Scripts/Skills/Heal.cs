using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/Heal")]
public class Heal : PlayerScriptableSkill
{
    public override string StatsDescription()
    {
        string stats = "";
        if (level < 1)
        {
            stats = "Heal: <color=green>" + effectValue + "</color> single target";
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
        if(character != null)
        {
            character.Heal((int)effectValue);
        }
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach(CharacterBattle hero in characters)
        {
            if(hero!= null && hero.IsAlive)
            {
                hero.Heal((int)effectValue);
            }
        }
    }
}
