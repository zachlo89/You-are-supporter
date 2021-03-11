using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    private GameManager gameManager;
    private Level level;
    [SerializeField] private Team team;
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Image backgroundImage;

    //Only for developemnet delete it later
    [SerializeField] private Level levelToGameManager;
    //end

    [SerializeField] private List<Vector3> SpwaningPoints = new List<Vector3>();
    [SerializeField] private BattleManager battleManager;


    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //To delete, only for development
        gameManager.SetCurrentLevel(levelToGameManager);
        //end

        level = gameManager.CurrentLevel;
        backgroundSprite = level.background;

        backgroundImage.sprite = backgroundSprite;
        SpawnPlayers();
        SpawnEnemies();
    }

    private void SpawnPlayers()
    {
        for(int i = 0; i < team.heroesList.Count; i++)
        {
            if(team.heroesList[i] != null)
            {
                GameObject temp = Instantiate(team.heroesList[i].prefab);
                temp.transform.localScale /= 100;
                temp.transform.position = SpwaningPoints[i];
                temp.tag = "Player";
                temp.GetComponent<CharacterBattle>().enabled = true;
                temp.GetComponent<CharacterBattle>().SetUpHero(team.heroesList[i], battleManager);
            }
        }
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < level.enemiesList.Count; i++)
        {
            if(level.enemiesList[i] != null)
            {
                GameObject temp = Instantiate(level.enemiesList[i].prefab);
                temp.transform.position = new Vector3(-SpwaningPoints[i].x, SpwaningPoints[i].y, SpwaningPoints[i].z);
                temp.GetComponentInChildren<CharacterBattle>().SetUpHero(level.enemiesList[i], battleManager);
            }
        }
    }
}
