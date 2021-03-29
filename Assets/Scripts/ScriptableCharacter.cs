﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterClass
{
    Tank,
    Berserker,
    Archer
}

[CreateAssetMenu (menuName = "ScriptableObject/Character")]
public class ScriptableCharacter : ScriptableObject
{
    public int expToGiveToThePlayers;
    public string characterName;
    public int buyValue;
    public bool isAlive;
    public int damage;
    public int armor;
    public int maxHealt;
    public int hpRegen;
    public int maxMana;
    public int manaRegen;
    public int attackRate;
    public float critChance;
    public float critDamageMultiplay;
    public int blockChance;
    public int dogdeChance;
    public int expirence;
    public int toNextLevel;
    public int level;
    [Range(1, 5)]
    public int stars;
    public Equipment equipment;
    public bool isAvaliable;


    public CharacterClass characterClass;

    public GameObject prefab;

    public Sprite headSprites;
    public Sprite hairsSprites;
    public Sprite eyesSprites;
    public Sprite eyebrowsSprites;
    public Sprite earsSprites;
    public Sprite mouthSprites;
    public Sprite beardSprites;
    public Sprite torso;
    public Sprite pelvis;
    public Sprite armL;
    public Sprite forearmoL;
    public Sprite armR;
    public Sprite forearmR;
    public Sprite handL;
    public Sprite handR;
    public Sprite legL;
    public Sprite legR;
    public Sprite shinL;
    public Sprite shinR;
    public Color bodyColor;
    public Color hairColor;

    public int GainExpirience(int expirence)
    {
        int gainedExpirience = expirence / level;
        this.expirence += gainedExpirience;
        while (CheckIfLevelUp())
        {
            AdjustStatsToLevel();
        }

        return gainedExpirience;
    }

    private bool CheckIfLevelUp()
    {
        if (expirence >= toNextLevel)
        {
            return true;
        }
        else return false;
    }

    private void AdjustStatsToLevel()
    {
        expirence -= toNextLevel;
        toNextLevel = (int)(toNextLevel * 1.5f);
        ++level;
        switch (characterClass)
        {
            case CharacterClass.Tank:
                maxHealt += (int)(maxHealt * 8 / 100);
                damage += (int)(damage * 6 / 100);
                break;
            case CharacterClass.Archer:
                maxHealt += (int)(maxHealt * 6 / 100);
                damage += (int)(damage * 8 / 100);
                break;
            case CharacterClass.Berserker:
                maxHealt += (int)(maxHealt * 7 / 100);
                damage += (int)(damage * 7 / 100);
                break;
        }
    }
}
