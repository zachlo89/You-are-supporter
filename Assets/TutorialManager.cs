using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Transform teams;
    [SerializeField] private Transform tavern;
    [SerializeField] private Transform healSkill, rightArrow, buySkill, equipSkill, skillPanel;
    [SerializeField] private TutorialS1BlockButtons t1Buttons;
    [SerializeField] private GameObject tutorailPanel;
    [SerializeField] private Transform canvas1;
    [SerializeField] private Transform stageTransform;
    [SerializeField] private Transform stageTransform2Parent;

    private GameObject panel;


    public void NextStep()
    {
        panel.GetComponent<TutorialS1>().Step1();
    }

    public void NextStep3()
    {
        panel.GetComponent<TutorialS1>().NextStep3();
    }

    public void NextStep4()
    {
        panel.GetComponent<TutorialS1>().NextStep4();
    }

    public void NextStep6()
    {
        panel.GetComponent<TutorialS1>().NextStep6();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Tutorial1", -1) == 0)
        {
            PlayerPrefs.SetInt("Tutorial1", -1);
            panel = Instantiate(tutorailPanel, canvas1);
            panel.GetComponent<TutorialS1>().S1Constructor(stageTransform, stageTransform2Parent);
            t1Buttons.BlockButtons1();
        }
        else if (PlayerPrefs.GetInt("Tutorial3", -1) == 0)
        {
            PlayerPrefs.SetInt("Tutorial3", -1);
            panel = Instantiate(tutorailPanel, canvas1);
            panel.GetComponent<TutorialS1>().S3Constructor();
            t1Buttons.BlockButtons2();
        }
        else if (PlayerPrefs.GetInt("Tutorial4", -1) == 0)
        {
            PlayerPrefs.SetInt("Tutorial5", 0);
            panel = Instantiate(tutorailPanel, canvas1);
            panel.GetComponent<TutorialS1>().S4Constructor(healSkill, rightArrow, buySkill, equipSkill, skillPanel);
            t1Buttons.BlockButtons3();
            PlayerPrefs.SetInt("Tutorial4", -1);
        }
        else if (PlayerPrefs.GetInt("Tutorial6", -1) == 0)
        {
            panel = Instantiate(tutorailPanel, canvas1);
            panel.GetComponent<TutorialS1>().S6Constructor(tavern,teams);
        }
        else Destroy(gameObject);

    }


}
