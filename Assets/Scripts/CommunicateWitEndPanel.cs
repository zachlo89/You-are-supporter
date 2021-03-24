using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicateWitEndPanel : MonoBehaviour
{
    private EndDetailedPanel endDetailedPanel;
    private ItemScriptable item;

    public void SetUpCommunciationWithEndPanel(EndDetailedPanel endDetailedPanel, ItemScriptable item)
    {
        this.endDetailedPanel = endDetailedPanel;
        this.item = item;
    }
    public void ShowDetails()
    {
        if(endDetailedPanel != null)
        {
            endDetailedPanel.gameObject.SetActive(true);
            endDetailedPanel.UpdateRightPanel(item, gameObject);
        }
    }
}
