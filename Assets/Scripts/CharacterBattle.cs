using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattle : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    private BattleManager battleManager;
    private ScriptableCharacter hero;
    private bool isAlive = true;

    //Add animator controller from hero

    private Animator animator;
    

    public bool IsAlive
    {
        get { return isAlive; }
    }

    private float criticalChance;
    private float criticalMultiply;
    private int maxHP;
    private int currentHealth;
    private int hpRegen;
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
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        InvokeRepeating("RegenMana", 1, 1);
    }

    private void OnEnable()
    {
        healthBar.enabled = true;
        if(manaBar != null)
        {
            manaBar.enabled = true;
        }
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

    private void RegenHP()
    {
        if (isAlive)
        {
            if(currentHealth < maxHP)
            {
                currentHealth += hpRegen;
                if(currentHealth> maxHP)
                {
                    currentHealth = maxHP;
                }
            }
            UpdateHealthBar();
        }
    }

    public void SetUpHero(ScriptableCharacter hero, BattleManager battleManager)
    {
        this.hero = hero;
        this.battleManager = battleManager;
        this.hpRegen = hero.hpRegen;
        this.criticalChance = hero.critChance;
        this.criticalMultiply = hero.critDamageMultiplay;
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

        UpdateHealthBar();
    }

    private void NormalAttack()
    {
        CharacterBattle temp = battleManager.GetFrontCharacter(gameObject.tag);
        if (temp != null)
        {
            if (CritAttack())
            {
                temp.GetDamage((int)(damage * criticalMultiply));
            } else temp.GetDamage(damage);

            animator.SetTrigger("NormalAttack");
        }

    }

    private void Skill1()
    {
        CharacterBattle temp = battleManager.GetMiddleCharacter(gameObject.tag);
        if(temp != null)
        {
            if (CritAttack())
            {
                temp.GetDamage((int)(damage * criticalMultiply));
            }
            else temp.GetDamage(damage);
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
                if (CritAttack())
                {
                    temp[i].GetDamage((int)(damage * criticalMultiply));
                }
                else temp[i].GetDamage(damage);
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
                    NormalAttack();
                    break;
                case 1:
                    Skill1();
                    break;
                case 2:
                    Skill2();
                    break;
                default:
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

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHP;
    }

    private bool CritAttack()
    {
        int random = Random.Range(0, 100);
        if (random <= criticalChance)
        {
            return true;
        }
        else return false;
    }
}
