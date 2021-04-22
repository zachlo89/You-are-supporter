using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/SkeletonMage/SkeletonCurse")]
public class SkeletonCurse : CharacterSkill
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
        foreach(CharacterBattle enemy in battleManager.GetAllEniemies(hero.tag))
        {
            enemy.AttackBuff((int)(-enemy.Damage * effectValue / 100), duration);
            enemy.Haste((int)(-enemy.AttackRate * effectValue / 100), duration);
            enemy.Defence((int)(-enemy.Armor * effectValue / 100), duration);
            if(particleEffects != null)
            {
                GameObject temp = Instantiate(particleEffects, enemy.transform);
                Destroy(temp, duration);
            }
        }
    }
}
