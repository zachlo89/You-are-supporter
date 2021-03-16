using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsEffect : MonoBehaviour
{
    private PlayerSKills playerSKills;
    private CharacterBattle character;

    private void Start()
    {
        playerSKills = GameObject.FindObjectOfType<PlayerSKills>();
        character = GetComponent<CharacterBattle>();
    }

    private void OnMouseDown()
    {
        playerSKills.UseSkill(character);
    }

}
