using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int s_health;
    public int s_maxHealth;

    public int s_armor;
    public int s_maxArmor;

    public int s_strength;
    public int s_agility;
    public int s_intelligence;
    public int s_endurance;
   // public static int s_specialSkill;

    public int s_money;  
    public int s_experience;   
    public int s_blockingPower;    
    public bool s_isAlife;
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
