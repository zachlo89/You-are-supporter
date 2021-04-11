using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctions : MonoBehaviour
{
    private CharacterBattle characterBattle;
    private Sprite defaultMouth;
    [SerializeField] private Sprite attackingMouth;
    [SerializeField] private SpriteRenderer mouthRenderer;
    private bool changeExpresion;
    private int skillCount;
    private void Start()
    {
        characterBattle = GetComponentInParent<CharacterBattle>();
        changeExpresion = true;
        if (defaultMouth == null && mouthRenderer != null)
        {
            defaultMouth = mouthRenderer.sprite;
        }
    }

    public void SetSkill(int skill)
    {
        this.skillCount = skill;
    }
    public void SetExpression()
    {
        if(mouthRenderer != null)
        {
            if (defaultMouth == null)
            {
                defaultMouth = mouthRenderer.sprite;
            }
            if (changeExpresion)
            {
                mouthRenderer.sprite = attackingMouth;
            }
            else mouthRenderer.sprite = defaultMouth;

            changeExpresion = !changeExpresion;
        }
    }

    public void NormalAttackEffect()
    {
        characterBattle.NormalAttackEffect();
    }
    public void UseSkill()
    {
        characterBattle.UseSkill(skillCount);
    }

}
