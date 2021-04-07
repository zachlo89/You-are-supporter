using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBattle : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject normalHitsParticlePrefab;
    [SerializeField] private AnimationFunctions animationFunctions;
    [SerializeField] private GameObject canvasPanel;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider manaBar;
    [SerializeField] private Slider attackRateSlider;
    [SerializeField] private GameObject damagePopUp;
    private PlayerSKills playerSKills;
    private List<CharacterSkill> activeSkills;
    private BattleManager battleManager;
    public BattleManager BattleManager
    {
        get { return battleManager; }
    }
   // private SkillsManager skillsManager;
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
    public int DodgeChance
    {
        get { return dodgeChance; }
    }
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
    public int AttackRate
    {
        get { return attackRate; }
    }
    private int manaRegen;
    private List<float> skillsCooldowns = new List<float>();
    private bool isMainHero;

    //List of varaible to possible use with diespealing negative effects
    private bool stunned = false;
    private bool blinded = false;
    private bool bleeding = false;


    //List of buffs useful with animation
    private bool isBlockIncrease = false;
    
    private bool bloodLust = false;
    private float bloodLustEffect = 0;

    private int defaultAttack = 0;

    private void Start()
    {
        attackRateSlider.value = Random.Range(0f, 0.2f);
        StartCoroutine(AttackRateSlider());

        animator = GetComponent<Animator>();
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        canvasPanel.SetActive(true);
        InvokeRepeating("RegenMana", 1, 1);
        InvokeRepeating("RegenHP", 1, 1);
    }

    public void ResetSlider()
    {
        attackRateSlider.value = 0;
    }
    

    IEnumerator AttackRateSlider()
    {
        while(isAlive && battleManager.CheckIfEnd())
        {
            yield return new WaitForSeconds((100f / attackRate)/100f);
            attackRateSlider.value += 0.01f;
            if(attackRateSlider.value == 1)
            {
                Attack();
            }
        }

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

    public void SetHP(int value)
    {
        this.maxHP = value;
        this.currentHealth = value;
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
        if (isAlive && maxMP > 0)
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

    public void MainHeroSetUpManaBar(Slider manabar)
    {
        manaBar.gameObject.SetActive(false);
        manaBar = manabar;
        manaBar.gameObject.SetActive(true);
        manaBar.value = 1;
    }

    public void SetUpHero(ScriptableCharacter hero, BattleManager battleManager, SkillsManager skillsManager)
    {
       // this.skillsManager = skillsManager;
        this.hero = hero;
        this.level = hero.level;
        this.maxHP = hero.maxHealt;
        this.maxMP = hero.maxMana;
        this.damage = hero.damage;
        this.armor = hero.armor;
        this.attackRate = hero.attackRate;
        if(hero.equipment != null)
        {
            AdjustStatsToEquipment();
        }


        this.battleManager = battleManager;
        this.hpRegen = hero.hpRegen;
        this.criticalChance = hero.critChance;
        this.criticalMultiply = hero.critDamageMultiplay;
        this.dodgeChance = hero.dogdeChance;
        this.blockChance = hero.blockChance;
        this.characterClass = hero.characterClass;
        this.manaRegen = hero.manaRegen;
        

        this.currentHealth = hero.maxHealt;
        this.currentMana = hero.maxMana;
        this.isMainHero = hero.isMainCharacter;
        

        if(maxMP <= 0)
        {
            manaBar.gameObject.SetActive(false);
        }
        
        
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
        if (!hero.isMainCharacter)
        {
            activeSkills = hero.characterActiveSkills;
            activeSkills.Sort(delegate (CharacterSkill x, CharacterSkill y)
            {
                return string.Compare(x.manaCost.ToString(), y.manaCost.ToString());
            });

            for(int i = 0; i < activeSkills.Count; i++)
            {
                activeSkills[i].Initialize(hero);
                activeSkills[i].SetUpHero(this);
                activeSkills[i].SetUpBattleManager(battleManager);
                skillsCooldowns.Add(0f);
            }
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
            Destroy(tempDamage, .5f);
            return;
        } else if (avoidBlock <=  blockChance)
        {
            tempDamage.GetComponentInChildren<TextMeshPro>().text = "Blocked";
            Destroy(tempDamage, .5f);
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
            if (!isMainHero)
            {
                manaBar.gameObject.SetActive(false);
            }
            healthBar.gameObject.SetActive(false);
            attackRateSlider.gameObject.SetActive(false);
            if(shadow != null)
            {
                shadow.SetActive(false);
            }
            
            CancelInvoke();
        }

        if (crit)
        {
            tempDamage.GetComponentInChildren<TextMeshPro>().outlineColor = Color.red;
        }
        tempDamage.GetComponentInChildren<TextMeshPro>().text = (-attack).ToString();
        Destroy(tempDamage, .5f);

        UpdateHealthBar();
    }

    public void NormalAttack()
    {
        animator.SetTrigger("NormalAttack");
    }

    public void NormalAttackEffect()
    {
        if (!isMainHero)
        {
            CharacterBattle enemy = battleManager.GetFrontCharacter(gameObject.tag);
            if (enemy != null)
            {
                if (CritAttack())
                {
                    enemy.GetDamage(damage + (int)(damage * criticalMultiply), true);
                    Instantiate(normalHitsParticlePrefab, enemy.transform);
                }
                else
                {
                    enemy.GetDamage(damage, false);
                    Instantiate(normalHitsParticlePrefab, enemy.transform);
                }
            }
        }
    }

    private void UseSkillAnimation(int i)
    {
        animationFunctions.SetSkill(i);
        animator.SetTrigger(activeSkills[i].skillName);
    }

    public void UseSkill(int skillCount)
    {
        if(battleManager != null && activeSkills[skillCount] != null)
        {
            activeSkills[skillCount].Use();
            currentMana -= activeSkills[0].manaCost;
            UpdateManaBar();
            StartCoroutine(SkillCooldown(skillCount));
        }
    }

    IEnumerator SkillCooldown(int i)
    {
        skillsCooldowns[i] = activeSkills[0].coolDown;
        while(skillsCooldowns[i] > 0)
        {
            skillsCooldowns[i] -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private void Attack()
    {
        if(!hero.isMainCharacter)
        {
            attackRateSlider.value = 0;
            int possibleAttacks = 0;
            try
            {
                if (!isMainHero && hero.equipment.GetEquipment[2] != null)
                {
                    possibleAttacks += activeSkills.Count;
                }
            }
            catch
            {
                Debug.Log("Missin equipment");
            }
            
            possibleAttacks *= 3;
            int currentAttack = Random.Range(0, possibleAttacks);
            switch (currentAttack)
            {
                case 0:
                    NormalAttack();
                    break;
                case 1:
                    if (activeSkills[0].manaCost <= currentMana && skillsCooldowns[0] <= 0)
                    {
                        UseSkillAnimation(0);
                    }
                    else NormalAttack();
                    break;
                case 4:
                    if (activeSkills[1].manaCost <= currentMana && skillsCooldowns[1] <= 0)
                    {
                        UseSkillAnimation(1);
                    }
                    else NormalAttack();
                    break;
                case 7:
                    if (activeSkills[2].manaCost <= currentMana && skillsCooldowns[2] <= 0)
                    {
                        UseSkillAnimation(2);
                    }
                    else NormalAttack();
                    break;
                case 10:
                    if (activeSkills[3].manaCost <= currentMana && skillsCooldowns[3] <= 0)
                    {
                        UseSkillAnimation(3);
                    }
                    else NormalAttack();
                    break;
                default:
                    NormalAttack();
                    break;
            }
        } else
        {
            playerSKills.CanAttack(true);
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
            manaBar.value = (float)currentMana / maxMP;
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.value = (float)currentHealth / maxHP;
        if (bloodLust)
        {
            EffectOfBloodLust();
        }
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

        GameObject temp = Instantiate(damagePopUp, transform.position, Quaternion.identity);
        temp.GetComponentInChildren<TextMeshPro>().outlineColor = Color.green;
        temp.GetComponentInChildren<TextMeshPro>().text = value.ToString();

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
        Debug.Log("Start coroutine");
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
        StartCoroutine(StunningEffects(duration));
    }

    IEnumerator StunningEffects(float duration)
    {
        stunned = true;
        while (stunned)
        {
            StopCoroutine(AttackRateSlider());
            yield return new WaitForSeconds(duration);
            stunned = false;
            StartCoroutine(AttackRateSlider());
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

}
