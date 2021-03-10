using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateToUpdateCharacterEquipment : MonoBehaviour
{
    public delegate void UpdateCharacterEquipmentSprites();
    public UpdateCharacterEquipmentSprites changeSprites;

    private void Start()
    {
        changeSprites += DummyMethod;
    }

    private void DummyMethod() {

    }
}
