using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private Level currentLevel;
    public Level CurrentLevel
    {
        get { return currentLevel; }
    }


    [SerializeField] private ListOfHeroes listOfHeroes;
    [SerializeField] private List<Level> levelList = new List<Level>();
    [SerializeField] private Team team;


    private int counter;

    private void Awake()
    {
        if(PlayerPrefs.GetString("Level") != null)
        {
            foreach(Level level in levelList)
            {
                if(level.name == PlayerPrefs.GetString("Level"))
                {
                    currentLevel = level;
                    break;
                }
            }
        }
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateCharactersLevel();
    }


    public void SetCurrentLevel(Level level)
    {
        this.currentLevel = level;
        PlayerPrefs.SetString("Level", level.name);
    }

    public void SetNextLevel()
    {
        int index = levelList.IndexOf(currentLevel);
        this.currentLevel = levelList[index + 1];
    }

    public void MarkAsCompleted(int stars)
    {
        this.currentLevel.stars = stars;
        this.currentLevel.isPassed = true;
        int index = levelList.IndexOf(currentLevel);
        try
        {
            if (levelList[index + 1] != null && levelList.Count > index + 1)
            {
                levelList[index + 1].isAvaliable = true;
            }
        }
        catch
        {
            Debug.Log("Ups something wrong with Game Manager function MarkAsCompleted");
        }
        
    }

    private void UpdateCharactersLevel()
    {
        foreach(ScriptableCharacter character in listOfHeroes.heroesList)
        {
            if (character.isAvaliable && character.expirence > 0)
            {
                int toNextLevel = character.toNextLevel;
                int newLevel = character.level;
                for(int i = 0; i < 11; i++)
                {
                    if(character.expirence >= toNextLevel)
                    {
                        character.expirence -= toNextLevel;
                        toNextLevel = (int)(toNextLevel * 1.5f);
                        ++newLevel;
                        character.level = newLevel;
                        character.toNextLevel = toNextLevel;
                        UpdateCharacterStats(character);
                    }  
                }
            }
        }
    }

    private void UpdateCharacterStats(ScriptableCharacter character)
    {
        switch (character.characterClass)
        {
            case CharacterClass.Tank:
                    character.maxHealt += (int)(character.maxHealt * 8 / 100);
                    character.damage += (int)(character.damage * 6 / 100);
                break;
            case CharacterClass.Archer:
                character.maxHealt += (int)(character.maxHealt * 6 / 100);
                character.damage += (int)(character.damage * 8 / 100);
                break;
            case CharacterClass.Berserker:
                character.maxHealt += (int)(character.maxHealt * 7 / 100);
                character.damage += (int)(character.damage * 7 / 100);
                break;
        }
    }

    public void UpdateTeamCharactersAfterBattle()
    {
        for(int j = 0; j < team.heroesList.Count; j ++)
        {
            if(team.heroesList[j] != null)
            {
                int toNextLevel = team.heroesList[j].toNextLevel;
                int newLevel = team.heroesList[j].level;
                for (int i = 0; i < 11; i++)
                {
                    if (team.heroesList[j].expirence >= toNextLevel)
                    {
                        team.heroesList[j].expirence -= toNextLevel;
                        toNextLevel = (int)(toNextLevel * 1.5f);
                        ++newLevel;
                        team.heroesList[j].level = newLevel;
                        team.heroesList[j].toNextLevel = toNextLevel;
                        UpdateCharacterStats(team.heroesList[j]);
                    }
                }
            }
        }
    }
}
