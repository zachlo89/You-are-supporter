using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This should be splited for 2 classes, but forgot to do that
public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expirenceText;
    [SerializeField] private Slider expirienceSlider;
    [SerializeField] private List<GameObject> starsList;
    [SerializeField] private Transform spawnigngPoint;
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject heroSelectPanel;
    private GameObject selectHeroPanel, dispalyAndAddToTeamPanel;
    private ScriptableCharacter hero;
    private Team team;
    private int index;

// Used by HeroesPanel
    public void PopulateHeroPanel(ScriptableCharacter hero, GameObject equipmentPanel, GameObject heroSelectPanel)
    {
        for(int i = 0; i < starsList.Count; i++)
        {
            starsList[i].SetActive(false);
        }
        this.heroSelectPanel = heroSelectPanel;
        this.equipmentPanel = equipmentPanel;
        this.hero = hero;
        nameText.text = hero.characterName;
        levelText.text = hero.level.ToString();
        expirenceText.text = hero.expirence.ToString() + "/" + hero.toNextLevel.ToString();
        expirienceSlider.value = (float)hero.expirence / (float)hero.toNextLevel;

        for(int i = 0; i < hero.stars; i++)
        {
            starsList[i].SetActive(true);
        }

        GameObject temp = Instantiate(hero.prefab, spawnigngPoint);
        temp.GetComponent<UpdateFaceAndBody>().SetUpFace(hero);
        temp.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
        temp.GetComponentInChildren<Animator>().Play("IdleMelee");
    }

    // Called when pressed (move from HeroesPanel to EquipmentPanel)

    public void SetEquipmentPanel()
    {
        equipmentPanel.SetActive(true);
        equipmentPanel.GetComponent<EquipmentPanel>().UpdateUI(hero);
    }


    //Used by DispalyAndAddToTheTeam
    public void PopulateHeroPanel(ScriptableCharacter hero, Team team, int index)
    {
        this.hero = hero;
        this.team = team;
        this.index = index;
        if(hero != null)
        {
            for (int i = 0; i < starsList.Count; i++)
            {
                starsList[i].SetActive(false);
            }
            nameText.text = hero.characterName;
            levelText.text = hero.level.ToString();
            expirenceText.text = hero.expirence.ToString() + "/" + hero.toNextLevel.ToString();
            expirienceSlider.value = (float)hero.expirence / (float)hero.toNextLevel;

            for (int i = 0; i < hero.stars; i++)
            {
                starsList[i].SetActive(true);
            }

            GameObject temp = Instantiate(hero.prefab, spawnigngPoint);
            temp.GetComponent<UpdateFaceAndBody>().SetUpFace(hero);
            temp.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
            temp.GetComponentInChildren<Animator>().Play("IdleMelee");
        }
        
        
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => AddToTeam(index, team));

    }

    // Used when you want to add avaliable hero to the team in DispalyAndAddToTheTeam
    private void AddToTeam(int index, Team team)
    {
        team.AddCharacter(hero, index);
        selectHeroPanel.gameObject.SetActive(true);
        selectHeroPanel.GetComponent<SelectHeroes>().PopulatePanel();
        dispalyAndAddToTeamPanel.SetActive(false);
    }

    public void SetSelectionPanel(GameObject selectionPanel, GameObject dispalyAndAddToTeamPanel)
    {
        this.selectHeroPanel = selectionPanel;
        this.dispalyAndAddToTeamPanel = dispalyAndAddToTeamPanel;
    }
}
