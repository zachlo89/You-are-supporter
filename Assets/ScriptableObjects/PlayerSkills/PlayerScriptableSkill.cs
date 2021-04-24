﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillDifficultyLevel
{
    Novice,
    Apprentice,
    Adept,
    Expert,
    Master
}

[CreateAssetMenu(menuName = "ScriptableObject/PlayerScriptableSkill")]
public abstract class PlayerScriptableSkill : ScriptableObject
{
    public GameObject particleEffect;
    public bool isPassive;
    public bool isBought;
    public bool isAvaliable;
    public bool isBuff;
    public bool isDebuf;
    public string skillName;
    public Sprite skillBorder;
    public SkillDifficultyLevel skillDifficultyLevel;
    public string description;
    public int level;
    public int maxLevel;
    public float coolDown;
    public int manaCost;
    public int nextLevelManaCost;
    public Sprite notOwnSprite;
    public Sprite icon;
    public float defaultEffectValue;
    public float effectValue;
    public float nextLevelValue;
    public void Initialize(ScriptableCharacter character)
    {
        effectValue = (1 + (character.level / 10f)) * defaultEffectValue * Mathf.Clamp(level, 1, maxLevel);
        manaCost = (int)((1 + (level / 10)) * manaCost);
        nextLevelValue = (1 + (character.level / 10f)) * defaultEffectValue * (level+1);
        nextLevelManaCost = (int)((1 + ((level+1) / 10)) * manaCost);
    }

    public abstract void Use(List<CharacterBattle> characters);
    public abstract void Use(CharacterBattle character);

    public abstract string StatsDescription();
    
}
