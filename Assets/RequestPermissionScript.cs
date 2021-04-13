using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class RequestPermissionScript : MonoBehaviour
{
    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            return;
        }
        else
        {
            // Ask for permission or proceed without the functionality enabled.
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }
}
