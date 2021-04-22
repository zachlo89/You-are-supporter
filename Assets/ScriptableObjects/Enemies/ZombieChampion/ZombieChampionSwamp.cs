using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/ZombieChampion/Swamp")]
public class ZombieChampionSwamp : CharacterSkill
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
        foreach (CharacterBattle enemy in battleManager.GetAllEniemies(hero.tag))
        {
            if (enemy.IsAlive)
            {
                enemy.Bleed2(duration, (int)(hero.Damage * effectValue / 100));
                GameObject temp = Instantiate(particleEffects, enemy.transform);
                Destroy(temp, duration);
            }
        }
    }

}
