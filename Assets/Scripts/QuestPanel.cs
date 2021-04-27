using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private GameObject questAlarm;
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private LobbyUpdateExpirence lobby;
    [SerializeField] private List<QuestsScriptable> questList = new List<QuestsScriptable>();

    [SerializeField] private GameObject questMissionPrefab;
    [SerializeField] private Transform questMissionsSpawningPoint;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private TextMeshProUGUI gold, rubins, energy;
    
    [SerializeField] private Button rightSidePanelButton;
    [SerializeField] private List<int> rightSideRewardsAmount = new List<int>();
    [SerializeField] private List<QuestReward> rewardsList = new List<QuestReward>();
    [SerializeField] private List<GameObject> multiRewardsList = new List<GameObject>();
    [SerializeField] private ScriptableBool multiRewardRecived;

    private void Start()
    {
        delegator.updateCount += UpdateGoldAndRubins;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        int counter = CheckQuestCount();
        if(counter > 0)
        {
            questAlarm.SetActive(true);
            questAlarm.GetComponentInChildren<TextMeshProUGUI>().text = counter.ToString();
        } else
        {
            questAlarm.SetActive(false);
        }
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
        RightSidePanel();
    }
    private void OnEnable()
    {
        CheckQuestCount();
    }
    private void UpdateGoldAndRubins()
    {
        gold.text = inventory.Gold.value.ToString();
        rubins.text = inventory.Rubins.value.ToString();
        energy.text = inventory.Energy.value + "/" + inventory.MaxEnergy.value;
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
        multiRewardRecived.value = true;
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
            if (!questList[i].beenClaimed && (questList[i].currentValue.value >= questList[i].questValue))
            {
                ++counter;
            }
        }
        if (CheckIfAllQuestBeenMade())
        {
            ++counter;
        } 
        
        return counter;
    }

    private bool CheckIfAllQuestBeenMade()
    {
        int counter = 0;
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].beenClaimed)
            {
                ++counter;
            }
        }
        if (counter == questList.Count && !multiRewardRecived)
        {
            rightSidePanelButton.interactable = true;
            return true;
        }
        else
        {
            rightSidePanelButton.interactable = false;
            return false;
        }
        
    }
}
