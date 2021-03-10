using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotAddOrLock : MonoBehaviour
{
    [SerializeField] private GameObject iconLock, textLock, addIcon, addText;
    [SerializeField] private Button button;

    public void AddTeamMemeber()
    {
        button.interactable = true;
        iconLock.SetActive(false);
        textLock.SetActive(false);
        addIcon.SetActive(true);
        addText.SetActive(true);
    }

    public void LockedSlot()
    {
        button.interactable = false;
        iconLock.SetActive(true);
        textLock.SetActive(true);
        addIcon.SetActive(false);
        addText.SetActive(false);
    }
}
