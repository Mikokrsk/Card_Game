using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int s_health;
    public static int s_maxHealth;

    public static int s_armor;
    public static int s_maxArmor;

    public static int s_strength;
    public static int s_agility;
    public static int s_intelligence;
    public static int s_endurance;
   // public static int s_specialSkill;

    public static int s_money;  
    public static int s_experience;   
    public static int s_blockingPower;    
    public static bool s_isAlife;
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
        if (s_armor > 0)
        {
            s_armor -= damage / s_blockingPower;
            if (s_armor < 0)
            {
                TakeDamageHealth(s_armor);
            }
        }
        else
        {
            TakeDamageHealth(damage);
        }

    }

    private void TakeDamageHealth(int damage)
    {
        s_health -= damage;
        if (s_health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        s_isAlife = false;
    }
}
