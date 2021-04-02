using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Tank/Bash")]
public class Bash : CharacterSkill
{
    public int stunDuration;
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 5);
        nextLevelValue = defaultEffectValue + (level + 1) * 5;
    }

    public override string StatsDescription()
    {
        string stats = "Single target";
        if (level < 1)
        {
            stats = "Attack enemy for: <color=red>200%\n " + stunDuration + "</color> second stun chance: <color=red>" + effectValue + "%</color>";
        }
        else
        {
            stats = "Attack enemy for: <color=red>200%\n " + stunDuration + "</color> second stun chance: <color=red>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level stun chance: <color=yellow>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        int attackValue = hero.Damage * 2;
        CharacterBattle enemy = battleManager.GetFrontCharacter(hero.tag);
        enemy.GetDamage(attackValue, false);
        if (ShouldStunned())
        {
            enemy.Stunned(stunDuration);
        }
    }

    private bool ShouldStunned()
    {
        int random = Random.Range(0, 100);
        if (random <= (int)effectValue)
        {
            return true;
        }
        else return false;
    }
}
