using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PersistableSO saveClass;
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
        saveClass = GameObject.FindObjectOfType<PersistableSO>();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }
    public void SaveGame()
    {
        saveClass.SaveAll();
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
        saveClass.SaveAll();

    }
}
