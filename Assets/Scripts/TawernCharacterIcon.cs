using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TawernCharacterIcon : MonoBehaviour
{
    private UpdateSlotCharacterUI updateCharacter;
    private ScriptableCharacter character;
    private TawernPanel tawenPanel;
    [SerializeField] private GameObject selectedIndicator;

    public void SetUpCharacter(ScriptableCharacter character, TawernPanel tawernPanel)
    {
        updateCharacter = GetComponent<UpdateSlotCharacterUI>();
        this.character = character;
        this.tawenPanel = tawernPanel;
        updateCharacter.UpdateSlotCharacter(character);
    }

    public void CharacterClick()
    {
        tawenPanel.CharacterClick(character, transform.GetSiblingIndex(), this);
    }

    public void Deselection()
    {
        selectedIndicator.SetActive(false);
    }
}
