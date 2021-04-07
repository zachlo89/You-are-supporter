using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterClass
{
    Tank,
    Berserker,
    Archer,
    Supporter
}

[CreateAssetMenu (menuName = "ScriptableObject/Character")]
public class ScriptableCharacter : ScriptableObject
{
    public bool isMainCharacter;
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
    public int avaliableSkillPoints;


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

    public List<PlayerSkillTree> playerSkillTree = new List<PlayerSkillTree>();
    public List<PlayerScriptableSkill> activeSkills = new List<PlayerScriptableSkill>();

    public List<CharacterSkillTree> characterSkillTree = new List<CharacterSkillTree>();
    public List<CharacterSkill> characterActiveSkills = new List<CharacterSkill>();

    public void AddSkill(PlayerScriptableSkill skill)
    {
        
        if (isMainCharacter)
        {
            if (skill != null && !CheckIfContainsSkill(skill) && activeSkills.Count < 5)
            {
                if(activeSkills.Count < 4)
                {
                    activeSkills.Add(skill);
                } else
                {
                    for(int i = 0; i < activeSkills.Count; i++)
                    {
                        if(activeSkills[i] == null)
                        {
                            activeSkills[i] = skill;
                        }
                    }
                }
            }
        }
    }

    public void AddSkill(CharacterSkill skill)
    {
        if (!isMainCharacter)
        {
            if (skill != null && !CheckIfContainsSkill(skill) && characterActiveSkills.Count < 5)
            {
                if (characterActiveSkills.Count < 4)
                {
                    characterActiveSkills.Add(skill);
                }
                else
                {
                    for (int i = 0; i < characterActiveSkills.Count; i++)
                    {
                        if (characterActiveSkills[i] == null)
                        {
                            characterActiveSkills[i] = skill;
                        }
                    }
                }
            }
        }
    }

    public int CheckIndex(PlayerScriptableSkill skill)
    {
        int temp = -1;
        if (CheckIfContainsSkill(skill))
        {
            temp = activeSkills.IndexOf(skill);
        }
        return temp;
    }

    public int CheckIndex(CharacterSkill skill)
    {
        int temp = -1;
        if (CheckIfContainsSkill(skill))
        {
            temp = characterActiveSkills.IndexOf(skill);
        }
        return temp;
    }
    public void RemoveSkill(int index)
    {
        activeSkills.RemoveAt(index);
    }

    public void RemoveSkill(bool normalCharacter, int index)
    {
        if (normalCharacter)
        {
            characterActiveSkills.RemoveAt(index);
        }
    }

    public bool CheckIfContainsSkill(PlayerScriptableSkill skill)
    {
        if (activeSkills.Contains(skill))
        {
            return true;
        }
        else return false;
    }

    public bool CheckIfContainsSkill(CharacterSkill skill)
    {
        if (characterActiveSkills.Contains(skill))
        {
            return true;
        }
        else return false;
    }


    public bool HasAvaliableSkillSlot(bool mainCharacter)
    {
        if (mainCharacter)
        {
            if (activeSkills.Count > 4)
            {
                return false;
            }
            else return true;
        } else
        {
            if (characterActiveSkills.Count > 4)
            {
                return false;
            }
            else return true;
        }
        
    }
        
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
        ++avaliableSkillPoints;
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
