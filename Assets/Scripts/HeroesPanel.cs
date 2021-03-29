using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroesPanel : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private ListOfHeroes listOfAvaliableHeroes;
    [SerializeField] private TextMeshProUGUI heroesCount;
    [SerializeField] private Transform spawnPanelParent;
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private GameObject equipmentPanel;
    private Team team;
    int counter;

    void Start()
    {
        PopulatePanel();
        delegator.changeSprites += PopulatePanel;
    }

    public void PopulatePanel()
    {
        counter = 0;
        foreach (Transform child in spawnPanelParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 0; i < listOfAvaliableHeroes.heroesList.Count; i++)
        {
            if (listOfAvaliableHeroes.heroesList[i].isAvaliable)
            {
                GameObject panel = Instantiate(panelPrefab, spawnPanelParent);
                panel.GetComponent<CharacterSlot>().PopulateHeroPanel(listOfAvaliableHeroes.heroesList[i], equipmentPanel, this.gameObject);
                ++counter;
            }
        }
        heroesCount.text = "Heroes " + counter + "/" + listOfAvaliableHeroes.heroesList.Count;
    }

    /*
    public void PopulateAvaliableCharacters(int teamSlotIndex, Team team)
    {
        counter = 0;
        foreach (Transform child in spawnPanelParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        this.team = team;
        foreach (ScriptableCharacter hero in listOfAvaliableHeroes.heroesList)
        {
            if (hero.isAvaliable && !team.heroesList.Contains(hero))
            {
                GameObject panel = Instantiate(panelPrefab, spawnPanelParent);
                panel.GetComponent<CharacterSlot>().PopulateHeroPanel(hero, equipmentPanel, this.gameObject);
                panel.GetComponent<CharacterSlot>().UpdateUIForTeam(teamSlotIndex, team);
                ++counter;
            }
        }
        heroesCount.text = "Heroes " + counter + "/7";
    } 
    */

}
