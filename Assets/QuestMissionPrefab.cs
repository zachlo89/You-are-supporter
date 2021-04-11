using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMissionPrefab : MonoBehaviour
{
    [SerializeField] private Sprite missionFininshed;
    [SerializeField] private Image questImage, rewardImage;
    [SerializeField] private TextMeshProUGUI questName, questDescription, textSlider, textAmount;
    [SerializeField] private Slider slider;
    [SerializeField] private Button buttonGet;
    [SerializeField] private GameObject getPanel;
    private QuestsScriptable quest;
    private QuestPanel questPanel;

    public void Constructor(QuestsScriptable quest, QuestPanel questPanel)
    {
        this.questPanel = questPanel;
        this.quest = quest;
        questImage.sprite = quest.questImage;
        rewardImage.sprite = quest.rewardImage;
        questName.text = quest.questName;
        questDescription.text = quest.questDescription;
        textSlider.text = quest.currentValue.value + "/" + quest.questValue;
        textAmount.text = quest.questRewardValue.ToString();
        slider.value = (float)quest.currentValue.value / float.Parse(quest.questValue);

        getPanel.GetComponentInChildren<TextMeshProUGUI>().text = quest.questRewardValue.ToString();
        getPanel.transform.GetChild(0).GetComponent<Image>().sprite = quest.rewardImage;


        if(quest.currentValue.value >= int.Parse(quest.questValue))
        {
            GetComponent<Image>().sprite = missionFininshed;
            slider.gameObject.SetActive(false);
            rewardImage.transform.parent.gameObject.SetActive(false);
            getPanel.gameObject.SetActive(true);
            buttonGet.gameObject.SetActive(true);
        } else
        {
            getPanel.gameObject.SetActive(false);
            buttonGet.gameObject.SetActive(false);
        }
    }

    public void ClaimReward()
    {
        quest.GetReward();
        quest.beenClaimed = true;
        questPanel.CheckQuestCount();
        Destroy(gameObject);
    }
}
