using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill/Dispel")]
public class Dispel : PlayerScriptableSkill
{
    public override string StatsDescription()
    {
        string stats = "Single target\n";
        stats += "Mana cost: <color=blue>" + manaCost + "</color>\n";
        return stats;
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
