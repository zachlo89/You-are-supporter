using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    private BattleManager battleManager;
    private CharacterBattle character;

    [SerializeField] private float tankSKill1Multiply;
    [SerializeField] private float tankSKill1Cooldown;
    [SerializeField] private float berserkerSkill1Multiply;
    [SerializeField] private float berserkerSKill1Cooldown;
    [SerializeField] private float archerSkill1Multiply;
    [SerializeField] private float archerSKill1Cooldown;

    private int tankSKill2RegenValue;
    [SerializeField] private float tankSkill2Duration;
    [SerializeField] private float tankSKill2Cooldown;
    [SerializeField] private int berserkerSkill2Multiply;
    [SerializeField] private float berserkerSkill2Duration;
    [SerializeField] private float berserkerSKill2Cooldown;
    [SerializeField] private float archerSkill2Multiply;
    [SerializeField] private float archerSKill2Cooldown;
    private void Start()
    {
        battleManager = GetComponent<BattleManager>();
    }

    public float Skill1(CharacterBattle character)
    {
        this.character = character;
        switch (character.HeroClass)
        {
            case CharacterClass.Tank:
                return TankSkill1(character);
            case CharacterClass.Berserker:
                return BerserkerSKill1(character);
            case CharacterClass.Archer:
                return ArcherSKill1(character);
            default:
                return 0;
        }
    }

    public float Skill2(CharacterBattle character)
    {
        this.character = character;
        switch (character.HeroClass)
        {
            case CharacterClass.Tank:
                return TankSkill2(character);
            case CharacterClass.Berserker:
                return BerserkerSKill2(character);
            case CharacterClass.Archer:
                return ArcherSKill2(character);
            default:
                return 0;
        }
    }

    private float TankSkill1(CharacterBattle character)
    {
        CharacterBattle enemy = battleManager.GetFrontCharacter(character.tag);
        if (CritAttack())
        {
            enemy.GetDamage((int)(character.Damage * tankSKill1Multiply * character.CriticalMultiply), true);
        } else enemy.GetDamage((int)(character.Damage * tankSKill1Multiply), false);

        return tankSKill1Cooldown;
    }

    private float BerserkerSKill1(CharacterBattle character)
    {
        for(int i = 0; i < berserkerSkill1Multiply; i++)
        {
            CharacterBattle enemy = battleManager.GetFrontCharacter(character.tag);
            if (CritAttack())
            {
                enemy.GetDamage((int)(character.Damage * character.CriticalMultiply), true);
            } else enemy.GetDamage((int)(character.Damage),false);
        }
        return berserkerSKill1Cooldown;
    }

    private float ArcherSKill1(CharacterBattle character)
    {
        CharacterBattle enemy = battleManager.GetFrontCharacter(character.tag);
        enemy.GetDamage((int)(character.Damage * archerSkill1Multiply), true);
        return archerSKill1Cooldown;
    }

    private float TankSkill2(CharacterBattle character)
    {
        StartCoroutine(Regeneration(character));
        return tankSKill2Cooldown;
    }

    IEnumerator Regeneration(CharacterBattle character)
    {
        tankSKill2RegenValue = (int)(character.GetMaxHP() * 2 / 100);
        character.AddRegeneration(tankSKill2RegenValue);
        yield return new WaitForSeconds(tankSkill2Duration);
        character.AddRegeneration(-tankSKill2RegenValue);
    }

    private float BerserkerSKill2(CharacterBattle character)
    {
        StartCoroutine(Rage(character));
        return berserkerSKill2Cooldown;
    }

    IEnumerator Rage(CharacterBattle character)
    {
        int armor = character.Armor;
        int damage = character.Damage;

        character.SetArmor(0);
        character.SetDamage(damage * berserkerSkill2Multiply);

        yield return new WaitForSeconds(berserkerSkill2Duration);

        character.SetArmor(armor);
        character.SetDamage(damage);
    }

    private float ArcherSKill2(CharacterBattle character)
    {
        List<CharacterBattle> enemiesList = new List<CharacterBattle>();
        enemiesList = battleManager.GetAllEniemies(character.tag);

        foreach(CharacterBattle enemy in enemiesList)
        {
            if (CritAttack())
            {
                enemy.GetDamage((int)(character.Damage * archerSkill2Multiply * character.CriticalMultiply), true);
            }
            else enemy.GetDamage((int)(character.Damage * archerSkill2Multiply), false);

        }
        
        return archerSKill2Cooldown;
    }

    private bool CritAttack()
    {
        int random = Random.Range(0, 100);
        if (random <= character.CriticalChance)
        {
            return true;
        }
        else return false;
    }
}
