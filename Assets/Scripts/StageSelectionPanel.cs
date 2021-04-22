using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectionPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> locks = new List<GameObject>();
    [SerializeField] private List<Button> stageButtons = new List<Button>();
    [SerializeField] private List<Stage> stageList = new List<Stage>();
    private void Start()
    {
        for(int i = 0; i < stageList.Count; i++)
        {
            if (stageList[i].levelsList[0].isAvaliable)
            {
                locks[i].SetActive(false);
                stageButtons[i].interactable = true;
            } else
            {
                locks[i].SetActive(true);
                stageButtons[i].interactable = false;
            }
        }
    }
}
