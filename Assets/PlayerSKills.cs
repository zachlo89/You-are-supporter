using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerSkill
{
    Heal,
    Defence,
    Haste
}

public class PlayerSKills : MonoBehaviour
{
    private PlayerSkill selectedSkill;
    private Camera cam;
    [SerializeField] private int healAmount;
    [SerializeField] private float buffDuration;
    [SerializeField] private int defenceIncrease;
    [SerializeField] private int hasteValue;

    private void Start()
    {
        cam = Camera.main;
    }
    public void SelectHeal()
    {
        selectedSkill = PlayerSkill.Heal;
    }
    public void SelectDefence()
    {
        selectedSkill = PlayerSkill.Defence;
    }
    public void SelectHaste()
    {
        selectedSkill = PlayerSkill.Haste;
    }

    public void ChangeEffect(int i)
    {
        switch (i)
        {
            case 0:
                selectedSkill = PlayerSkill.Heal;
                break;
            case 1:
                selectedSkill = PlayerSkill.Defence;
                break;
            case 2:
                selectedSkill = PlayerSkill.Haste;
                break;
            default:
                break;
        }
    }

    public void UseSkill(CharacterBattle character)
    {
        switch (selectedSkill)
        {
            case PlayerSkill.Heal:
                character.Heal(healAmount);
                break;
            case PlayerSkill.Defence:
                character.Defence(defenceIncrease, buffDuration);
                break;
            case PlayerSkill.Haste:
                character.Haste(hasteValue, buffDuration);
                break;
            default:
                return;
        }
    }
}
