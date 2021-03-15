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
    private void Start()
    {
        changeExpresion = true;
        characterBattle = GetComponentInParent<CharacterBattle>();
        defaultMouth = mouthRenderer.sprite;
    }
    public void SetExpression()
    {
        if (changeExpresion)
        {
            mouthRenderer.sprite = attackingMouth;
        }
        else mouthRenderer.sprite = defaultMouth;

        changeExpresion = !changeExpresion;
    }

    public void NormalAttackEffect()
    {
        characterBattle.NormalAttackEffect();
    }
    public void Skill1Effect()
    {
        characterBattle.Skill1Effect();
    }

    public void Skill2Effect()
    {
        characterBattle.Skill2Effect();
    }


}
