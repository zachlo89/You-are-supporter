using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/SkeletonChampion/SkeletonSlam")]
public class SkeletonSlam : CharacterSkill
{
    public int stunDuration;
    public override void Initialize(ScriptableCharacter character)
    {
        
    }

    public override string StatsDescription()
    {
        return null;
    }

    public override void Use()
    {
        int attackValue = (int)(hero.Damage * effectValue / 100);
        List<CharacterBattle> enemiesList = battleManager.GetAllEniemies(hero.tag);
        foreach(CharacterBattle enemy in enemiesList)
        {
            if (enemy.IsAlive)
            {
                enemy.GetDamage(attackValue, false);
                if(particleEffects != null)
                {
                    Instantiate(particleEffects, enemy.transform);
                }

                if (ShouldStunned())
                {
                    enemy.Stunned(stunDuration);
                }
            }
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
