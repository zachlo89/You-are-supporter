using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopRewardIcon : MonoBehaviour
{
    [SerializeField] private ShopRewardScriptable shopReward;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private AdsManager adsManager;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        CheckIfAvaliable();
    }

    public void CheckIfAvaliable()
    {
        if (shopReward.cost <= inventory.Rubins.value)
        {
            button.interactable = true;
        }
        else button.interactable = false;

        if(adsManager != null && shopReward.reward == ShopRewardReward.rubins)
        {
            if (adsManager.CheckIfAddIsReady())
            {
                button.interactable = true;
            }
            else button.interactable = false;
        }
    }

    public void BuyReward()
    {
        if(inventory.Rubins.value >= shopReward.cost)
        {
            inventory.AddRubins(-shopReward.cost);
        }
        switch (shopReward.reward)
        {
            case ShopRewardReward.normalChest:
                inventory.AddNormalChest(shopReward.count);
                break;
            case ShopRewardReward.epicChest:
                inventory.AddEipicChest(shopReward.count);
                break;
            case ShopRewardReward.gold:
                inventory.AddGold(shopReward.count);
                break;
            case ShopRewardReward.rubins:
                adsManager.ShowRewardedVideo();
                break;
        }
    }
}
