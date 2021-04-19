using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/Tank/DefenseStance")]
public class DefenseStance : CharacterSkill
{
    public float duration;
    public override void Initialize(ScriptableCharacter character)
    {
        effectValue = defaultEffectValue + (Mathf.Clamp(level, 1, maxLevel) * 5);
        nextLevelValue = defaultEffectValue + (level + 1) * 5;
    }

    public override string StatsDescription()
    {
        string stats = "Target self\n";
        if (level < 1)
        {
            stats += "Increase block chance: <color=yellow>" + effectValue + "%</color>";
        }
        else
        {
            stats += "Increase block chance: <color=yellow>" + effectValue + "%</color>\n";
            if (level < maxLevel)
            {
                stats += "Next level: <color=yellow>" + nextLevelValue + "%</color>";
            }
        }
        return stats;
    }

    public override void Use()
    {
        hero.IncreaseBlockChance((int)effectValue, duration);
        GameObject temp = Instantiate(particleEffects, hero.transform);
        Destroy(temp, duration);
    }
}
