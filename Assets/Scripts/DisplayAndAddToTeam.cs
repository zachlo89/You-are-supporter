﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayAndAddToTeam : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private ListOfHeroes listOfAvaliableHeroes;
    [SerializeField] private TextMeshProUGUI heroesCount;
    [SerializeField] private Transform spawnPanelParent;
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private Team team;
    int counter;
    private int index = 0;

    void Start()
    {
        PopulatePanel();
        delegator.changeSprites += PopulatePanel;
    }

    private void OnEnable()
    {
        PopulatePanel();
    }

    private void OnDisable()
    {
        delegator.changeSprites();
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
    public void PopulatePanel()
    {
        counter = 0;
        foreach (Transform child in spawnPanelParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < listOfAvaliableHeroes.heroesList.Count; i++)
        {
            if (listOfAvaliableHeroes.heroesList[i].isAvaliable && !team.heroesList.Contains(listOfAvaliableHeroes.heroesList[i]))
            {
                GameObject panel = Instantiate(panelPrefab, spawnPanelParent);
                panel.GetComponent<CharacterSlot>().SetSelectionPanel(selectionPanel, gameObject);
                panel.GetComponent<CharacterSlot>().PopulateHeroPanel(listOfAvaliableHeroes.heroesList[i], team, index);
                ++counter;
            }
        }

        GameObject panel1 = Instantiate(panelPrefab, spawnPanelParent);
        panel1.GetComponent<CharacterSlot>().SetSelectionPanel(selectionPanel, gameObject);
        panel1.GetComponent<CharacterSlot>().PopulateHeroPanel(null, team, index);

        heroesCount.text = "Heroes " + counter + "/" + listOfAvaliableHeroes.heroesList.Count;
    }

}
