using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctions : MonoBehaviour
{
    private BattleManager battleManager;
    private CharacterBattle characterBattle;
    private Sprite defaultMouth;
    [SerializeField] private Sprite attackingMouth;
    [SerializeField] private SpriteRenderer mouthRenderer;
    private Sprite defaultEyes;
    [SerializeField] private Sprite dizzyEyes;
    [SerializeField] private SpriteRenderer eyesRenderer;
    private bool changeDizzyExpresion;
    private bool changeExpresion;
    private int skillCount;

    [SerializeField] private GameObject missleObject;
    [SerializeField] private Transform spawningPoint;
    private void Start()
    {
        battleManager = GameObject.FindObjectOfType<BattleManager>();
        characterBattle = GetComponentInParent<CharacterBattle>();
        changeExpresion = true;
        changeDizzyExpresion = true;
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
            if(mouthRenderer != null)
            {
                if (changeExpresion)
                {
                    mouthRenderer.sprite = attackingMouth;
                }
                else mouthRenderer.sprite = defaultMouth;
            }
            changeExpresion = !changeExpresion;
        }
    }

    public void SetDizzyExpression()
    {
        if (eyesRenderer != null)
        {
            if (defaultEyes == null)
            {
                defaultEyes = eyesRenderer.sprite;
            }
            if (eyesRenderer != null)
            {
                if (changeDizzyExpresion)
                {
                    eyesRenderer.sprite = dizzyEyes;
                }
                else eyesRenderer.sprite = defaultEyes;
            }
            changeDizzyExpresion = !changeDizzyExpresion;
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

    public void SpawnMagicMissle()
    {
        CharacterBattle enemy = battleManager.GetFrontCharacter(transform.parent.gameObject.tag);
        if (enemy != null && missleObject != null)
        {
            GameObject temp = Instantiate(missleObject, spawningPoint.transform.position, Quaternion.identity);
            temp.GetComponent<MissleMovement>().SetTarget(enemy.transform, characterBattle);
        }
    }

    public void MagicMIssleConstructor(GameObject magicMIssle)
    {
        this.missleObject = magicMIssle;
    }
    
    public void StartBattle()
    {
        characterBattle.StartBattle();
    }

}
