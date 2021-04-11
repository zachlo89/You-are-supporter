using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
public class LobbyUpdateExpirence : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private ScriptableInt questLeveledUp;
    [SerializeField] private Team team;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI expirience;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI rubinsValue;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private GameObject questAlarm;
    [SerializeField] private GameObject skillsAlarm;
    private bool isLeveledUp = false;
    private void Start()
    {
        delegator.levelUppp += AlarmLevelUp;
        delegator.changeSprites += UpdateLobby;
        UpdateLobby();
    }

    private void UpdateLobby()
    {
        foreach (Transform child in spawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        characterName.text = team.heroesList[0].characterName;
        level.text = team.heroesList[0].level.ToString();
        expirience.text = team.heroesList[0].expirence + "/" + team.heroesList[0].toNextLevel;
        slider.value = team.heroesList[0].expirence / team.heroesList[0].toNextLevel;
        GameObject temp = Instantiate(team.heroesList[0].prefab, spawningPoint);
        temp.transform.localScale *= 1.5f;
        temp.GetComponent<UpdateFaceAndBody>().SetCharacter(team.heroesList[0]);
        temp.GetComponent<UpdateEquipment>().EquipAll(team.heroesList[0].equipment);
        temp.transform.GetChild(0).GetChild(0).GetComponent<SortingGroup>().sortingOrder = 20;

        goldValue.text = inventory.Gold.value.ToString();
        rubinsValue.text = inventory.Rubins.value.ToString();

        if (isLeveledUp)
        {
            questAlarm.SetActive(true);
        }
        else questAlarm.SetActive(false);
    }

    public int ShowQuestAllert(int counter)
    {
        if(counter == 0)
        {
            questAlarm.SetActive(false);
        } else
        {
            questAlarm.SetActive(true);
            questAlarm.GetComponentInChildren<TextMeshProUGUI>().text = counter.ToString();
        }
        return counter;
    }

    private void AlarmLevelUp()
    {
        ++questLeveledUp.value;
        isLeveledUp = true;
    }

    public void RemoveLevelUpAlarm()
    {
        isLeveledUp = false;
    }

}
