using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
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

    public int minbBlockingPower=1;
    // public static int s_specialSkill;
    [SerializeField] private Text moneyText;
    public int money;
    public int experience;

    public bool isAlife;

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

    public void UpdateHUD()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        armorSlider.maxValue = maxArmor;
        armorSlider.value = armor;
        moneyText.text = $"Money :{money}";
    }

    public void TakeDamageArmor(int damage)
    {
        if (armor > 0)
        {
            armor -= damage / blockingPower;
            if (armor < 0)
            {
                TakeDamageHealth(armor);
            }
        }
        else
        {
            TakeDamageHealth(damage);
        }
        UpdateHUD();
    }

    public void TakeDamageHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
        UpdateHUD();
    }

    public void TakeMoney(int money)
    {
        this.money += money;
        UpdateHUD();
    }

    public void Death()
    {
        isAlife = false;
    }
}
