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


    [SerializeField] private List<Level> levelList = new List<Level>();


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

    public void MarkAsCompleted()
    {
        this.currentLevel.stars = 3;
        this.currentLevel.isPassed = true;
        int index = levelList.IndexOf(currentLevel);
        levelList[index + 1].isAvaliable = true;
    }

}
