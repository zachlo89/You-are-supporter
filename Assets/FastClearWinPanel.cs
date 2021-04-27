using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastClearWinPanel : MonoBehaviour
{
    [SerializeField] private FastClearLootDrooper looter;
    [SerializeField] private Transform spawningPoint;
    private void OnEnable()
    {
        looter.SpawnItems();
    }

    private void OnDisable()
    {
        for(int i = 1; i < spawningPoint.childCount; i++)
        {
            Destroy(spawningPoint.GetChild(i));
        }
    }
}
