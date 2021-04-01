using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBattle : MonoBehaviour
{
    [SerializeField] private GameObject canvasPanel;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private GameObject damagePopUp;
    private PlayerSKills playerSKills;
    private BattleManager battleManager;
    public BattleManager BattleManager
    {
        get { return battleManager; }
    }
    private SkillsManager skillsManager;
    private ScriptableCharacter hero;
    public ScriptableCharacter Hero
    {
        get { return hero; }
    }
    private bool isAlive = true;

    //Add animator controller from hero

    private Animator animator;
    public Animator HeroAnimator
    {
        get { return animator; }
    }

    public bool IsAlive
    {
        get { return isAlive; }
    }

    private CharacterClass characterClass;
    public CharacterClass HeroClass
    {
        get
        {
            return characterClass;
        }
    }
    private int dodgeChance;
    private int blockChance;
    private int level;
    public int Level
    {
        get { return level; }
    }
    private float criticalChance;
    public float CriticalChance
    {
        get { return criticalChance; }
    }
    private float criticalMultiply;
    public float CriticalMultiply
    {
        get { return criticalMultiply; }
    }
    private int maxHP;
    private int currentHealth;
    private int hpRegen;
    private int maxMP;
    public int MaxMP
    {
        get { return maxMP; }
    }
    private int currentMana;
    private int damage;
    public int Damage
    {
        get { return damage; }
    }
    private int armor;
    public int Armor
    {
        get { return armor; }
    }
    private int attackRate;
    private int manaRegen;
    private int manaCostSkill1 = 50;
    private int manaCostSkill2 = 70;
    private float skill1Cooldown;
    private float skill2Cooldown;
    private bool isMainHero;

    private void Start()
    {
        StartCoroutine(Attack());

        animator = GetComponent<Animator>();
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        canvasPanel.SetActive(true);
        InvokeRepeating("RegenMana", 1, 1);
        InvokeRepeating("RegenHP", 1, 1);
    }

    public void SetUpPlayerSkills(PlayerSKills playerSKills)
    {
        this.playerSKills = playerSKills;
        playerSKills.SetUpMainCharacter(this);
    }

    public void AddRegeneration(int regeneration)
    {
        hpRegen += regeneration;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    public void SetArmor(int armor)
    {
        this.armor = armor;
    }

    public void SetDamage(int newDamage)
    {
        this.damage = newDamage;
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
            if (isMainHero && playerSKills!= null)
            {
                playerSKills.SetCurrentMana(currentMana);
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

    public void MainHeroSetUpManaBar(Image manabar)
    {
        manaBar.fillAmount = 0;
        manaBar = manabar;
        manaBar.fillAmount = 1;
        manabar.gameObject.SetActive(true);
    }

    public void SetUpHero(ScriptableCharacter hero, BattleManager battleManager, SkillsManager skillsManager)
    {
        this.skillsManager = skillsManager;
        this.hero = hero;
        this.level = hero.level;
        this.maxHP = hero.maxHealt;
        this.maxMP = hero.maxMana;
        this.damage = hero.damage;
        this.armor = hero.armor;
        this.attackRate = hero.attackRate;
        AdjustStatsToEquipment();

        this.battleManager = battleManager;
        this.hpRegen = hero.hpRegen;
        this.criticalChance = hero.critChance;
        this.criticalMultiply = hero.critDamageMultiplay;
        this.skill1Cooldown = 0;
        this.skill2Cooldown = 0;
        this.dodgeChance = hero.dogdeChance;
        this.blockChance = hero.blockChance;
        
        this.manaRegen = hero.manaRegen;
        

        this.currentHealth = hero.maxHealt;
        this.currentMana = hero.maxMana;
        this.isMainHero = hero.isMainCharacter;
        
        
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
        if (isMainHero && playerSKills != null)
        {
            playerSKills.SetCurrentMana(currentMana);
        }
    }

    private void AdjustStatsToEquipment()
    {
        for (int i = 0; i < hero.equipment.GetEquipment.Count; i++)
        {
            if (hero.equipment.GetEquipment[i] != null)
            {
                damage += hero.equipment.GetEquipment[i].damage;
                armor += hero.equipment.GetEquipment[i].armor;
                attackRate += hero.equipment.GetEquipment[i].attackRate;
                maxHP += hero.equipment.GetEquipment[i].hp;
            }
        }
    }

    public void GetDamage(int recievedDamage, bool crit)
    {
        GameObject tempDamage = Instantiate(damagePopUp, transform.position , Quaternion.identity);
        int avoidBlock = Random.Range(0, 100);
        int aboidMiss = Random.Range(0, 100);
        if(aboidMiss <= dodgeChance)
        {
            tempDamage.GetComponentInChildren<TextMeshPro>().text = "Dodged";
            Destroy(tempDamage, 1f);
            return;
        } else if (avoidBlock <=  blockChance)
        {
            tempDamage.GetComponentInChildren<TextMeshPro>().text = "Blocked";
            Destroy(tempDamage, 1f);
            return;
        }
        int attack = recievedDamage - armor;
        attack = attack > 0 ? attack : 0;
        currentHealth -= attack;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            animator.SetTrigger("Death");
            CancelInvoke();
        }

        if (crit)
        {
            tempDamage.GetComponentInChildren<TextMeshPro>().color = Color.red;
        }
        tempDamage.GetComponentInChildren<TextMeshPro>().text = attack.ToString();
        Destroy(tempDamage, 1f);

        UpdateHealthBar();
    }

    private void NormalAttack()
    {
        animator.SetTrigger("NormalAttack");
    }

    public void NormalAttackEffect()
    {
        CharacterBattle enemy = battleManager.GetFrontCharacter(gameObject.tag);
        if(enemy != null)
        {
            if (CritAttack())
            {
                enemy.GetDamage(damage + (int)(damage * criticalMultiply), true);
            }
            else enemy.GetDamage(damage, false);
        }
    }

    private void Skill1()
    {
        animator.SetTrigger("Skill1");
    }

    public void Skill1Effect()
    {
        skill1Cooldown = skillsManager.Skill1(this);
        currentMana -= manaCostSkill1;
        UpdateManaBar();
        StartCoroutine(Skill1Avaliable());
    }

    IEnumerator Skill1Avaliable()
    {
        while(skill1Cooldown > 0)
        {
            skill1Cooldown -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void Skill2()
    {
        animator.SetTrigger("Skill2");
    }

    public void Skill2Effect()
    {
        currentMana -= manaCostSkill1;
        UpdateManaBar();
        StartCoroutine(Skill2Avaliable());
    }

    IEnumerator Skill2Avaliable()
    {
        while (skill2Cooldown > 0)
        {
            skill2Cooldown -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator Attack()
    {
        float random = Random.Range(0f, 1f);
        yield return new WaitForSeconds((100/ attackRate) + random);
        while (isAlive && battleManager.CheckIfEnd())
        {
            int possibleAttacks = 1;
            if (currentMana > manaCostSkill2 && skill2Cooldown <= 0)
            {
                possibleAttacks = 3;
            }
            else if (currentMana > manaCostSkill1 && skill1Cooldown <= 0)
            {
                possibleAttacks = 2;
            }
            int currentAttack = Random.Range(0, possibleAttacks);
            if (isMainHero)
            {
                currentAttack = 0;
            }
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
            yield return new WaitForSeconds((100f / attackRate));
        }
    }

    public void MainPlayerUseMana(int mana)
    {
        currentMana -= mana;
        UpdateManaBar();
        playerSKills.SetCurrentMana(currentMana);
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

    public void Heal(int value)
    {
        if(currentHealth > 0)
        {
            currentHealth += value;
        }

        if(currentHealth > maxHP)
        {
            currentHealth = maxHP;
        }

        UpdateHealthBar();
    }

    public void Defence(int value, float duration)
    {
        StartCoroutine(DefenceBuff(value, duration));
    }

    IEnumerator DefenceBuff(int value, float duration)
    {
        armor += value;
        yield return new WaitForSeconds(duration);
        armor -= value;
    }

    public void Haste(int value, float duration)
    {
        StartCoroutine(HasteBuff(value, duration));
    }

    IEnumerator HasteBuff(int value, float duration)
    {
        attackRate += value;
        yield return new WaitForSeconds(duration);
        attackRate -= value;
    }

    public void AttackBuff(int value, float duration)
    {
        StartCoroutine(AttackIncrease(value, duration));
    }

    IEnumerator AttackIncrease(int value, float duration)
    {
        damage += value;
        yield return new WaitForSeconds(duration);
        damage -= value;
    }

    public void Dispel()
    {
        //TO DO remove all negative buffs
    }

    public void ManaRegen(int mana)
    {
        manaRegen += mana;
    }

    public void HpRegenBuff(int value, float duration)
    {
        StartCoroutine(RegenHPBuff(value, duration));
    }

    IEnumerator RegenHPBuff(int value, float duration)
    {
        hpRegen += value;
        yield return new WaitForSeconds(duration);
        hpRegen -= value;
    }

    public void InreaseMaxMana(int value)
    {
        maxMP += value;
    }

}
