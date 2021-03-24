using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEquipmenyDetails : MonoBehaviour
{
    private CompareAndDisplayDetails compareAndDisplayDetails;
    public void DisplayDetails()
    {
        compareAndDisplayDetails = GetComponentInChildren<CompareAndDisplayDetails>();


        if(compareAndDisplayDetails != null)
        {
            compareAndDisplayDetails.SetItem(GetComponentInChildren<SetItemIcon>().GetItem);
            compareAndDisplayDetails.EquipmentDetails();
        }
    }

    public void HideDetails()
    {
        if(compareAndDisplayDetails != null)
        {
            compareAndDisplayDetails.HideDetailsPanel();
        }
    }

}
