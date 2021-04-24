using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopPanel : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI rubinsValue;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private List<ShopRewardIcon> buttonsList = new List<ShopRewardIcon>();

    private void Start()
    {
        delegator.updateCount += CheckButtonsAvaliability;
        CheckButtonsAvaliability();
    }

    private void CheckButtonsAvaliability()
    {
        goldValue.text = inventory.Gold.value.ToString();
        rubinsValue.text = inventory.Rubins.value.ToString();
        foreach (ShopRewardIcon reward in buttonsList)
        {
            reward.CheckIfAvaliable();
        }
    }
}
