using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private Slider healthSlider;
    public int health;
    public int maxHealth;
    [SerializeField] private Slider armorSlider;
    public int armor;
    public int maxArmor;

    public int strength;//Attack Power
    public int agility;// 
    public int intelligence;//Heal Power
    public int endurance;
    [SerializeField] private TMP_Text protection;
    public int blockingPower;

    public int minBlockingPower = 1;
    // public static int s_specialSkill;
    [SerializeField] private TMP_Text moneyText;
    public int money;
    public int experience;
    public Animator animator;
    public bool isAlive;
    [SerializeField] private Canvas canvas;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetPlayerAttributes();
    }
    private void SetPlayerAttributes()
    {
        canvas = gameObject.GetComponentInChildren<Canvas>();
       // healthSlider = canvas.transform.Find("HealthSlider").GetComponentInChildren<Slider>();
       // armorSlider = canvas.transform.Find("ArmorSlider").GetComponentInChildren<Slider>();
        animator = GetComponentInChildren<Animator>();        
    }
    private void Start()
    {
        //SaveManager.Instance.LoadPlayerData();
        SetPlayerAttributes();
    }

    public void UpdateHUD()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        armorSlider.maxValue = maxArmor;
        armorSlider.value = armor;
        moneyText.text = $"Money :{money}";
        protection.text = $"Protect :{blockingPower}";
    }

    public virtual float PlayerAttack(Enemy enemy, int damagePowerCard)
    {
        animator.SetTrigger("Attack");
        enemy.EnemyGetDamage(damagePowerCard);
        var animWaitTime = animator.GetCurrentAnimatorStateInfo(0).length;
        return animWaitTime;
    }
    public virtual float PlayerHeal(int healPowerCard)
    {
        animator.SetTrigger("Heal");
        health += healPowerCard;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        UpdateHUD();
        var animWaitTimeEnemy = animator.GetCurrentAnimatorStateInfo(0).length;
        return animWaitTimeEnemy;
    }
    public virtual float PlayerProtection(int protectionPowerCard)
    {
        animator.SetTrigger("Protection");
        blockingPower += blockingPower;
        var animWaitTimeEnemy = animator.GetCurrentAnimatorStateInfo(0).length;
        return animWaitTimeEnemy;
    }
    public virtual float PlayerGetDamage(int damage)
    {
        damage /= blockingPower;
        if (damage <= 0)
        {
            damage = 1;
        }
        if (armor > 0)
        {
            armor -= damage;
            if (armor < 0)
            {
                health -= -armor;
                armor = 0;
            }
        }
        else
        {
            health -= damage;
        }
        UpdateHUD();
        animator.SetTrigger("GetDamage");
        var animWaitTime = animator.GetCurrentAnimatorStateInfo(0).length;
        if (health <= 0)
        {
            Debug.Log($"AnimTime (PlayerDeath) = {animWaitTime}");
            animWaitTime = PlayerDeath();
            return animWaitTime; ;
        }
        else
        {
            Debug.Log($"AnimTime (Player) = {animWaitTime}");
            return animWaitTime;
        }

    }

    public void TakeMoney(int money)
    {
        this.money += money;
        UpdateHUD();
    }

    public virtual float PlayerDeath()
    {
        isAlive = false;
        Debug.Log("Player is dead");
        animator.SetBool("Dead", true);
        animator.SetTrigger("GetDamage");        
        var animWaitTime = animator.GetCurrentAnimatorStateInfo(0).length;
        return animWaitTime;
    }
}
