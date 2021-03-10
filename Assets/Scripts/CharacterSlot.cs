using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private ScriptableCharacter hero;
    private Team team;
    private int index;


    public void PopulateHeroPanel(ScriptableCharacter hero, GameObject equipmentPanel, GameObject heroSelectPanel)
    {
        for(int i = 0; i < starsList.Count; i++)
        {
            starsList[i].SetActive(false);
        }
        this.heroSelectPanel = heroSelectPanel;
        this.equipmentPanel = equipmentPanel;
        this.hero = hero;
        nameText.text = hero.name;
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
    }

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
            nameText.text = hero.name;
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
        }
        
        
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => AddToTeam(index, team));

    }

    public void SetEquipmentPanel()
    {
        heroSelectPanel.GetComponent<Animator>().SetTrigger("FadeOutRight");
        equipmentPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        equipmentPanel.GetComponent<EquipmentPanel>().UpdateUI(hero);
    }

    public void UpdateUIForTeam(int index, Team team)
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => AddToTeam(index, team));
        
    }

    private void AddToTeam(int index, Team team)
    {
        Debug.Log("Team name: " + team.name);
        Debug.Log("Index: " + index);
        team.AddCharacter(hero, index);
        SelectHeroes selectHeroPanel = FindObjectOfType<SelectHeroes>();
        selectHeroPanel.PopulatePanel();
        selectHeroPanel.gameObject.GetComponent<Animator>().SetTrigger("FadeIn");
        GetComponentInParent<Animator>().SetTrigger("FadeOut");
    }
}
