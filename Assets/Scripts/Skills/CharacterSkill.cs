using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterSkill : ScriptableObject
{
    public GameObject particleEffects;
    public bool isPassive;
    public bool isBought;
    public bool isAvaliable;
    public bool isBuff;
    public string skillName;
    public Sprite skillBorder;
    public SkillDifficultyLevel skillDifficultyLevel;
    public string description;
    public int level;
    public int maxLevel;
    public float coolDown;
    public int manaCost;
    public Sprite notOwnSprite;
    public Sprite icon;
    public float defaultEffectValue;
    public float effectValue;
    public float nextLevelValue;
    protected BattleManager battleManager;
    protected CharacterBattle hero;

    public abstract void Initialize(ScriptableCharacter character);

    public void SetUpHero(CharacterBattle hero)
    {
        this.hero = hero;
    }

    public void SetUpBattleManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }

    public abstract void Use();

    public abstract string StatsDescription();
}
