using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUpdateExpirence : MonoBehaviour
{
    [SerializeField] private GameObject newEquipemntAlert;
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
<<<<<<< Updated upstream
        delegator.changeSprites += UpdateLobby;
=======
        if (inventory.GetInvevtory.Count > 0)
        {
            newEquipemntAlert.SetActive(true);
        }
        else newEquipemntAlert.SetActive(false);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
        temp.GetComponentInChildren<Animator>().Play("IdleMelee");
        temp.transform.GetChild(0).GetChild(0).GetComponent<SortingGroup>().sortingOrder = 20;

>>>>>>> Stashed changes
        goldValue.text = inventory.Gold.value.ToString();
    }
}
