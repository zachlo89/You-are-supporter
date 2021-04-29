using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
public class LobbyUpdateExpirence : MonoBehaviour
{
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private ScriptableInt questLeveledUp;
    [SerializeField] private Team team;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI expirience;
    [SerializeField] private Slider slider;
    [SerializeField] private List<Transform> spawningPoints = new List<Transform>();
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI rubinsValue;
    [SerializeField] private TextMeshProUGUI energy;
    [SerializeField] private ScriptableItemManager inventory;

    [SerializeField] private GameObject buttonNormalChest, buttonEpicChest;

    private void Start()
    {
        delegator.updateCount += UpdateLobby;
        delegator.changeSprites += UpdateLobby;
        UpdateLobby();
        if(inventory.GetInvevtory.Count > 0)
        {
            equipmentPanel.SetActive(true);
        }
    }

    private void UpdateLobby()
    {
        for(int i = 0; i < spawningPoints.Count; i++)
        {
            foreach (Transform child in spawningPoints[i])
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        characterName.text = team.heroesList[0].characterName;
        level.text = team.heroesList[0].level.ToString();
        expirience.text = team.heroesList[0].expirence + "/" + team.heroesList[0].toNextLevel;
        slider.value = (float)team.heroesList[0].expirence / (float)team.heroesList[0].toNextLevel;
        for(int i = 0; i < team.heroesList.Count; i++)
        {
            try
            {
                GameObject temp = Instantiate(team.heroesList[i].prefab, spawningPoints[i]);
                temp.transform.localScale *= 1.5f;
                temp.GetComponent<UpdateFaceAndBody>().SetCharacter(team.heroesList[i]);
                temp.GetComponent<UpdateEquipment>().EquipAll(team.heroesList[i].equipment);
                temp.GetComponentInChildren<Animator>().Play("IdleMelee");
                temp.transform.GetChild(0).GetChild(0).GetComponent<SortingGroup>().sortingOrder = 20;
            }
            catch
            {
                Debug.Log("Missing hero");
            }
        }
        

        goldValue.text = inventory.Gold.value.ToString();
        rubinsValue.text = inventory.Rubins.value.ToString();
        energy.text = inventory.Energy.value + "/" + inventory.MaxEnergy.value;

        if(inventory.GetNormalChestCount.value > 0)
        {
            buttonNormalChest.SetActive(true);
            buttonNormalChest.transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = inventory.GetNormalChestCount.value.ToString();
        } else buttonNormalChest.SetActive(false);

        if (inventory.EpicChestsCount.value > 0)
        {
            buttonEpicChest.SetActive(true);
            buttonEpicChest.transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = inventory.EpicChestsCount.value.ToString();
        } else buttonEpicChest.SetActive(false);
    }
}
