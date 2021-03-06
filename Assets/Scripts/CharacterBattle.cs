using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBattle : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    [SerializeField] private GameObject stunnEffects;
    private float timer;
    private List<PlayerScriptableSkill> playerPassiveSkills = new List<PlayerScriptableSkill>();
    private List<CharacterSkill> passiveCharacterSkill = new List<CharacterSkill>();
    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject normalHitsParticlePrefab;
    [SerializeField] private AnimationFunctions animationFunctions;
>>>>>>> Stashed changes
    [SerializeField] private GameObject canvasPanel;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private GameObject damagePopUp;
    private BattleManager battleManager;
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

    private void Start()
    {
<<<<<<< Updated upstream
        StartCoroutine(Attack());
=======
        
    }

    public void StartBattle()
    {
        timer = 0;
        attackRateSlider.value = Random.Range(0f, 0.2f);
        if (!isMainHero)
        {
            StartCoroutine("AttackRateSlider");
        }
        else
        {
            attackRateSlider.gameObject.SetActive(false);
        }
>>>>>>> Stashed changes

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        canvasPanel.SetActive(true);
        InvokeRepeating("RegenMana", 1, 1);
<<<<<<< Updated upstream
=======
        InvokeRepeating("RegenHP", 1, 1);
        Debug.Log("StartBattle");
    }

    public void ResetSlider()
    {
        attackRateSlider.value = 0;
    }
    

    IEnumerator AttackRateSlider()
    {
        while(isAlive && battleManager.CheckIfEnd() && !isMainHero)
        {
            timer += Time.deltaTime;
            var percent = timer / (attackRate / 100);
            attackRateSlider.value = percent;
            yield return new WaitForEndOfFrame();
            if (percent >= 1)
            {
                Attack();
                timer = 0;
            }
        }

    }
    public void SetUpPlayerSkills(PlayerSKills playerSKills)
    {
        this.playerSKills = playerSKills;
        playerSKills.SetUpMainCharacter(this);
>>>>>>> Stashed changes
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
        //AdjustStatsToLevel();
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

    private void AdjustStatsToLevel()
    {
        switch (hero.characterClass)
        {
            case CharacterClass.Tank:
                for(int i = 0; i < level; i++)
                {
                    maxHP += (int)(maxHP * 8 / 100);
                    damage += (int)(damage * 6 / 100);
                }
                break;
            case CharacterClass.Archer:
                maxHP += (int)(maxHP * 6 / 100);
                damage += (int)(damage * 8 / 100);
                break;
            case CharacterClass.Berserker:
                maxHP += (int)(maxHP * 7 / 100);
                damage += (int)(damage * 7 / 100);
                break;
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
        Vector3 temp = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject tempDamage = Instantiate(damagePopUp, temp, Quaternion.identity);
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
<<<<<<< Updated upstream
        while (skill2Cooldown > 0)
=======
        skillsCooldowns[i] = activeSkills[i].coolDown;
        while(skillsCooldowns[i] > 0)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    }
=======

        GameObject temp = Instantiate(damagePopUp, transform.position, Quaternion.identity);
        temp.GetComponentInChildren<TextMeshPro>().outlineColor = Color.green;
        temp.GetComponentInChildren<TextMeshPro>().text = value.ToString();
        temp.GetComponent<Animator>().SetTrigger("Heal");
        Destroy(temp, .5f);
}
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======

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
        while (duration > 0)
        {
            Heal(value);
            yield return new WaitForSeconds(1f);
            duration -= 1f;
        }
        
    }

    public void InreaseMaxMana(int value)
    {
        maxMP += value;
        currentMana = maxMP;
    }

    public void IncreaseBlockChance(int value, float duration)
    {
        StartCoroutine(BlockChanceBuff(value, duration));
    }

    IEnumerator BlockChanceBuff(int value, float duration)
    {
        blockChance += value;
        yield return new WaitForSeconds(duration);
        blockChance -= value;
    }

    public void Stunned(float duration)
    {
        stunned = true;
        StartCoroutine(StunningEffects(duration));
    }

    IEnumerator StunningEffects(float duration)
    {
        while (stunned)
        {
            animationFunctions.SetDizzyExpresion();
            if(stunnEffects != null)
            {
                GameObject temp = Instantiate(stunnEffects, transform);
                Destroy(temp, duration);
            }
            StopCoroutine("AttackRateSlider");
            if (isMainHero)
            {
                Debug.Log("BlockSkillUsage");
                playerSKills.BlockSkills(duration);
                Debug.Log("BlockSkillUsage");
            }
            yield return new WaitForSeconds(duration);
            stunned = false;
            StartCoroutine("AttackRateSlider");
            animationFunctions.SetDizzyExpresion();
        }
       
    }

    public void SetNewCriticalDamage(float value)
    {
        criticalMultiply = value;
    }

    public void SetNewDodgeChance(int value)
    {
        dodgeChance = value;
    }

    public void SetNewCriticalChance(float value)
    {
        criticalChance = value;
    }
    
    public void Blind(float duration)
    {
        StartCoroutine(LoseSight(duration));
    }

    IEnumerator LoseSight(float duration)
    {
        blinded = true;
        while (blinded)
        {
            StopCoroutine(AttackRateSlider());
            yield return new WaitForSeconds(duration);
            blinded = false;
            StartCoroutine(AttackRateSlider());
        }
    }

    public void BloodLust(float effectValue)
    {
        bloodLust = true;
        defaultAttack = damage;
        this.bloodLustEffect = effectValue;
    }

    private void EffectOfBloodLust()
    {
        damage = defaultAttack + (int)(maxHP / currentHealth * bloodLustEffect);
    }

    public void Bleed(float duration)
    {
        StartCoroutine(BleedStatus(duration));
    }

    IEnumerator BleedStatus(float duration)
    {
        bleeding = true;
        while (bleeding)
        {
            this.GetDamage((int)(maxHP * .5 / 100), false);
            yield return new WaitForSeconds(1f);
            duration -= 1;
            if(duration <= 0)
            {
                bleeding = false;
            }
        }
    }

    private void DestroyParticles()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<ParticleSystem>() != null)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

>>>>>>> Stashed changes
}
