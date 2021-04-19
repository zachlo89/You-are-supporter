using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHeroToTeam : MonoBehaviour
{
    [SerializeField] private Team team;
    private GameObject heroesPanel;
    private GameObject selectHeroPanel;

    public void AddSelectHeroPanel(GameObject heroesPanel, GameObject selectHeroPanel)
    {
        this.heroesPanel = heroesPanel;
        this.selectHeroPanel = selectHeroPanel;
    }


    public void OpenHeroesList()
    {
        selectHeroPanel.SetActive(false);
        heroesPanel.SetActive(true);
        heroesPanel.GetComponent<DisplayAndAddToTeam>().SetIndex(transform.GetSiblingIndex());
        heroesPanel.GetComponent<DisplayAndAddToTeam>().PopulatePanel();
    }
}
