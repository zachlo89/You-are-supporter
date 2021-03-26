using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TawernCharacterIcon : MonoBehaviour
{
    private UpdateSlotCharacterUI updateCharacter;
    private ScriptableCharacter character;
    private TawernPanel tawenPanel;


    public void SetUpCharacter(ScriptableCharacter character, TawernPanel tawernPanel)
    {
        updateCharacter = GetComponent<UpdateSlotCharacterUI>();
        this.character = character;
        this.tawenPanel = tawernPanel;
        updateCharacter.UpdateSlotCharacter(character);
    }

    public void CharacterClick()
    {
        tawenPanel.CharacterClick(character, transform.GetSiblingIndex());
    }

    public void OnDeselection()
    {
        StartCoroutine(Deselection());
    }

    IEnumerator Deselection()
    {
        yield return new WaitForSeconds(.1f);
        tawenPanel.HideDetailsPanel();
    }
}
