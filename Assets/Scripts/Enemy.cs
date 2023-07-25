using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    [SerializeField] private Slider healthSlider;
    public int armor;
    public int maxArmor;
    [SerializeField] private Slider armorSlider;
    public int strength;
    public int blockingPower;
    public int minBlockingPower = 1;
    public bool isAlive;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform cam;
    // [SerializeField] private Player player;

    private void Awake()
    {
        blockingPower = minBlockingPower;
        healthSlider.maxValue = health = maxHealth;
        healthSlider.value = health;
        armorSlider.maxValue = armor = maxArmor;
        armorSlider.value = armor;
        BattleManager.Instance.enemy = this;
        cam = Camera.main.GetComponent<Transform>();
        canvas.transform.LookAt(transform.position + cam.forward);
    }
    private void OnEnable()
    {
/*        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        armorSlider.maxValue = maxArmor;
        armorSlider.value = armor;
        BattleManager.Instance.enemy = this;
        cam = Camera.main.GetComponent<Transform>();
        canvas.transform.LookAt(transform.position + cam.forward);*/
    }
    /*public void Attack()
    {
        player.TakeDamage(strength * attackPower);
    }*/

/*    public void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            armor -= damage;
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

    private void TakeDamageHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
        UpdateHUD();
    }*/
    public void Death()
    {
        Destroy(gameObject);
    }

    public void UpdateHUD()
    {
        healthSlider.value = health;
        armorSlider.value = armor;
    }
}
