using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum PlayerSkill
{
    Skill1,
    Skill2,
    Skill3,
    Skill4
}

public class PlayerSKills : MonoBehaviour
{
    [SerializeField] private Team team;
    private PlayerSkill selectedSkill;
    [SerializeField] private GameObject skill1Button, skill2Button, skill3Button, skill4Button;
    [SerializeField] private int healAmount;
    [SerializeField] private float buffDuration;
    [SerializeField] private int defenceIncrease;
    [SerializeField] private int hasteValue;

    public void SelectSkill1()
    {
        selectedSkill = PlayerSkill.Skill1;
    }
    public void SelectSkill2()
    {
        selectedSkill = PlayerSkill.Skill2;
    }
    public void SelectSkill3()
    {
        selectedSkill = PlayerSkill.Skill3;
    }
    public void SelectSkill4()
    {
        selectedSkill = PlayerSkill.Skill4;
    }

    public void UseSkill(CharacterBattle character)
    {
        switch (selectedSkill)
        {
            case PlayerSkill.Skill1:
                character.Heal(healAmount);
                break;
            case PlayerSkill.Skill2:
                character.Defence(defenceIncrease, buffDuration);
                break;
            case PlayerSkill.Skill3:
                character.Haste(hasteValue, buffDuration);
                break;
            default:
                return;
        }
    }


    
}
