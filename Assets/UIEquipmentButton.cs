using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipmentButton : MonoBehaviour
{
    private CompareAndDisplayDetails cadd;
    public void DisplayEquipemnt()
    {
        cadd = GetComponentInChildren<CompareAndDisplayDetails>();
        if(cadd != null)
        {
            GetComponentInChildren<CompareAndDisplayDetails>().DisplayDetails();
        }
    }

    public void HideDetails()
    {
        if(cadd != null)
        {
            GetComponentInChildren<CompareAndDisplayDetails>().HideDetailsPanel();
        }
    }
}
