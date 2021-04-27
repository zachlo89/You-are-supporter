using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinPanel : MonoBehaviour
{
    [SerializeField] private ScriptableInt questPlays;
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
    private AudioManager audioManager;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    public void WinPanelConstructor()
    {
        audioManager.Play("Win");
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
        questPlays.value += 1;
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
        audioManager.StopPlaying();
        audioManager.Play("Lobby");
        SceneManager.LoadScene("Lobby");
    }
}
