using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPanel : MonoBehaviour
{

    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private LobbyUpdateExpirence lobby;
    [SerializeField] private List<QuestsScriptable> questList = new List<QuestsScriptable>();

    [SerializeField] private GameObject questMissionPrefab;
    [SerializeField] private Transform questMissionsSpawningPoint;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private TextMeshProUGUI gold, rubins;
    
    [SerializeField] private Button rightSidePanelButton;
    [SerializeField] private List<int> rightSideRewardsAmount = new List<int>();
    [SerializeField] private List<QuestReward> rewardsList = new List<QuestReward>();
    [SerializeField] private List<GameObject> multiRewardsList = new List<GameObject>();
    private bool multiRewardRecived = false;
    private void Start()
    {
        CheckQuestCount();
        foreach (Transform child in questMissionsSpawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < questList.Count; i++)
        {
            if (!questList[i].beenClaimed)
            {
                GameObject temp = Instantiate(questMissionPrefab, questMissionsSpawningPoint);
                temp.GetComponent<QuestMissionPrefab>().Constructor(questList[i], this);
            }
        }
        UpdateGoldAndRubins();
        delegator.updateCount += UpdateGoldAndRubins;
        gameObject.SetActive(false);
        RightSidePanel();
    }

    private void UpdateGoldAndRubins()
    {
        gold.text = inventory.Gold.value.ToString();
        rubins.text = inventory.Rubins.value.ToString();
    }

    private void RightSidePanel()
    {
        for(int i = 0; i < multiRewardsList.Count; i++)
        {
            multiRewardsList[i].GetComponentInChildren<TextMeshProUGUI>().text = rightSideRewardsAmount[i].ToString();
        }
    }

    public void CollectReward()
    {
        multiRewardRecived = true;
        for(int i = 0; i < rewardsList.Count; i++)
        {
            switch (rewardsList[i])
            {
                case QuestReward.gold:
                    inventory.AddGold(rightSideRewardsAmount[i]);
                    break;
                case QuestReward.rubins:
                    inventory.AddRubins(rightSideRewardsAmount[i]);
                    break;
                case QuestReward.normalChest:
                    inventory.AddNormalChest(rightSideRewardsAmount[i]);
                    break;
                case QuestReward.epicChest:
                    inventory.AddEipicChest(rightSideRewardsAmount[i]);
                    break;
            }
        }
        rightSidePanelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Claimed";
        rightSidePanelButton.interactable = false;
    }

    public int CheckQuestCount()
    {
        int counter = 0;
        for (int i = 0; i < questList.Count; i++)
        {
            if (!questList[i].beenClaimed)
            {
                ++counter;
            }
        }
        if (counter == 0 && !multiRewardRecived)
        {
            lobby.ShowQuestAllert(counter + 1);
            rightSidePanelButton.interactable = true;
        }
        else
        {
            lobby.ShowQuestAllert(counter);
            rightSidePanelButton.interactable = false;
        }
        
        return counter;
    }
}
