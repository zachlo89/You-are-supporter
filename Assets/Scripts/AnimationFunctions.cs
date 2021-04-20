using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctions : MonoBehaviour
{
    private CharacterBattle characterBattle;
    private Sprite defaultMouth;
    [SerializeField] private Sprite attackingMouth;
    [SerializeField] private SpriteRenderer mouthRenderer;
    [SerializeField] private SpriteRenderer eyesRenderer;
    [SerializeField] private Sprite dizzyEyes;
    private Sprite defaultEyes;
    private bool changeEyesExpresion;
    private bool changeExpresion;
    private void Start()
    {
<<<<<<< Updated upstream
        changeExpresion = true;
        characterBattle = GetComponentInParent<CharacterBattle>();
        defaultMouth = mouthRenderer.sprite;
=======
        changeEyesExpresion = true;
        battleManager = GameObject.FindObjectOfType<BattleManager>();
        characterBattle = GetComponentInParent<CharacterBattle>();
        changeExpresion = true;
        if (defaultMouth == null && mouthRenderer != null)
        {
            defaultMouth = mouthRenderer.sprite;
        }
        if(defaultEyes == null && eyesRenderer != null)
        {
            defaultEyes = eyesRenderer.sprite;
        }
    }

    public void SetSkill(int skill)
    {
        this.skillCount = skill;
>>>>>>> Stashed changes
    }
    public void SetExpression()
    {
        if (changeExpresion)
        {
<<<<<<< Updated upstream
            mouthRenderer.sprite = attackingMouth;
=======
            if (defaultMouth == null)
            {
                defaultMouth = mouthRenderer.sprite;
            }
            if(defaultMouth != null)
            {
                if (changeExpresion)
                {
                    mouthRenderer.sprite = attackingMouth;
                }
                else mouthRenderer.sprite = defaultMouth;
            }
            changeExpresion = !changeExpresion;
>>>>>>> Stashed changes
        }
        else mouthRenderer.sprite = defaultMouth;

        changeExpresion = !changeExpresion;
    }

    public void SetDizzyExpresion()
    {
        if (eyesRenderer != null)
        {
            if (defaultEyes == null)
            {
                defaultEyes = eyesRenderer.sprite;
            }
            if (defaultEyes != null)
            {
                if (changeEyesExpresion)
                {
                    eyesRenderer.sprite = dizzyEyes;
                }
                else eyesRenderer.sprite = defaultEyes;
            }
            changeEyesExpresion = !changeEyesExpresion;
        }
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

<<<<<<< Updated upstream
=======
    public void MagicMIssleConstructor(GameObject magicMIssle)
    {
        this.missleObject = magicMIssle;
    }
    
    public void StartBattle()
    {
        Debug.Log("AnimationFunction Start battle");
        characterBattle.StartBattle();
    }
>>>>>>> Stashed changes

}
