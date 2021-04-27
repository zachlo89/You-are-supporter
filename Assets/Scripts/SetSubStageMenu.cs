using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetSubStageMenu : MonoBehaviour
{
    [SerializeField] private GameObject parentPosition;
    [SerializeField] private GameObject subStageIcon;
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI rubinsValue;
    [SerializeField] private TextMeshProUGUI energyValue;
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private ScriptableItemManager inventory;

    private void Start()
    {
        delegator.updateCount += UpdateStatsCount;
        UpdateStatsCount();
    }

    private void OnEnable()
    {
        popupPanel.SetActive(false);
    }
    private void UpdateStatsCount()
    {
        goldValue.text = inventory.Gold.value.ToString();
        rubinsValue.text = inventory.Rubins.value.ToString();
        energyValue.text = inventory.Energy.value + "/" + inventory.MaxEnergy.value;
    }
    public void SetSubstage(Stage stage)
    {
        foreach (Transform child in parentPosition.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < stage.levelsList.Count; i++)
        {
            GameObject temp = Instantiate(subStageIcon, parentPosition.transform);
            bool hasPrevious;
            bool hasNext;
            if (i == 0)
            {
                hasPrevious = false;
            } else hasPrevious = true;
            if (i == stage.levelsList.Count - 1)
            {
                hasNext = false;
            } else hasNext = true;
            temp.GetComponent<StageSubSelectMenu>().UpdateSubStageMenuUI(stage.levelsList[i], i+1, hasPrevious, hasNext);
            temp.GetComponent<StageSubSelectMenu>().SetPopupPanel(popupPanel);
            stageName.text = stage.name;
        }
    }


}
