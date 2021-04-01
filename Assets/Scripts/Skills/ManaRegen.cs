using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/ManaRegen")]
public class ManaRegen : PlayerScriptableSkill
{
    public override string StatsDescription()
    {
        string stats = "<color=white>Passive skill:\n</color>";
        if (level < 1)
        {
            stats += "Increase mana regeneration: <color=purple>" + effectValue + "</color>";
        }
        else
        {
            stats += "Increase mana regeneration: <color=purple>" + effectValue + "</color> \n";
            stats += "Next level: <color=purple>" + nextLevelValue + "</color>";
        }

        return stats;
    }

    public override void Use(List<CharacterBattle> characters)
    {
        foreach (CharacterBattle hero in characters)
        {
            if (hero != null && hero.IsAlive)
            {
                hero.ManaRegen((int)effectValue);
            }
        }
    }

    public override void Use(CharacterBattle character)
    {
        if (character != null && character.IsAlive)
        {
            character.ManaRegen((int)effectValue);
        }
    }
}
