using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetSubStageMenu : MonoBehaviour
{
    [SerializeField] private GameObject parentPosition;
    [SerializeField] private GameObject subStageIcon;
    [SerializeField] private TextMeshProUGUI stageName;

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
            stageName.text = stage.name;
        }
    }
}
