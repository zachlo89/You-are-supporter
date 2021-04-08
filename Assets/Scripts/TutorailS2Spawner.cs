using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorailS2Spawner : MonoBehaviour
{
    [SerializeField] private Transform canvas1;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject tutorail2Panel;
    [SerializeField] private Transform manabar;
    private GameObject temp;
    private void Start()
    {

        if (PlayerPrefs.GetInt("Tutorial2", -1) == 0)
        {
            temp = Instantiate(tutorialPanel, canvas1);
            temp.GetComponent<TutorialS1>().S2Constructor();
        }
        else if (PlayerPrefs.GetInt("Tutorial5", -1) == 0)
        {
            temp = Instantiate(tutorail2Panel, canvas1);
            temp.GetComponent<TutorialS1>().S5Constructor(manabar);
        }
        else if (PlayerPrefs.GetInt("Tutorial6", -1) == 0)
        {

        }
        else Destroy(gameObject);
    }

    public void Step5()
    {
        temp.GetComponent<TutorialS1>().NextStep5();
    }
}
