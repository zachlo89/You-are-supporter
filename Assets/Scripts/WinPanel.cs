using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> starsList = new List<GameObject>();
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private Transform expPrefabSpwaningPoint;
    [SerializeField] private GameObject lootDropPrefab;
    [SerializeField] private Transform lootDropSpawningPoint;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private Team team;
    [SerializeField] private LootSpawner lootSpawner;
    [SerializeField] private int maxItemCount;
    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    public void WinPanelConstructor()
    {
        List<CharacterBattle> listOfHeroes = battleManager.GetAllPlayersCharacters();
        int stars = 3;
        for(int i = 0; i < listOfHeroes.Count; i++)
        {
            if (!listOfHeroes[i].IsAlive)
            {
                --stars;
            }
        }
        ShowStars(stars);
        ShowExpBars();
        SpawnLoot();
        gm.MarkAsCompleted(stars);
    }

    private void ShowStars(int starsCount)
    {
        //TODO create animation for stars
        if (starsCount <= 0)
        {
            starsCount = 1;
        }
        for(int i = 0; i < starsCount; i++)
        {
            starsList[i].SetActive(true);
        }
    }

    private void ShowExpBars()
    {
        for(int i = 0; i < team.heroesList.Count; i++)
        {
            if(team.heroesList[i] != null)
            {
                GameObject temp = Instantiate(expPrefab, expPrefabSpwaningPoint);
                var heroCopy = Instantiate(team.heroesList[i]);
                temp.GetComponent<FillTheExpBar>().FillBarConstructor(heroCopy, team.heroesList[i].GainExpirience(battleManager.GetEnemiesExpirience()));
            }
        }
    }

    private void SpawnLoot()
    {
        lootSpawner.SpawnItems();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
