using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattle : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private SpriteRenderer image;
    private BattleManager battleManager;
    private ScriptableCharacter hero;
    private bool isAlive = true;

    //Add animator controller from hero

    private Animator animator;
    

    public bool IsAlive
    {
        get { return isAlive; }
    }

    private int maxHP;
    private int currentHealth;
    private int maxMP;
    private int currentMana;
    private int damage;
    private int armor;
    private int attackRate;
    private int manaRegen;
    private int manaCostSkill1 = 50;
    private int manaCostSkill2 = 70;

    private void Start()
    {
        StartCoroutine(Attack());

        animator = GetComponent<Animator>();

        InvokeRepeating("RegenMana", 1, 1);
    }

    private void RegenMana()
    {
        if (isAlive)
        {
            if (currentMana < maxMP)
            {
                currentMana += manaRegen;
                if (currentMana > maxMP)
                {
                    currentMana = maxMP;
                }
                UpdateManaBar();
            }
        } 
    }

    public void SetUpHero(ScriptableCharacter hero, BattleManager battleManager)
    {
        this.hero = hero;
        this.battleManager = battleManager;
        //image.sprite = hero.image;
        maxHP = hero.maxHealt;
        currentHealth = hero.maxHealt;
        currentMana = hero.maxMana;
        damage = hero.damage;
        armor = hero.armor;
        attackRate = hero.attackRate;
        manaRegen = hero.manaRegen;
        maxMP = hero.maxMana;
        if(hero.equipment != null)
        {
            for (int i = 0; i < hero.equipment.GetEquipment.Count; i++)
            {
                if (hero.equipment.GetEquipment[i] != null)
                {
                    currentHealth += hero.equipment.GetEquipment[i].hp;
                    damage += hero.equipment.GetEquipment[i].damage;
                    armor += hero.equipment.GetEquipment[i].armor;
                    attackRate += hero.equipment.GetEquipment[i].attackRate;

                }
            }
        }
        battleManager.PopulateList(gameObject.tag, this);
        UpdateManaBar();
    }

    public void GetDamage(int recievedDamage)
    {
        int attack = recievedDamage - armor;
        attack = attack > 0 ? attack : 0;
        currentHealth -= attack;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(name + " Died");
            isAlive = false;
            animator.SetTrigger("Death");
            CancelInvoke();
        }

        healthBar.fillAmount = (float)currentHealth / maxHP;
    }

    private void NormalAttack()
    {
        CharacterBattle temp = battleManager.GetFrontCharacter(gameObject.tag);
        if (temp != null)
        {
            temp.GetDamage(damage);
            animator.SetTrigger("NormalAttack");
        }

    }

    private void Skill1()
    {
        CharacterBattle temp = battleManager.GetMiddleCharacter(gameObject.tag);
        if(temp != null)
        {
            temp.GetDamage(damage);
            currentMana -= manaCostSkill1;
            animator.SetTrigger("Skill1");
            UpdateManaBar();
        }
    }

    private void Skill2()
    {
        List<CharacterBattle> temp = battleManager.GetAllEniemies(gameObject.tag);
        for(int i = 0; i < temp.Count; i++)
        {
            if(temp[i].isAlive && temp[i] != null)
            {
                temp[i].GetDamage(damage);
                currentMana -= manaCostSkill2;
                animator.SetTrigger("Skill2");
                UpdateManaBar();
            }
        }
    }

    IEnumerator Attack()
    {
        float random = Random.Range(0f, 1f);
        yield return new WaitForSeconds((attackRate / 100f) + random);
        while (isAlive)
        {
            int possibleAttacks = 1;
            if (currentMana > manaCostSkill2)
            {
                possibleAttacks = 3;
            }
            else if (currentMana > manaCostSkill1)
            {
                possibleAttacks = 2;
            }
            int currentAttack = Random.Range(0, possibleAttacks);

            switch (currentAttack)
            {
                case 0:
                    Debug.Log("NormalAttack");
                    NormalAttack();
                    break;
                case 1:
                    Debug.Log("Skill1");
                    Skill1();
                    break;
                case 2:
                    Debug.Log("Skill2");
                    Skill2();
                    break;
                default:
                    Debug.Log("Default");
                    NormalAttack();
                    break;
            }
            float random2 = Random.Range(.2f, 1.5f);
            yield return new WaitForSeconds((attackRate / 100f)+random2);
        }
    }

    private void UpdateManaBar()
    {
        if(manaBar != null)
        {
            manaBar.fillAmount = (float)currentMana / maxMP;
        }
    }
}
