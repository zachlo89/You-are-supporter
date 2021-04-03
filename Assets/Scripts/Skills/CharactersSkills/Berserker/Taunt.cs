using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Berserker/Taunt")]
public class Taunt : CharacterSkill
{
    public float duration;
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 5);
        nextLevelValue = defaultEffectValue + (level + 1) * 5;
    }

    public override string StatsDescription()
    {
        string stats = "Multi target\n";
        if (level < 1)
        {
            stats += "Reduce enemies defence for: <color=yellow>" + effectValue + "</color> seconds.";
        }
        else
        {
            stats += "Reduce enemies defence for: <color=yellow>" + effectValue + "</color> seconds.\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=yellow>" + nextLevelValue + "</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        List<CharacterBattle> enemiesList = battleManager.GetAllEniemies(hero.tag);
        for(int i = 0; i < enemiesList.Count; i++)
        {
            if (enemiesList[i].IsAlive)
            {
                enemiesList[i].Defence(-(int)effectValue, duration);
            }
        }
    }
}
