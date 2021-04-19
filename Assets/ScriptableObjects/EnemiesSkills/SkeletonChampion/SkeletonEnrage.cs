using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/SkeletonChampion/Enrage")]
public class SkeletonEnrage : CharacterSkill
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
        float attackIncrease = hero.Damage * effectValue / 100;
        float defenceDecrease = hero.Armor * effectValue / 100;
        hero.SetArmor(hero.Armor + (int)defenceDecrease);
        hero.SetDamage(hero.Damage + (int)attackIncrease);
        if(particleEffects != null)
        {
            GameObject effects = Instantiate(particleEffects, hero.transform);
            Destroy(effects, duration);
        }
    }
}
