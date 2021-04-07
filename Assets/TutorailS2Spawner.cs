using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorailS2Spawner : MonoBehaviour
{
    [SerializeField] private Transform canvas1;
    [SerializeField] private GameObject tutorialPanel;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Tutorial2", -1) == 0)
        {
            GameObject temp = Instantiate(tutorialPanel, canvas1);
            temp.GetComponent<TutorialS1>().S2Constructor();
        }
    }
}
