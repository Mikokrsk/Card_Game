using System.Collections;
using System.Collections.Generic;
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
    public int blockingPower;

    public int minBlockingPower=1;
    // public static int s_specialSkill;
    [SerializeField] private Text moneyText;
    public int money;
    public int experience;

    public bool isAlive;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
   /* private void OnEnable()
    {
        SaveManager.Instance.LoadPlayerData();
    }*/
    private void Start()
    {
        SaveManager.Instance.LoadPlayerData();
    }
    public void UpdateHUD()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        armorSlider.maxValue = maxArmor;
        armorSlider.value = armor;
        moneyText.text = $"Money :{money}";
    }
/*
    public void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            armor -= damage / blockingPower;
            if (armor < 0)
            {
                TakeDamageHealth(-armor);
            }
        }
        else
        {
            TakeDamageHealth(damage);
        }
        UpdateHUD();
    }

    private void TakeDamageHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
        UpdateHUD();
    }*/

    public void TakeMoney(int money)
    {
        this.money += money;
        UpdateHUD();
    }

    public void Death()
    {
        isAlive = false;
    }
}
