using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/ZombieChampion/Regeneration")]
public class ZombieRegeneration : CharacterSkill
{
    public float duration;
    public override void Initialize(ScriptableCharacter character)
    {

    }

    public override string StatsDescription()
    {
        return null;
    }

    public override void Use()
    {
        float value = hero.GetMaxHP() * effectValue / 100;
        hero.HpRegenBuff((int)value, duration);
        Instantiate(particleEffects, hero.transform);
    }
}
