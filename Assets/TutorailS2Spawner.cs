using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorailS2Spawner : MonoBehaviour
{
    [SerializeField] private Transform canvas1;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private ScriptableBool isTutorial1;
    private void Start()
    {
        if (isTutorial1.value)
        {
            GameObject temp = Instantiate(tutorialPanel, canvas1);
            temp.GetComponent<TutorialS1>().S2Constructor();
        }
    }
}
