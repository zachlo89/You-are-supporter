using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectionPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> locks = new List<GameObject>();
    [SerializeField] private List<Button> stageButtons = new List<Button>();
    [SerializeField] private List<Stage> stagesList = new List<Stage>();
    private void Start()
    {
        for(int i = 0; i < stagesList.Count; i++)
        {
            if (stagesList[i].levelsList[0].isAvaliable)
            {
                stageButtons[i].interactable = true;
                locks[i].SetActive(false);
            } else
            {
                stageButtons[i].interactable = false;
                locks[i].SetActive(true);
            }
        }
    }
}
