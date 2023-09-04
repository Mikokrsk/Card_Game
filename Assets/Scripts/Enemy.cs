using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string name;
    [SerializeField] private Text nameText;
    public int health;
    public int maxHealth;
    [SerializeField] private Slider healthSlider;
    public int armor;
    public int maxArmor;
    [SerializeField] private Slider armorSlider;
    public int strength;
    public int healPower;
    public int blockingPower;
    public int minBlockingPower = 1;
    public bool isAlive;
    public Animator animator;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform cam;
    [SerializeField] protected int stepCount = 3;

    private void Awake()
    {
        SetEnemyAttributes();
        SetDefaultEnemyValues();
    }

    private void SetDefaultEnemyValues()
    {
        canvas.transform.LookAt(canvas.transform.position + cam.forward);
        blockingPower = minBlockingPower;
        healthSlider.maxValue = health = maxHealth;
        healthSlider.value = health;
        armorSlider.maxValue = armor = maxArmor;
        armorSlider.value = armor;
        nameText.text = name;
    }

    private void SetEnemyAttributes()
    {
        canvas = gameObject.GetComponentInChildren<Canvas>();
        nameText = canvas.transform.Find("EnemyName").GetComponent<Text>();
        healthSlider = canvas.transform.Find("HealthSlider").GetComponent<Slider>();
        armorSlider = canvas.transform.Find("ArmorSlider").GetComponent<Slider>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.GetComponent<Transform>();
    }

    public virtual IEnumerator EnemyTurn()
    {
        for (int i = 0; i < stepCount; i++)
        {
            var animationTime = 1f;
            int index = Random.Range(0, 3);
            if (index == 0 && blockingPower <= minBlockingPower * 2)
            {
                animationTime = EnemyProtection();
            }
            else if (index == 1 && health <= maxHealth - healPower)
            {
                animationTime = EnemyHeal();
            }
            else
            {
                animationTime = EnemyAttack(BattleManager.Instance.player);
            }

            Debug.Log($"Enemy Time = {animationTime}");
            yield return new WaitForSeconds(animationTime+1);
            if (BattleManager.Instance.player.isAlive==false)
            {
                gameManager.Instance.GameOverState();
            }
        }
        BattleManager.Instance.PlayerTurn();
        StopCoroutine(EnemyTurn());
    }

    public virtual float EnemyAttack(Player player)
    {
        animator.SetTrigger("Attack");
        var animWaitTimeEnemy = animator.GetCurrentAnimatorStateInfo(0).length;
        var animWaitTimePlayer = player.PlayerGetDamage(strength);
        var animWaitTime = animWaitTimeEnemy;
        if (animWaitTimeEnemy < animWaitTimePlayer)
        {
            animWaitTime = animWaitTimePlayer;
        }
        return animWaitTime;
    }
    public virtual float EnemyHeal()
    {
        //Heal
        animator.SetTrigger("Heal");
        health += healPower;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        UpdateHUD();
        var animWaitTimeEnemy = animator.GetCurrentAnimatorStateInfo(0).length;
        return animWaitTimeEnemy;
    }
    public virtual float EnemyProtection()
    {
        animator.SetTrigger("Protection");
        blockingPower += blockingPower;
        var animWaitTimeEnemy = animator.GetCurrentAnimatorStateInfo(0).length;
        return animWaitTimeEnemy;
    }
    public virtual float EnemyGetDamage(int damage)
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
            animWaitTime = EnemyDeath();
            Debug.Log($"AnimTime (EnemyDeath) = {animWaitTime}");
            return animWaitTime;
        }
        else
        {
            Debug.Log($"AnimTime (Enemy GetDamage) = {animWaitTime}");
            return animWaitTime;
        }
    }

    public float EnemyDeath()
    {
        animator.SetBool("Death", true);
        //animator.SetTrigger("GetDamage");
        isAlive = false;
        var animWaitTime = animator.GetCurrentAnimatorStateInfo(0).length;
        return animWaitTime;
    }

    public void UpdateHUD()
    {
        healthSlider.value = health;
        armorSlider.value = armor;
    }

}

