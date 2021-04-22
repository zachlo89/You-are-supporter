using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterScriptableSkill/SkeletonMage/SkeletonRessurection")]
public class SkeletonResurection : CharacterSkill
{
    public override void Initialize(ScriptableCharacter character)
    {
    }

    public override string StatsDescription()
    {
        return null;
    }

    public override void Use()
    {
        CharacterBattle target = battleManager.GetFrontCharacterDeadOrAlive("Player");
        target.Ressurection();
        if(particleEffects != null)
        {
            Instantiate(particleEffects, target.transform);
        }
    }
}
