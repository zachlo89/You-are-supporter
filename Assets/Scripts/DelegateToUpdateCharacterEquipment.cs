using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateToUpdateCharacterEquipment: MonoBehaviour
{
    public delegate void UpdateCharacterEquipmentSprites();
    public UpdateCharacterEquipmentSprites changeSprites;
    public delegate void UpdateInventory();
    public UpdateInventory updateCount;
    public delegate void CharacterGainedLevel();
    public CharacterGainedLevel levelUppp;
    
    public void Awake()
    {
        changeSprites += DummyMethod;
        updateCount += UpdateCountDummy;
        levelUppp += DummyLevelUp;
    }

    private void DummyMethod() {
    }

    private void UpdateCountDummy()
    {
    }

    private void DummyLevelUp()
    {
    }
}
