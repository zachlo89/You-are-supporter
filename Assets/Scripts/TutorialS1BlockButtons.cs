using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialS1BlockButtons : MonoBehaviour
{
    [SerializeField] private List<Button> blockButtons1 = new List<Button>();
    [SerializeField] private List<Button> blockButtons2 = new List<Button>();
    [SerializeField] private List<Button> blockButtons3 = new List<Button>();
    public void BlockButtons1()
    {
        foreach(Button button in blockButtons1)
        {
            button.interactable = false;
        }
        blockButtons1[4].gameObject.SetActive(false);
        blockButtons1[5].gameObject.SetActive(false);
        blockButtons1[5].gameObject.SetActive(false);
        blockButtons1[6].gameObject.SetActive(false);
    }
    public void BlockButtons2()
    {
        foreach (Button button in blockButtons2)
        {
            button.interactable = false;
        }
        blockButtons1[4].gameObject.SetActive(false);
        blockButtons1[3].gameObject.SetActive(false);
        blockButtons1[6].gameObject.SetActive(false);
        blockButtons1[5].gameObject.SetActive(false);
    }
    public void BlockButtons3()
    {
        foreach (Button button in blockButtons3)
        {
            button.interactable = false;
        }
        blockButtons1[2].gameObject.SetActive(false);
        blockButtons1[3].gameObject.SetActive(false);
        blockButtons1[4].gameObject.SetActive(false);
        blockButtons1[5].gameObject.SetActive(false);
    }
}
