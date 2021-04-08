using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
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
    [SerializeField] private TextMeshProUGUI rubinsValue;
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
        characterName.text = team.heroesList[0].characterName;
        level.text = team.heroesList[0].level.ToString();
        expirience.text = team.heroesList[0].expirence + "/" + team.heroesList[0].toNextLevel;
        slider.value = team.heroesList[0].expirence / team.heroesList[0].toNextLevel;
        GameObject temp = Instantiate(team.heroesList[0].prefab, spawningPoint);
        temp.transform.localScale *= 1.5f;
        temp.GetComponent<UpdateFaceAndBody>().SetCharacter(team.heroesList[0]);
        temp.GetComponent<UpdateEquipment>().EquipAll(team.heroesList[0].equipment);
        /*foreach (Transform child in temp.transform.GetChild(0))
        {
            if(child.GetComponent<SpriteRenderer>() != null)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder /= 10;
            }
            foreach(Transform grandChild in child)
            {
                if (grandChild.GetComponent<SpriteRenderer>() != null)
                {
                    grandChild.GetComponent<SpriteRenderer>().sortingOrder /= 10;
                }
                foreach (Transform grandgrandChild in grandChild)
                {
                    if (grandgrandChild.GetComponent<SpriteRenderer>() != null)
                    {
                        grandgrandChild.GetComponent<SpriteRenderer>().sortingOrder /= 10;
                    }
                    foreach (Transform grandgrandgrandChild in grandgrandChild)
                    {
                        if (grandgrandgrandChild.GetComponent<SpriteRenderer>() != null)
                        {
                            grandgrandgrandChild.GetComponent<SpriteRenderer>().sortingOrder /= 10;
                        }
                    }
                }
            }
        }
        */
        temp.transform.GetChild(0).GetChild(0).GetComponent<SortingGroup>().sortingOrder = 20;

        goldValue.text = inventory.Gold.value.ToString();
        rubinsValue.text = inventory.Rubins.value.ToString();
    }

}
