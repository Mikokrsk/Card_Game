using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public int health;
    public int maxHealth;

    public int armor;
    public int maxArmor;

    public int strength;
    public int agility;
    public int intelligence;
    public int endurance;
   // public static int s_specialSkill;

    public int money;  
    public int experience;   
    public int blockingPower;    
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    }

    private void TakeDamageHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isAlife = false;
    }
}
