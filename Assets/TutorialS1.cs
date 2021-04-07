using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialS1 : MonoBehaviour
{
    private Transform healSkill;
    private Transform rightArrow;
    private Transform buySkill;
    private Transform equipSkill;
    private Transform skillPanel;
    [SerializeField] private Button button;
    private bool partOne;
    private bool partTwo;
    private bool partThree;
    private bool partFour;
    private bool partFive;
    [SerializeField] private GameObject tutorailImage;
    [SerializeField] private GameObject tutrailText;
    private string chat1 = "Let's jump straight into the battle, the most fun!";
    private string chat2 = "Now choose stage";
    private string chat3 = "Finnally level";
    private string chat4 = "Uuuu you have tough opponent in front of you, slime can be dangerous enemy...\n <color=red> Redbar at the top of character show current hp\n <color=yellow> Filling yellow bar indicate when character can attack</color>";
    private string chat5 = "To attack your enemy simply wait until yellow bar fill to maximum, then you can perform normal attacks";
    private string chat6 = "Great you recived staff from loot, equip it";
    private string chat7 = "Now click on hero\n" +
        "On right side of equipment panel you have all items in your inventory.\n Left side show you character and all stats.\n Click on item and equip it";
    private string chat8 = "Great job lets try another battle";
    private string chat9 = "Awesome, you level up, check out skill panel and learn new spell";
    private string chat10 = "On right side you have all skill possible for you character class";
    private string chat11 = "Small arrows allows you to rotate betwen future character. You'll find same in inventory panel, but doesn't do to much right now.";
    private string chat12 = "Click on skill";
    private string chat13 = "You can buy it now";
    private string chat14 = "Equip it into one of 4 avaliable slots";
    private string chat15 = "Time for another battle! If you want you can equip some more items";
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject hand;
    private Transform stageTransform;
    private Transform stageTransform2Parent;
    int counter;
    public void S1Constructor(Transform stageTransform, Transform stageTransform2Parent)
    {
        counter = 0;
        partOne = true;
        partTwo = false;
        partThree = false;
        partFour = false;
        partFive = false;
        this.stageTransform = stageTransform;
        this.stageTransform2Parent = stageTransform2Parent;
        Step1();
    }
    public void Step1()
    {
        if (!partTwo && partOne && !partThree && !partFour && !partFive)
        {
            switch (counter)
            {
                case 0:
                    hand.SetActive(true);
                    ChangeText(chat1);
                    break;
                case 1:
                    button.gameObject.SetActive(false);
                    hand.transform.position = stageTransform.transform.position;
                    hand.transform.SetParent(stageTransform);
                    hand.GetComponent<RectTransform>().pivot = new Vector2(1f, -0.5f);
                    ChangeText(chat2);
                    break;
                case 2:
                    hand.transform.position = stageTransform2Parent.GetChild(0).transform.position;
                    hand.transform.SetParent(stageTransform2Parent.GetChild(0));
                    ChangeText(chat3);
                    break;
            }
            ++counter;
        }
        
    }

    private void ChangeText(string text)
    {
        tutorialText.text = text;
    }

    public void S2Constructor()
    {
        counter = 0;
        partOne = false;
        partTwo = true;
        partThree = false;
        partFour = false;
        partFive = false;
        Step2();
    }
    public void Step2()
    {
        if (partTwo && !partOne && !partThree && !partFour && !partFive)
        {
            switch (counter)
            {
                case 0:
                    Time.timeScale = 0;
                    ChangeText(chat4);
                    break;
                case 1:
                    ChangeText(chat5);
                    break;
                case 2:
                    Time.timeScale = 1;
                    partTwo = false;
                    this.gameObject.SetActive(false);
                    break;
            }
            ++counter;
        }
    }

    public void S3Constructor()
    {
        partThree = true;
        partTwo = false;
        partOne = false;
        partFour = false;
        partFive = false;
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        tutrailText.transform.rotation = Quaternion.Euler(0, 0, 0);
        counter = 0;
        NextStep3();
        hand.SetActive(true);
    }

    public void NextStep3()
    {
        if (!partTwo && !partOne && partThree && !partFour && !partFive)
        {
            switch (counter)
            {
                case 0:
                    ChangeText(chat6);
                    break;
                case 1:
                    Destroy(hand.gameObject);
                    ChangeText(chat7);
                    break;
                case 2:
                    ChangeText(chat8);
                    StartCoroutine(Hide());
                    break;
            }
            ++counter;

        }
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(2f);
        partThree = false;
        this.gameObject.SetActive(false);
    }

    public void S4Constructor(Transform healSkill, Transform rightArrow, Transform buySkill, Transform equipSkill, Transform skillPanel)
    {
        counter = 0;
        this.skillPanel = skillPanel;
        this.healSkill = healSkill;
        this.rightArrow = rightArrow;
        this.buySkill = buySkill;
        this.equipSkill = equipSkill;
        partThree = false;
        partTwo = false;
        partOne = false;
        partFour = true;
        partFive = false;
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        tutrailText.transform.rotation = Quaternion.Euler(0, 0, 0);
        NextStep4();
    }
    
    public void NextStep4()
    {
        if (!partTwo && !partOne && !partThree && partFour && !partFive)
        {
            switch (counter)
            {
                case 0:
                    hand.SetActive(true);
                    hand.transform.position = skillPanel.position;
                    hand.transform.SetParent(skillPanel);

                    hand.GetComponent<RectTransform>().pivot = new Vector2(1f, 0f);
                    ChangeText(chat9);
                    break;
                case 1:
                    ChangeText(chat10);
                    break;
                case 2:
                    ChangeText(chat11);
                    hand.SetActive(true);
                    hand.transform.position = rightArrow.transform.position;
                    hand.transform.SetParent(rightArrow);
                    break;
                case 3:
                    ChangeText(chat12);
                    hand.transform.position = healSkill.transform.GetChild(0).transform.position;
                    hand.transform.SetParent(healSkill.transform.GetChild(0));
                    break;
                case 4:
                    ChangeText(chat13);
                    hand.transform.position = buySkill.transform.position;
                    hand.transform.SetParent(buySkill);
                    break;
                case 5:
                    ChangeText(chat14);
                    hand.transform.position = equipSkill.transform.position;
                    hand.transform.SetParent(equipSkill);
                    break;
                case 6:
                    ChangeText(chat15);
                    Destroy(hand.gameObject);
                    StartCoroutine(Hide());
                    break;
            }
            ++counter;

        }
    }
}
