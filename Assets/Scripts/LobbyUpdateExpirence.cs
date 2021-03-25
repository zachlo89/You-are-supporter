using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUpdateExpirence : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private Team team;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI expirience;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private ScriptableItemManager inventory;
    private void Start()
    {
        UpdateLobby();
        delegator.changeSprites += UpdateLobby;
    }

    private void UpdateLobby()
    {
        foreach (Transform child in spawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        characterName.text = team.heroesList[0].name;
        level.text = team.heroesList[0].level.ToString();
        expirience.text = team.heroesList[0].expirence + "/" + team.heroesList[0].toNextLevel;
        slider.value = team.heroesList[0].expirence / team.heroesList[0].toNextLevel;
        GameObject temp = Instantiate(team.heroesList[0].prefab, spawningPoint);
        temp.transform.localScale *= 1.5f;
        temp.GetComponent<UpdateFaceAndBody>().SetCharacter(team.heroesList[0]);
        temp.GetComponent<UpdateEquipment>().EquipAll(team.heroesList[0].equipment);
        goldValue.text = inventory.Gold.value.ToString();
    }
}
