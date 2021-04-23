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
    [SerializeField] private SpriteRenderer backgroundImage;

    [SerializeField] private List<Vector3> spwaningPoints = new List<Vector3>();
    [SerializeField] private BattleManager battleManager;

    [SerializeField] private Slider manabar;
    [SerializeField] private PlayerSKills playerSKills;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        level = gameManager.CurrentLevel;
        backgroundSprite = level.background;

        backgroundImage.sprite = backgroundSprite;
        SpawnPlayers();
        SpawnEnemies();
    }

    private void SpawnPlayers()
    {
        int counter = 0;
        for(int i = team.heroesList.Count -1; i >= 0; i--)
        {
            try
            {
                GameObject temp = Instantiate(team.heroesList[i].prefab);
                temp.transform.localScale /= 100;
                temp.transform.position = spwaningPoints[counter++];
                temp.tag = "Player";
                temp.GetComponent<UpdateFaceAndBody>().SetUpFace(team.heroesList[i]);
                temp.GetComponent<UpdateEquipment>().EquipAll(team.heroesList[i].equipment);
                temp.GetComponent<CharacterBattle>().enabled = true;
                temp.GetComponent<CharacterBattle>().SetUpHero(team.heroesList[i], battleManager);
                temp.AddComponent<PlayerSkillsEffect>();
                if (team.heroesList[i].isMainCharacter)
                {
                    temp.GetComponent<CharacterBattle>().MainHeroSetUpManaBar(manabar);
                    temp.GetComponent<CharacterBattle>().SetUpPlayerSkills(playerSKills);
                }
            } catch
            {
                Debug.Log("Missing hero");
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
                temp.transform.localScale /= 100;
                temp.transform.rotation = Quaternion.Euler(0, 180, 0);
                temp.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 0);
                temp.transform.position = new Vector3(-spwaningPoints[i].x, spwaningPoints[i].y, spwaningPoints[i].z);
                temp.tag = "Enemy";
                try
                {
                    temp.GetComponent<UpdateFaceAndBody>().SetUpFace(level.enemiesList[i]);
                    temp.GetComponent<UpdateEquipment>().EquipAll(level.enemiesList[i].equipment);
                } catch
                {
                    Debug.Log("Missing components");
                }

                temp.GetComponent<CharacterBattle>().enabled = true;
                temp.GetComponent<CharacterBattle>().SetUpHero(level.enemiesList[i], battleManager);
                temp.AddComponent<PlayerNegativeEffects>();
            }
        }
    }
}
