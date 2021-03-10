using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableCharacter")]
public class ScriptableCharacter : ScriptableObject
{
    public bool isAlive;
    public int damage;
    public int armor;
    public int maxHealt;
    public int maxMana;
    public int manaRegen;
    public int attackRate;
    public int expirence;
    public int toNextLevel;
    public int level;
    [Range(1, 5)]
    public int stars;
    public Equipment equipment;
    public bool isAvaliable;
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
}
