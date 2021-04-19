using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectionPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> locks = new List<GameObject>();
    [SerializeField] private List<Button> stageButtons = new List<Button>();
    private void Start()
    {
        if(PlayerPrefs.GetInt("Tutorial1", 1) == -1 && PlayerPrefs.GetInt("Tutorial2", 1) == -1 && PlayerPrefs.GetInt("Tutorial3", 1) == -1
            && PlayerPrefs.GetInt("Tutorial4", 1) == -1 && PlayerPrefs.GetInt("Tutorial5", 1) == -1 && PlayerPrefs.GetInt("Tutorial6", 1) == -1)
        {
            locks[1].SetActive(false);
            stageButtons[1].interactable = true;
        }
    }
}
