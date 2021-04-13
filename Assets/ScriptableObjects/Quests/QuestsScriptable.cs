using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestReward
{
    gold,
    rubins,
    normalChest,
    epicChest
}

[CreateAssetMenu(menuName = "ScriptableObject/Quests")]
public class QuestsScriptable : ScriptableObject
{
    public string questName;
    public string questDescription;
    public string questValue;
    public int questRewardValue;
    public Sprite questImage;
    public Sprite rewardImage;
    public ScriptableInt currentValue;
    public ScriptableItemManager inventory;
    public QuestReward questReward;
    public bool beenClaimed = false;
    public void GetReward()
    {
        switch (questReward)
        {
            case QuestReward.gold:
                inventory.AddGold(questRewardValue);
                break;
            case QuestReward.rubins:
                inventory.AddRubins(questRewardValue);
                break;
            case QuestReward.normalChest:
                inventory.AddNormalChest(questRewardValue);
                break;
            case QuestReward.epicChest:
                inventory.AddEipicChest(questRewardValue);
                break;
        }
        beenClaimed = true;
    }
}
