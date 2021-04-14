using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestRewardPanel : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private Sprite normalChest;
    [SerializeField] private Sprite epicChest;
    [SerializeField] private Image chestImage;
    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private Transform rewardSpawningPoint;
    [SerializeField] private TextMeshProUGUI gold, rubins;
    [SerializeField] RandomLootGenerator looter;

    public void SpawnRewardPanel(int i)
    {
        for(int k = 2; k < rewardSpawningPoint.childCount; k++)
        {
            GameObject.Destroy(rewardSpawningPoint.GetChild(k).gameObject);
        }
        if (i <= 0)
        {
            chestImage.sprite = normalChest;
            GetGoldAndRubins(i);
            for(int j = 0; j < inventory.GetNormalChestCount.value * 2; j++)
            {
                GenerateRandomItem(i);
            }
            inventory.AddNormalChest(-inventory.GetNormalChestCount.value);
        }
        else
        {
            chestImage.sprite = epicChest;
            GetGoldAndRubins(i);
            for (int j = 0; j < inventory.EpicChestsCount.value * 2; j++)
            {
                GenerateRandomItem(i);
            }
            inventory.AddEipicChest(-inventory.EpicChestsCount.value);
        }
    }

    private void GetGoldAndRubins(int i)
    {
        if (i <= 0)
        {
            int normalGold = Random.Range(50 * inventory.GetNormalChestCount.value, 100 * inventory.GetNormalChestCount.value);
            inventory.AddGold(normalGold);
            gold.text = normalGold.ToString();
            int normalRubin = Random.Range(1 * inventory.GetNormalChestCount.value, 5 * inventory.GetNormalChestCount.value);
            inventory.AddRubins(normalRubin);
            rubins.text = normalRubin.ToString();
        }
        else
        {
            int epicGold = Random.Range(100 * inventory.EpicChestsCount.value, 250 * inventory.EpicChestsCount.value);
            inventory.AddGold(epicGold);
            gold.text = epicGold.ToString();
            int epicRubins = Random.Range(1 * inventory.EpicChestsCount.value, 5 * inventory.EpicChestsCount.value);
            inventory.AddRubins(epicRubins);
            rubins.text = epicRubins.ToString();
        }
    }

    private void GenerateRandomItem(int i)
    {
        ItemScriptable item = looter.GetRandomItems(i);
        GameObject temp = Instantiate(rewardPrefab, rewardSpawningPoint);
        temp.GetComponent<SetItemIcon>().UpdateIconUI(item);
    }
}
